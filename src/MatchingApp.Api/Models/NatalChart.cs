using System.Text.Json.Serialization;

namespace MatchingApp.Api.Models
{
    public class NatalChart
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string? SunSign { get; set; }
        public string? MoonSign { get; set; }
        public string? MercurySign { get; set; }
        public string? VenusSign { get; set; }
        public string? MarsSign { get; set; }
        public string? JupiterSign { get; set; }
        public string? SaturnSign { get; set; }
        public string? UranusSign { get; set; }
        public string? NeptuneSign { get; set; }
        public string? PlutoSign { get; set; }
        public string? Ascendant { get; set; }
        [JsonIgnore]
        public Client? Client { get; set; }
    }
}
