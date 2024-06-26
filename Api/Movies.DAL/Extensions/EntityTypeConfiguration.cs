using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.DAL.Entities;

namespace Movies.DAL.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring entity types.
    /// </summary>
    public static class EntityTypeConfiguration
    {
        /// <summary>
        /// Configures the base properties for an entity that inherits from <see cref="BaseEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="builder">The builder to configure the entity type.</param>
        public static void ConfigureBaseEntity<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : BaseEntity
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Created).IsRequired();
            builder.Property(e => e.Modified).IsRequired();
        }
    }
}
