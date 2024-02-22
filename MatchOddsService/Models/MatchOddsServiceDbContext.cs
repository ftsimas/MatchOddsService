using MatchOddsService.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace MatchOddsService.Models
{
    public class MatchOddsServiceDbContext : DbContext
    {
        public MatchOddsServiceDbContext(DbContextOptions<MatchOddsServiceDbContext> options) : base(options) { }

        public DbSet<Match> Matches { get; set; }

        public DbSet<MatchOdds> MatchOdds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MatchOdds>()
                .HasOne(mo => mo.Match)
                .WithMany(m => m.MatchOdds)
                .HasForeignKey(mo => mo.MatchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasIndex(m => new { m.MatchDate, m.TeamA, m.TeamB, m.Sport })
                .IsUnique();

            modelBuilder.Entity<MatchOdds>()
                .HasIndex(mo => new { mo.MatchId, mo.Specifier })
                .IsUnique();

        }
    }
}
