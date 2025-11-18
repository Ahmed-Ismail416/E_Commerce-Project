using DomainLayer.Exceptions;
using Shared.ErrorModels;

namespace E_Commerce.MiddleWares
{
    public class CustomExcepionHnadlerMiddleWare
    {
        public RequestDelegate _next { get; }
        public ILogger _logger { get; }

        public CustomExcepionHnadlerMiddleWare(RequestDelegate Next, ILogger<CustomExcepionHnadlerMiddleWare> Logger)
        {
            this._next = Next;
            this._logger = Logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                await HnadleNotFoundEndPointAsync(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            var Response = new ErrorToReturn
            {
                message = ex.Message
            };
            // Set Status Code
            Response.statusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnUthorizedException => StatusCodes.Status401Unauthorized,
                BadRequestException badrequestexception => GetBadRequestErrors(badrequestexception, Response),
                _ => 500
            };

            context.Response.StatusCode = Response.statusCode;
            // return response
            await context.Response.WriteAsJsonAsync(Response);
        }

        private static int GetBadRequestErrors(BadRequestException badrequestexception, ErrorToReturn Response)
        {
             
            Response.Errors = badrequestexception.Errors;
            return StatusCodes.Status400BadRequest;
        }

        private static async Task HnadleNotFoundEndPointAsync(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                await context.Response.WriteAsJsonAsync(new ErrorToReturn
                {
                    statusCode = context.Response.StatusCode,
                    message = $"End Point {context.Request.Path} Not Found"
                });
            }
        }
    }
}
