using MatchingApp.Api.Data;
using MatchingApp.Api.Models;
using MatchingApp.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatchingApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly MatchService _matchService;

        public MatchesController(AppDbContext context, MatchService matchService)
        {
            _context = context;
            _matchService = matchService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatches()
        {
            return await _context.Matches.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> GetMatch(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            return match;
        }

        public class CreateMatchRequest
        {
            public int ClientAId { get; set; }
            public int ClientBId { get; set; }
        }

        [HttpPost]
        public async Task<ActionResult<Match>> CreateMatch(CreateMatchRequest request)
        {
            if (request.ClientAId == request.ClientBId)
            {
                return BadRequest();
            }

            var existing = await _context.Matches
                .AnyAsync(m =>
                    (m.ClientAId == request.ClientAId && m.ClientBId == request.ClientBId) ||
                    (m.ClientAId == request.ClientBId && m.ClientBId == request.ClientAId));

            if (existing)
            {
                return Conflict();
            }

            var clientA = await _context.Clients.Include(c => c.NatalChart).FirstOrDefaultAsync(c => c.Id == request.ClientAId);
            var clientB = await _context.Clients.Include(c => c.NatalChart).FirstOrDefaultAsync(c => c.Id == request.ClientBId);

            if (clientA == null || clientB == null)
            {
                return NotFound();
            }

            var score = _matchService.CalculateCompatibility(clientA.NatalChart, clientB.NatalChart);

            var match = new Match
            {
                ClientAId = request.ClientAId,
                ClientBId = request.ClientBId,
                Score = score
            };

            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMatch), new { id = match.Id }, match);
        }

        [HttpGet("recommendations/{clientId}")]
        public async Task<ActionResult<IEnumerable<MatchRecommendation>>> GetRecommendations(int clientId, [FromQuery] int top = 5)
        {
            if (top < 1) top = 5;

            var client = await _context.Clients.Include(c => c.NatalChart).FirstOrDefaultAsync(c => c.Id == clientId);
            if (client == null)
            {
                return NotFound();
            }

            var others = await _context.Clients
                .Include(c => c.NatalChart)
                .Where(c => c.Id != clientId && c.Name != client.Name)
                .ToListAsync();

            var recs = others.Select(o =>
            {
                var detail = _matchService.CalculateCompatibilityDetail(client.NatalChart, o.NatalChart);
                return new MatchRecommendation
                {
                    Client = o,
                    Score = detail.Score,
                    Reasons = detail.Reasons
                };
            })
            .OrderByDescending(r => r.Score)
            .Take(top)
            .ToList();

            return recs;
        }
    }
}
