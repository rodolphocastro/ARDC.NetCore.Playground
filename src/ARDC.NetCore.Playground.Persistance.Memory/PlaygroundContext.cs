using ARDC.NetCore.Playground.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ARDC.NetCore.Playground.Persistance.Memory
{
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
    }
}
