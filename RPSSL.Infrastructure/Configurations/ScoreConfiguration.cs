using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RPSSL.Domain.Entities;

namespace RPSSL.Infrastructure.Configurations
{
    internal sealed class ScoreConfiguration : IEntityTypeConfiguration<Score>
    {
        public void Configure(EntityTypeBuilder<Score> builder)
        {
            builder.ToTable("Scores");

            builder.HasKey(webinar => webinar.Id);

            builder.Property(webinar => webinar.PlayerOne).HasMaxLength(100);
            builder.Property(webinar => webinar.PlayerTwo).HasMaxLength(100);
        }
    }
}
