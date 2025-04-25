using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net;
using System.Text.Json;  
using System.Threading.Tasks;

namespace Product_Inventory_Management_API
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new
            {
                Message = "An unexpected error occurred.",
                Detail = exception.Message
            };

            if (exception is FileNotFoundException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse = new
                {
                    Message = "The requested resource was not found.",
                    Detail = exception.Message
                };
            }

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
