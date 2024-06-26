using Microsoft.EntityFrameworkCore;
using Movies.DAL.Configurations.Base;
using Movies.DAL.Entities;
using System.Reflection;

namespace Movies.DAL
{
    public class MoviesDbContext : DbContext
    {
        public DbSet<SearchResult> SearchResults { get; set; }

        public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyConfigurations(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private static void ApplyConfigurations(ModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type =>
                       type.BaseType != null &&
                       type.BaseType.IsGenericType &&
                       type.BaseType.GetGenericTypeDefinition() == typeof(BaseTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                var configurationInstance = Activator.CreateInstance(type) as IBaseTypeConfiguration;
                configurationInstance?.ApplyConfiguration(modelBuilder);
            }
        }
    }
}
