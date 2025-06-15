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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Unique index for Match pairs
            modelBuilder.Entity<Match>()
                .HasIndex(m => new { m.ClientAId, m.ClientBId })
                .IsUnique();

            // No need to define the index for Client.Email again,
            // because the [Index] attribute on Client takes care of it

            base.OnModelCreating(modelBuilder);
        }
    }
}
