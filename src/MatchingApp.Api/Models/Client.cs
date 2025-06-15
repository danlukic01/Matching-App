namespace MatchingApp.Api.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public DateTime BirthDate { get; set; }
        public TimeSpan BirthTime { get; set; }
        public string? BirthLocation { get; set; }
        public NatalChart? NatalChart { get; set; }
    }
}
