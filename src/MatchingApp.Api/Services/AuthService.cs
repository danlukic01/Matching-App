namespace MatchingApp.Api.Services
{
    public class AuthService
    {
        private readonly Dictionary<string, int> _sessions = new();

        public string CreateSession(int clientId)
        {
            var token = Guid.NewGuid().ToString();
            _sessions[token] = clientId;
            return token;
        }

        public int? GetClientId(string? token)
        {
            if (token != null && _sessions.TryGetValue(token, out var id))
            {
                return id;
            }
            return null;
        }
    }
}
