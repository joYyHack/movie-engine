using Microsoft.EntityFrameworkCore;

namespace Movies.DAL.Configurations.Base
{
    public interface IBaseTypeConfiguration
    {
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}
