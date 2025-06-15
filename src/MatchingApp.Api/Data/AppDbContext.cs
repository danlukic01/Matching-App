using Microsoft.EntityFrameworkCore;
using MatchingApp.Api.Models;

namespace MatchingApp.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Clients => Set<Client>();
        public DbSet<NatalChart> NatalCharts => Set<NatalChart>();
        public DbSet<Match> Matches => Set<Match>();
    }
}
