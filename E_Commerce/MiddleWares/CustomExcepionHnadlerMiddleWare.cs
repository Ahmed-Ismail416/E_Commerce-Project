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
            // Set Status Code
            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                _ => 500
            };
            // set content type
            context.Response.ContentType = "application/json";
            // Create Object Response
            var response = new ErrorToReturn
            {
                statusCode = context.Response.StatusCode,
                message = ex.Message
            };

            // return response
            await context.Response.WriteAsJsonAsync(response);
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
