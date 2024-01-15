
using System.Net;

namespace NZwalker.Middleware;
public class ExceptionHandlerMiddleware(
    ILogger<ExceptionHandlerMiddleware> logger,
    RequestDelegate next
)
{
    private readonly ILogger<ExceptionHandlerMiddleware> logger = logger;
    private readonly RequestDelegate next = next;

    public async Task InvokeAsync(HttpContext httpContext){
        try
        {
            await next(httpContext);
        }
        catch (System.Exception e)
        {

            Guid errorId = Guid.NewGuid();
            
            // Log this exception
            logger.LogError(e, $"{errorId.ToString()} : {e.Message}");

            // return custom error response
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var error = new {
                Id = errorId,
                Message = "Something went wrong"
            };

            await httpContext.Response.WriteAsJsonAsync(error);
        }
    }
    
}