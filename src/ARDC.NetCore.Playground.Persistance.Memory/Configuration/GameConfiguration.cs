using ARDC.NetCore.Playground.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARDC.NetCore.Playground.Persistence.Core.Configuration
{
    /// <summary>
    /// Configurations for the Game Entity.
    /// </summary>
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            // Id
            builder.HasKey(g => g.Id);

            // Name
            builder.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(255);

            // ReleaseDate
            builder.Property(g => g.ReleasedOn)
                .IsRequired();

            // Relationships
            builder.HasMany<Review>().WithOne(r => r.Subject).HasForeignKey(r => r.SubjectId);
        }
    }
}
