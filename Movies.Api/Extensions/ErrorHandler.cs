using MoviesFetcher.Middleware;

namespace MoviesFetcher.Extensions
{
    public static class ErrorHandler
    {
        public static void UseErrorHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
