using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;
using RPSSL.Application.Exceptions;
using System.Text.Json;

namespace RPSSL.Presentation.Exceptions
{
    internal sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            string responseBody = "";

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = exception switch
            {
                CustomValidationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            if (exception is CustomValidationException validationException)
            {
                var problemDetails = new ValidationProblemDetails(validationException.Errors);

                problemDetails.Status = httpContext.Response.StatusCode;

                problemDetails.Title = validationException.Message;

                responseBody = JsonSerializer.Serialize(problemDetails);
            }
            else
            {
                var problemDetails = new ProblemDetails
                {
                    Status = httpContext.Response.StatusCode,
                    Title = "Server error",
                    Detail = exception.Message
                };

                responseBody = JsonSerializer.Serialize(problemDetails);

                _logger.LogError(
                    exception, "Exception occurred: {Message}", exception.Message);
            }

            await httpContext.Response.WriteAsync(responseBody, cancellationToken);

            return true;
        }
    }
}
