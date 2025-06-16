using System;

namespace MatchingApp.Api.Models
{
    public class ContactRequest
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Type { get; set; } = "like"; // like, wink, message
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
