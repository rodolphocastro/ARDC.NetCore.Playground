using ARDC.NetCore.Playground.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARDC.NetCore.Playground.Persistence.Core.Configuration
{
    /// <summary>
    /// Configurations for the Review Entity.
    /// </summary>
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            // ID
            builder.HasKey(r => r.Id);

            // Author Name
            builder.Property(r => r.AuthorName)
                .IsRequired()
                .HasMaxLength(255);

            // Text
            builder.Property(r => r.ReviewText)
                .IsRequired()
                .HasMaxLength(2000);

            // Score
            builder.Property(r => r.Score)
                .IsRequired();

            // SubjectId
            builder.Property(r => r.SubjectId)
                .IsRequired();

            // Relationships
            builder.HasOne(r => r.Subject).WithMany().HasForeignKey(r => r.SubjectId);
        }
    }
}
