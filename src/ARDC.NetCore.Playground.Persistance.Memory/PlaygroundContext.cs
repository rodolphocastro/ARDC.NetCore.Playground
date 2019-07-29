using ARDC.NetCore.Playground.Domain.Models;
using ARDC.NetCore.Playground.Persistance.Memory.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ARDC.NetCore.Playground.Persistance.Memory
{
    /// <summary>
    /// InMemory Context for the Playground Models.
    /// </summary>
    public class PlaygroundContext : DbContext
    {
        public PlaygroundContext()
        {
        }

        public PlaygroundContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Game> Games { get; protected set; }

        public DbSet<Review> Reviews { get; protected set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new GameConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
        }
    }
}
