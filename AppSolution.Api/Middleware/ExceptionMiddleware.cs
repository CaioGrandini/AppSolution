using System.Text.Json;
using AppSolution.Domain.Exceptions;

namespace AppSolution.Api.Middleware;

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
        catch (ValidationException ex)
        {
            context.Response.StatusCode = 400;

            await context.Response.WriteAsJsonAsync(
                new
                {
                    message = ex.Message
                });
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode = 404;

            await context.Response.WriteAsJsonAsync(
                new
                {
                    message = ex.Message
                });
        }
        catch (Exception)
        {
            context.Response.StatusCode = 500;

            await context.Response.WriteAsJsonAsync(
                new
                {
                    message = "Erro interno."
                });
        }
    }
}