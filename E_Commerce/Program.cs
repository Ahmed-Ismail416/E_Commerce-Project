
using DomainLayer.Contracts;
using E_Commerce.Extensions;
using E_Commerce.MiddleWares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens.Experimental;
using Persistence;
using Persistence.Data;
using Persistence.UnitOfWork;
using ServiceAbstraction;
using Services;
using Services.Mapping;
using Shared.ErrorModels;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace E_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services


            builder.Services.AddControllers()
                            .AddApplicationPart(typeof(Presentation.Controllers.ProductController).Assembly);

            builder.Services.AddSwaggerServices();

            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddAplicationServices();

            
            builder.Services.AddWebApiServices();
            builder.Services.AddJWTService(builder.Configuration);
            #endregion

            var app = builder.Build();
            await app.DataSeed();

            app.UseCustomExceptionMiddleWare();
            #region PipeLine
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.ConfigObject = new()
                    {
                        DisplayRequestDuration = true
                    };
                    options.DocumentTitle = "E_Commerce";

                    options.DocExpansion( DocExpansion.None);

                });
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
