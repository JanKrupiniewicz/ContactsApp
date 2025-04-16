using System.Net;
using System.Text.Json;

namespace ContactsApp.Server.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var statusCode = HttpStatusCode.InternalServerError;
            var result = JsonSerializer.Serialize(new
            {
                error = exception.Message,
                stackTrace = exception.StackTrace
            });

            response.StatusCode = (int)statusCode;
            return response.WriteAsync(result);
        }
    }
}
