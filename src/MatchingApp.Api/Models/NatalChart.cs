using System.Text.Json.Serialization;

namespace MatchingApp.Api.Models
{
    public class NatalChart
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string? SunSign { get; set; }
        public string? MoonSign { get; set; }
        public string? Ascendant { get; set; }
        [JsonIgnore]
        public Client? Client { get; set; }
    }
}
