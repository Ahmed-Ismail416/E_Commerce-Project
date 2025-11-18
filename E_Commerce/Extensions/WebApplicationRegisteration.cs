using DomainLayer.Contracts;
using E_Commerce.MiddleWares;

namespace E_Commerce.Extensions
{
    public static class WebApplicationRegisteration
    {
        public static async Task DataSeed(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var objectofdataseeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await objectofdataseeding.DataSeedAsync();
            await objectofdataseeding.IdnentityDataSeedAsync();
        }
        public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExcepionHnadlerMiddleWare>();
            return app;
        }
    }
}
