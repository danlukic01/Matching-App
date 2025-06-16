using System.Security.Cryptography;
using System.Text;
using MatchingApp.Api.Data;
using MatchingApp.Api.Models;
using MatchingApp.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatchingApp.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class ClientProfileUpdateDto : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly NatalChartService _natalService;
        private readonly AuthService _authService;

        public ClientProfileUpdateDto(AppDbContext context, NatalChartService natalService, AuthService authService)
        {
            _context = context;
            _natalService = natalService;
            _authService = authService;
        }

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes);
        }

        public class RegisterRequest
        {
            public string? Name { get; set; }
            public DateTime BirthDate { get; set; }
            public TimeSpan BirthTime { get; set; }
            public string? BirthLocation { get; set; }
            public string? Gender { get; set; }
            public string? PreferredGender { get; set; }
            public string? Bio { get; set; }
            public string? Interests { get; set; }
            public bool IsProfilePublic { get; set; } = true;
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (await _context.Clients.AnyAsync(c => c.Email == request.Email))
            {
                return Conflict("Email already registered");
            }

            var client = new Client
            {
                Name = request.Name,
                BirthDate = request.BirthDate,
                BirthTime = request.BirthTime,
                BirthLocation = request.BirthLocation,
                Gender = request.Gender,
                PreferredGender = request.PreferredGender,
                Bio = request.Bio,
                Interests = request.Interests,
                IsProfilePublic = request.IsProfilePublic,
                Email = request.Email,
                PasswordHash = HashPassword(request.Password)
            };

            client.NatalChart = _natalService.Calculate(client);
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            var token = _authService.CreateSession(client.Id);
            return Ok(new { token, clientId = client.Id });
        }

        public class LoginRequest
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Email == request.Email);
            if (client == null)
            {
                return Unauthorized();
            }

            var hash = HashPassword(request.Password);
            if (client.PasswordHash != hash)
            {
                return Unauthorized();
            }

            var token = _authService.CreateSession(client.Id);
            return Ok(new { token, clientId = client.Id });
        }
    }
}
