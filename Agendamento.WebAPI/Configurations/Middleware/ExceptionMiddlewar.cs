using Agendamento.Domain.Exceptions;
using FluentValidation;
using System.Net;
using System.Text;
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
                DomainValidationException _ => (int)HttpStatusCode.BadRequest,
                ValidationException _ => (int)HttpStatusCode.BadRequest,
                NotFoundException _ => (int)HttpStatusCode.NotFound,
                ConflictException _ => (int)HttpStatusCode.Conflict,
                DatabaseException _ => (int)HttpStatusCode.InternalServerError,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var message = exception is ValidationException validationException
                ? FormatValidationErrors(validationException)
                : exception.Message;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = message.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " ")
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var responseText = JsonSerializer.Serialize(response, options);
            return context.Response.WriteAsync(responseText);
        }

        private static string FormatValidationErrors(ValidationException validationException)
        {
            var sb = new StringBuilder();
            foreach (var error in validationException.Errors)
            {
                sb.AppendLine($"{error.PropertyName}: {error.ErrorMessage}");
            }
            return sb.ToString().Trim();
        }
    }
}