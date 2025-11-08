
using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.UnitOfWork;
using ServiceAbstraction;
using Services;
using Services.Mapping;

namespace E_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services


            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options =>
                                                        options.UseSqlServer(builder.Configuration.GetConnectionString("E_Commerce")));
            builder.Services.AddScoped<IDataSeeding, Persistence.DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(p => p.AddProfile(new ProfileMapping()));
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddTransient<PictureResolver>();
            #endregion

            var app = builder.Build();
            using var scope = app.Services.CreateScope();
            var objectofdataseeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await objectofdataseeding.SeedAsync();


            #region PipeLine

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
