using System.Text.Json;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ValidationExceptionMiddleware> _logger;

    public ValidationExceptionMiddleware(RequestDelegate next, ILogger<ValidationExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "EF Core update exception");
            context.Response.StatusCode = 400;

            var errorDetails = "An unexpected database error occurred. Please try again or contact support.";

            var message = ex.InnerException?.Message ?? ex.Message;

            if (message.Contains("FOREIGN KEY", StringComparison.OrdinalIgnoreCase))
                errorDetails = "A related record was not found. Please ensure all referenced entities exist.";

            if (message.Contains("UNIQUE", StringComparison.OrdinalIgnoreCase) ||
                message.Contains("duplicate key", StringComparison.OrdinalIgnoreCase))
                errorDetails = "This record already exists or a unique field is duplicated.";

            if (message.Contains("cannot insert the value NULL", StringComparison.OrdinalIgnoreCase) ||
                message.Contains("violates not-null constraint", StringComparison.OrdinalIgnoreCase))
                errorDetails = "Some required fields were not provided.";

            if (message.Contains("CHECK constraint", StringComparison.OrdinalIgnoreCase))
                errorDetails = "A business rule (check constraint) has been violated.";

            if (message.Contains("REFERENCE constraint", StringComparison.OrdinalIgnoreCase))
                errorDetails = "Cannot delete or update a record because it is used by another table.";
            
            await context.Response.WriteAsJsonAsync(new { error = errorDetails });
        }
        catch (ValidationException ex)
        {
            await HandleValidationExceptionAsync(context, ex);
        }
    }

    private static Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status400BadRequest;

        var response = new ApiResponse
        {
            Success = false,
            Message = "Validation Failed",
            Errors = exception.Errors
                .Select(error => (ValidationErrorDetail)error)
        };

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
    }
}