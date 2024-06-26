using Microsoft.EntityFrameworkCore;

namespace Movies.DAL.Configurations.Base
{
    /// <summary>
    /// Base interface for applying configurations to the model builder.
    /// </summary>
    public interface IBaseTypeConfiguration
    {
        /// <summary>
        /// Applies this configuration instance to the specified model builder.
        /// </summary>
        /// <param name="modelBuilder">The model builder to apply configuration to.</param>
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}