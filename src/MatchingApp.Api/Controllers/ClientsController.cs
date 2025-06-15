using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MatchingApp.Api.Data;
using MatchingApp.Api.Models;
using MatchingApp.Api.Services;
using System.Collections.Generic;

namespace MatchingApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly NatalChartService _natalService;

        public ClientsController(AppDbContext context, NatalChartService natalService)
        {
            _context = context;
            _natalService = natalService;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, Client updated)
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

            var chart = _natalService.Calculate(client);

            if (client.NatalChart == null)
            {
                client.NatalChart = chart;
            }
            else
            {
                client.NatalChart.SunSign = chart.SunSign;
                client.NatalChart.MoonSign = chart.MoonSign;
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
