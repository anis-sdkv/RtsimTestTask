using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RtsimTestTask.Domain.Exceptions;

namespace RtsimTestTask.Api.Middleware;

public class DefaultExceptionHandlerMiddleware(RequestDelegate next, ILogger<DefaultExceptionHandlerMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            var response = context.Response;
            context.Response.ContentType = "text/plain";
            if (exception is DomainException)
            {
                response.StatusCode = 400;
                await response.WriteAsync($"{exception.GetType().FullName}\n{exception.Message}");
            }
            else
            {
                logger.LogError(exception, exception.Message);
                response.StatusCode = 500;
                await response.WriteAsync(
                    "Something has gone wrong with the server. We are currently working to fix it.");
            }
        }
    }
}