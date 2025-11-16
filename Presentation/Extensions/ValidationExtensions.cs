using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shared.ErrorModels;

namespace Presentation.Extensions
{
    public static class ValidationExtensions
    {
        public static IServiceCollection AddValidationErrorResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Any())
                        .Select(M => new ValidationError()
                        {
                            Field = M.Key,
                            Errors = M.Value.Errors.Select(e => e.ErrorMessage)
                        });

                    var errorResponse = new ValidationErrorToReturn
                    {
                        Message = "Validation Error",
                        ValidationErrors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}
