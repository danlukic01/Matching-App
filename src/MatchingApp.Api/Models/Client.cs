using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MatchingApp.Api.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Client
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [MaxLength(450)]
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public DateTime BirthDate { get; set; }
        public TimeSpan BirthTime { get; set; }
        public string? BirthLocation { get; set; }
        public NatalChart? NatalChart { get; set; }
    }
}
