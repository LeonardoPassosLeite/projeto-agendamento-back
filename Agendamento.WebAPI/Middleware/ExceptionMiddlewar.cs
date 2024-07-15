using Agendamento.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace Agendamento.WebAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception switch
            {
                DomainValidationException => (int)HttpStatusCode.Conflict,
                NotFoundException => (int)HttpStatusCode.NotFound,
                DatabaseException => (int)HttpStatusCode.InternalServerError,
                _ => (int)HttpStatusCode.InternalServerError,
            };

            var response = new
            {
                context.Response.StatusCode,
                Message = exception.Message ?? "An error occurred."
            };

            var responseText = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(responseText);
        }
    }
}