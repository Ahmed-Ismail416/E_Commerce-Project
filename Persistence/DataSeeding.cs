using DomainLayer.Contracts;
using DomainLayer.Models.Products;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public  class DataSeeding(StoreDbContext dbcontext) : IDataSeeding
    {
        public async Task SeedAsync()
        {
            var getMigrates = await dbcontext.Database.GetAppliedMigrationsAsync();
            if (getMigrates.Any())
            {
               await dbcontext.Database.MigrateAsync();
            }

            try
            {
                // 1. Brands
                if (!dbcontext.ProductBrands.Any())
                {
                    using var brandsFile = File.OpenRead(@"..\Persistence\DataSeed\brands.json");
                    var brands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(brandsFile);
                    if (brands?.Any() == true)
                    {
                        await dbcontext.ProductBrands.AddRangeAsync(brands);
                    }
                }
                // 2. Types
                if (!dbcontext.ProductTypes.Any())
                {
                    using var typesFile = File.OpenRead(@"..\Persistence\DataSeed\types.json");
                    var types = await JsonSerializer.DeserializeAsync<List<ProductType>>(typesFile);
                    if (types?.Any() == true)
                    {
                        await dbcontext.ProductTypes.AddRangeAsync(types);
                    }
                }

                // 2. Products
                if (!dbcontext.Products.Any())
                {
                    using var productsFile = File.OpenRead(@"..\Persistence\DataSeed\products.json");
                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(productsFile);
                    if (products?.Any() == true)
                    {
                        await dbcontext.Products.AddRangeAsync(products);
                    }
                }

              

                // ✅ احفظ الكل مرة واحدة في نهاية المطاف
                await dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // 🚨 لا تترك الـ catch فارغ! ده يخليك تضيع الأخطاء
                Console.WriteLine($"Seeding Error: {ex.Message}");
                throw; // أو سجّل الخطأ في Logger
            }
        }
    }

}
