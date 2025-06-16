using Microsoft.AspNetCore.Mvc;

    // Models/ClientProfileUpdateDto.cs
    namespace MatchingApp.Api.Models
    {
        public class ClientProfileUpdateDto
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public DateTime BirthDate { get; set; }
            public TimeSpan BirthTime { get; set; }
            public string? BirthLocation { get; set; }
            public string? Gender { get; set; }
            public string? PreferredGender { get; set; }
            public string? Bio { get; set; }
            public string? Interests { get; set; }
            public bool IsProfilePublic { get; set; }
        }
    }


