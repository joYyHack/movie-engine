using Microsoft.EntityFrameworkCore;
using Movies.DAL;

namespace MoviesFetcher.Extensions
{
    public static class DbMigration
    {
        /// <summary>
        /// Applies any pending migrations for the context to the database and will create the database if it does not already exist.
        /// </summary>
        /// <param name="webApp">The web application instance.</param>
        /// <returns>The web application instance with the database migrated.</returns>
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
