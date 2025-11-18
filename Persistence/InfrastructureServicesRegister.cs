using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.UnitOfWork;
using StackExchange.Redis;
using Persistence.Repositories;
using Persistence.Identity;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
namespace Persistence
{
    public static class InfrastructureServicesRegister
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            // Here you can register your infrastructure services, e.g., database context, repositories, etc.
            Services.AddDbContext<StoreDbContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("E_Commerce")));
            Services.AddScoped<IDataSeeding, Persistence.DataSeeding>();
            Services.AddScoped<IUnitOfWork, Persistence.UnitOfWork.UnitOfWork>();
            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(Configuration.GetConnectionString("Redis"));
            });

            Services.AddDbContext<StoreIdentityDbContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("E_Commerce.Identity")));

            Services.AddIdentityCore<ApplicationUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<StoreIdentityDbContext>();

            return Services;
        }
    }
}
