using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Extensions
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
            return Services;
        }
        public static IServiceCollection AddWebApiServices(this IServiceCollection Services)
        {
            Services.Configure<ApiBehaviorOptions>((options) =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState
                                         .Where(e => e.Value.Errors.Any())
                                         .Select(M => new Shared.ErrorModels.ValidationError()
                                         {
                                             Field = M.Key,
                                             Errors = M.Value.Errors.Select(e => e.ErrorMessage)
                                         });
                    var errorResponse = new Shared.ErrorModels.ValidationErrorToReturn
                    {
                        Message = "Validation Error",
                        ValidationErrors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return Services;
        }
    }
}
