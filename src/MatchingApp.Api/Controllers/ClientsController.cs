using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MatchingApp.Api.Data;
using MatchingApp.Api.Models;
using MatchingApp.Api.Services;

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
