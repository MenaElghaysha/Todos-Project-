using Application.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.Exceptions;

public sealed class GlobalExceptionHandler(
    IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        System.Exception exception,
        CancellationToken cancellationToken)
    {
        ProblemDetails problemDetails = exception switch
        {
            NotFoundException ex => new ProblemDetails
            {
                Title = "Resource not found.",
                Status = StatusCodes.Status404NotFound,
                Detail = exception.Message
            },

            ValidationException ex => new ValidationProblemDetails(
                ex.Errors
                    .GroupBy(g => g.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    )
            )
            {
                Title = "Validation failed.",
                Detail = "One or more validation errors occurred.",
                Status = StatusCodes.Status400BadRequest
            },

            _ => new ProblemDetails
            {
                Title = "An unexpected error occurred.",
                Status = StatusCodes.Status500InternalServerError,
                Detail = exception.Message
            }
        };
        
        httpContext.Response.StatusCode = problemDetails.Status!.Value;

        await problemDetailsService.WriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = problemDetails
        });

        return true;
    }
}