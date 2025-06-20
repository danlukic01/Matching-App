using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MatchingApp.Api.Data;
using MatchingApp.Api.Models;
using MatchingApp.Api.Services;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Collections.Generic;

namespace MatchingApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly NatalChartService _natalService;
        private readonly IWebHostEnvironment _env;

        public ClientsController(AppDbContext context, NatalChartService natalService, IWebHostEnvironment env)
        {
            _context = context;
            _natalService = natalService;
            _env = env;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _context.Clients.Include(c => c.NatalChart).FirstOrDefaultAsync(c => c.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            return client;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients([FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 20;

            var query = _context.Clients.Include(c => c.NatalChart).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c => c.Name != null && c.Name.Contains(search));
            }

            var clients = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return clients;
        }

        // --- UPDATED PUT METHOD USING DTO ---
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] ClientProfileUpdateDto updated)
        {
            if (id != updated.Id)
            {
                return BadRequest();
            }

            var client = await _context.Clients.Include(c => c.NatalChart).FirstOrDefaultAsync(c => c.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            client.Name = updated.Name;
            client.BirthDate = updated.BirthDate;
            client.BirthTime = updated.BirthTime;
            client.BirthLocation = updated.BirthLocation;
            client.Gender = updated.Gender;
            client.PreferredGender = updated.PreferredGender;
            client.Bio = updated.Bio;
            client.Interests = updated.Interests;
            client.IsProfilePublic = updated.IsProfilePublic;

            var chart = _natalService.Calculate(client);

            if (client.NatalChart == null)
            {
                client.NatalChart = chart;
            }
            else
            {
                client.NatalChart.SunSign = chart.SunSign;
                client.NatalChart.MoonSign = chart.MoonSign;
                client.NatalChart.MercurySign = chart.MercurySign;
                client.NatalChart.VenusSign = chart.VenusSign;
                client.NatalChart.MarsSign = chart.MarsSign;
                client.NatalChart.JupiterSign = chart.JupiterSign;
                client.NatalChart.SaturnSign = chart.SaturnSign;
                client.NatalChart.UranusSign = chart.UranusSign;
                client.NatalChart.NeptuneSign = chart.NeptuneSign;
                client.NatalChart.PlutoSign = chart.PlutoSign;
                client.NatalChart.Ascendant = chart.Ascendant;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}/photo")]
        public async Task<IActionResult> UploadPhoto(int id, IFormFile file)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            if (file == null || file.Length == 0)
            {
                return BadRequest();
            }

            var ext = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{ext}";
            var dir = Path.Combine(_env.WebRootPath, "profile-photos");
            Directory.CreateDirectory(dir);
            var path = Path.Combine(dir, fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            client.ProfilePhotoFileName = fileName;
            client.PhotoApproved = false;
            await _context.SaveChangesAsync();

            return Ok(new { fileName });
        }

        [HttpPost]
        public async Task<ActionResult<Client>> CreateClient(Client client)
        {
            var chart = _natalService.Calculate(client);
            client.NatalChart = chart;
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
        }
    }
}
