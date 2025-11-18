using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

        public static IServiceCollection AddJWTService(this IServiceCollection Services, IConfiguration _config)
        {
            Services.AddAuthentication(Config =>
            {
                Config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _config["JWTOptions:Issuer"],
                    ValidAudience = _config["JWTOptions:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["JWTOptions:SecretKey"]))
                };
            });
            return Services;    
        }
    }
}
