using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace realationshipss.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
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
                Console.WriteLine($"Exception: {ex.Message}");
                
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync
                (
                    System.Text.Json.JsonSerializer.Serialize
                    (
                        new
                        {
                            Error = "An unexpected error occurred",
                            Details = ex.Message
                        }
                    )
                );
        }

        }
    
    }
}
