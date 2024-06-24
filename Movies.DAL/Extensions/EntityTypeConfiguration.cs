using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.DAL.Entities;

namespace Movies.DAL.Extensions
{
    public static class EntityTypeConfiguration
    {
        public static void ConfigureBaseEntity<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : BaseEntity
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Created).IsRequired();
            builder.Property(e => e.Modified).IsRequired();
        }
    }
}
