using MatchingApp.Api.Data;
using MatchingApp.Api.Models;
using MatchingApp.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MatchingApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactRequestsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly MatchService _matchService;
        private readonly AuthService _authService;
        private readonly NatalChartService _natalService;

        public ContactRequestsController(AppDbContext context, MatchService matchService, AuthService authService, NatalChartService natalService)
        {
            _context = context;
            _matchService = matchService;
            _authService = authService;
            _natalService = natalService;
        }

        private int? GetAuthenticatedClientId()
        {
            var token = Request.Headers["X-Auth-Token"].FirstOrDefault();
            return _authService.GetClientId(token);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactRequest>>> GetRequests()
        {
            var clientId = GetAuthenticatedClientId();
            if (clientId == null)
            {
                return Unauthorized();
            }

            var requests = await _context.ContactRequests
                .Where(r => r.ReceiverId == clientId)
                .ToListAsync();

            return requests;
        }

        public class CreateRequest
        {
            public int ReceiverId { get; set; }
            public string Type { get; set; } = "like"; // like, wink, message
            public string? Message { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRequest request)
        {
            var senderId = GetAuthenticatedClientId();
            if (senderId == null)
            {
                return Unauthorized();
            }

            if (request.Type != "like" && request.Type != "wink" && request.Type != "message")
            {
                return BadRequest("Invalid request type");
            }

            var receiver = await _context.Clients.Include(c => c.NatalChart).FirstOrDefaultAsync(c => c.Id == request.ReceiverId);
            var sender = await _context.Clients.Include(c => c.NatalChart).FirstOrDefaultAsync(c => c.Id == senderId);
            if (receiver == null || sender == null)
            {
                return NotFound();
            }

            if (sender.NatalChart == null)
            {
                sender.NatalChart = _natalService.Calculate(sender);
            }
            if (receiver.NatalChart == null)
            {
                receiver.NatalChart = _natalService.Calculate(receiver);
            }
            if (_context.ChangeTracker.HasChanges())
            {
                await _context.SaveChangesAsync();
            }

            bool alreadyMatched = await _context.Matches.AnyAsync(m =>
                (m.ClientAId == senderId && m.ClientBId == request.ReceiverId) ||
                (m.ClientAId == request.ReceiverId && m.ClientBId == senderId));

            if (request.Type == "message")
            {
                if (!alreadyMatched)
                {
                    return Forbid();
                }
            }
            else
            {
                bool allowed = alreadyMatched;
                if (!allowed)
                {
                    // check top 5 recommendations
                    var others = await _context.Clients
                        .Include(c => c.NatalChart)
                        .Where(c => c.Id != senderId && c.Name != sender.Name)
                        .Where(c => string.IsNullOrEmpty(sender.PreferredGender) || c.Gender == sender.PreferredGender)
                        .Where(c => string.IsNullOrEmpty(c.PreferredGender) || c.PreferredGender == sender.Gender)
                        .ToListAsync();

                    foreach (var other in others.Where(o => o.NatalChart == null))
                    {
                        other.NatalChart = _natalService.Calculate(other);
                    }

                    if (_context.ChangeTracker.HasChanges())
                    {
                        await _context.SaveChangesAsync();
                    }

                    var topIds = others.Select(o => new
                    {
                        Id = o.Id,
                        Score = _matchService.CalculateCompatibility(sender.NatalChart, o.NatalChart)
                    })
                    .OrderByDescending(o => o.Score)
                    .Take(5)
                    .Select(o => o.Id)
                    .ToList();

                    allowed = topIds.Contains(request.ReceiverId);
                }

                if (!allowed)
                {
                    return Forbid();
                }
            }

            var cr = new ContactRequest
            {
                SenderId = senderId.Value,
                ReceiverId = request.ReceiverId,
                Type = request.Type,
                Message = request.Message,
                CreatedAt = DateTime.UtcNow
            };

            _context.ContactRequests.Add(cr);

            if (request.Type == "like" || request.Type == "wink")
            {
                bool reciprocal = await _context.ContactRequests.AnyAsync(r =>
                    r.SenderId == request.ReceiverId &&
                    r.ReceiverId == senderId &&
                    (r.Type == "like" || r.Type == "wink"));

                if (reciprocal && !alreadyMatched)
                {
                    var score = _matchService.CalculateCompatibility(sender.NatalChart, receiver.NatalChart);
                    var match = new Match
                    {
                        ClientAId = senderId.Value,
                        ClientBId = request.ReceiverId,
                        Score = score
                    };
                    _context.Matches.Add(match);
                }
            }

            await _context.SaveChangesAsync();

            return Ok(cr);
        }
    }
}
