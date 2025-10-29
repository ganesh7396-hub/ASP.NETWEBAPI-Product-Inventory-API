
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using Product_Inventory_Management_API.Exceptions;


namespace Product_Inventory_Management_API.Middleware
{
    public class ErrorHandlingMiddleware
    {

     
            private readonly RequestDelegate _next;

            public ErrorHandlingMiddleware(RequestDelegate next)
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
                    await HandleExceptionAsync(context, ex);
                }
            }

            private static Task HandleExceptionAsync(HttpContext context, Exception ex)
            {


            var (statusCode, errorMessage) = ex switch
            {
                BadRequestException => (StatusCodes.Status400BadRequest, ex.Message),
                UnauthorizedException => (StatusCodes.Status401Unauthorized, ex.Message),
                ForbiddenException => (StatusCodes.Status403Forbidden, ex.Message),
                NotFoundException => (StatusCodes.Status404NotFound, ex.Message),
                ConflictException => (StatusCodes.Status409Conflict, ex.Message),
                _ => (StatusCodes.Status500InternalServerError, "An error occurred while processing your request.")
            };


            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";



            var response = new
                {
                    error = ex.Message,
                    details = ex.StackTrace
                };

                return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
            }
        }

    }
