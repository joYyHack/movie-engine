using Movies.DAL.Extensions;
using Movies.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Movies.DAL.Configurations.Base
{
    public abstract class BaseTypeConfiguration<TEntity> : IBaseTypeConfiguration, IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(this);
        }   

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ConfigureBaseEntity();
        }
    }
}
