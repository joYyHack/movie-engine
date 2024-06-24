using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.DAL.Configurations.Base;
using Movies.DAL.Entities;

namespace Movies.DAL.Configurations
{
    public class SearchTypeEntityConfiguration : BaseTypeConfiguration<SearchResult>
    {
        public override void Configure(EntityTypeBuilder<SearchResult> builder)
        {
            base.Configure(builder);

            builder.ToTable("SearchResults");
            builder.Property(u => u.MovieTitle).IsRequired();
        }
    }
}
