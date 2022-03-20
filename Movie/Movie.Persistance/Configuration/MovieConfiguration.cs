using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Movie.Persistance.Configuration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Domain.POCO.Movie>
    {
        public void Configure(EntityTypeBuilder<Domain.POCO.Movie> builder)
        {
            builder.Property(x => x.Id).HasColumnType("UniqueIdentifier");
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Gener).IsRequired();
            builder.Property(x => x.Director).IsRequired();
            builder.Property(x => x.IMDBReiting).IsRequired();
            builder.Property(x => x.PremierTime).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Currency).IsRequired().HasMaxLength(5);
            builder.Property(x => x.BannerUrl).IsRequired();
            builder.Property(x => x.Status).IsRequired();
        }
    }
}
