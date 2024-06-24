using Movies.BL.Models;
using Newtonsoft.Json;
using System.Net;

namespace MoviesFetcher.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var responseModel = new Response<string>
                {
                    Failed = true,
                    Message = $"{ex.Source}:{ex.TargetSite} - {ex?.Message}", 
                    HttpStatusCode = HttpStatusCode.InternalServerError
                };

                var result = JsonConvert.SerializeObject(responseModel);

                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await response.WriteAsync(result);

                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
