using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class AplicationServicesRegesiter
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(p => p.AddProfiles(new Profile[]
            {
                new ProfileMapping(),
                new BasketProfile(),
                new IdentityProfile(),
                new OrderProfile()
            }));
            Services.AddTransient<PictureResolver>();
            Services.AddTransient<OrderPictureResolver>();
            //Services.AddAutoMapper(typeof(AplicationServicesRegesiter).Assembly);

            Services.AddScoped<IServiceManager, ServiceManager>();
            return Services;
        }
    }
}
