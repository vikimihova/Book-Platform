using Microsoft.AspNetCore.Http;
using System.Net;

namespace BookPlatform.Web.Infrastructure.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate next;
        
        public CustomExceptionMiddleware(RequestDelegate next) 
        { 
            this.next = next; 
        }

        public async Task InvokeAsync(HttpContext httpContext) 
        { 
            try 
            { 
                await next(httpContext); 
            } 
            catch (Exception ex) 
            { 
                await HandleExceptionAsync(httpContext, ex); 
            } 
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception) 
        { 
            context.Response.ContentType = "text/html";                        

            if (exception is ArgumentException ||
                exception is ArgumentNullException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return context.Response.WriteAsync("<html><body><h1>400 Bad Request</h1><p>Invalid input provided.</p></body></html>");
            }

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync("<html><body><h1>500 Internal Server Error</h1><p>An unexpected error occurred.</p></body></html>");
        }
    }        
}
