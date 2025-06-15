namespace MatchingApp.Api.Models
{
    public class Match
    {
        public int Id { get; set; }
        public int ClientAId { get; set; }
        public int ClientBId { get; set; }
        public double Score { get; set; }
    }
}
