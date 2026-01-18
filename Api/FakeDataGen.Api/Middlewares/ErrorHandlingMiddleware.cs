using FakeDataGen.Api.Contracts;
using FakeDataGen.Application.Exceptions;

namespace FakeDataGen.Api.Middlewares;

public sealed class ErrorHandlingMiddleware(
    ILogger<ErrorHandlingMiddleware> logger
) : IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var error = new ErrorResponse(
                Message: ex.Message
            );

            await context.Response.WriteAsJsonAsync(error);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var error = new ErrorResponse(
                Message: "Ocorreu um erro inesperado.",
                Detail: ex.Message
            );

            await context.Response.WriteAsJsonAsync(error);
        }
    }
}
