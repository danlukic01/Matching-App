using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MatchingApp.Api.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Client
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; } = null!;

        [MaxLength(255)]
        public string? PasswordHash { get; set; }
        public DateTime BirthDate { get; set; }
        public TimeSpan BirthTime { get; set; }
        public string? BirthLocation { get; set; }
        public string? Gender { get; set; }
        public string? PreferredGender { get; set; }
        public string? Bio { get; set; }
        public string? Interests { get; set; }
        public bool IsProfilePublic { get; set; } = true;
        public string? ProfilePhotoFileName { get; set; }
        public bool PhotoApproved { get; set; } = false;
        public NatalChart? NatalChart { get; set; }
    }
}
