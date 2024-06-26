using MoviesFetcher.Middleware;

namespace MoviesFetcher.Extensions
{
    public static class ErrorHandler
    {
        /// <summary>
        /// Adds the custom error handling middleware to the application's request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public static void UseErrorHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
