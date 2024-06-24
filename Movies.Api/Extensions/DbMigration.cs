using Microsoft.EntityFrameworkCore;
using Movies.DAL;

namespace MoviesFetcher.Extensions
{
    public static class DbMigration
    {
        public static WebApplication MigrateDb(this WebApplication webApp)
        {
            using (var scope = webApp.Services.CreateScope())
            {
                using var dbContext = scope.ServiceProvider.GetRequiredService<MoviesDbContext>();
                try
                {
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<MoviesDbContext>>();
                    logger.LogError(ex, $"An error occurred while migrating the database. {ex.Message}");
                    throw;
                }
            }
            return webApp;
        }
    }
}
