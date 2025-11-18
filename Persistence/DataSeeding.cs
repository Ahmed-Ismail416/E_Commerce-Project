using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(StoreDbContext dbcontext,
                            UserManager<ApplicationUser> _userManager,
                            RoleManager<IdentityRole> _roleManager,
                            StoreIdentityDbContext _identiyDbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
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
                    if (brands?.Count == 0)
                    {
                        await dbcontext.ProductBrands.AddRangeAsync(brands);
                    }
                }
                // 2. Types
                if (!dbcontext.ProductTypes.Any())
                {
                    using var typesFile = File.OpenRead(@"..\Persistence\DataSeed\types.json");
                    var types = await JsonSerializer.DeserializeAsync<List<ProductType>>(typesFile);
                    if (types?.Count == 0)
                    {
                        await dbcontext.ProductTypes.AddRangeAsync(types);
                    }
                }

                // 2. Products
                if (!dbcontext.Products.Any())
                {
                    using var productsFile = File.OpenRead(@"..\Persistence\DataSeed\products.json");
                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(productsFile);
                    if (products?.Count == 0)
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

        public async Task IdnentityDataSeedAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                    await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
                }
                if (!_userManager.Users.Any())
                {
                    var user1 = new ApplicationUser
                    {
                        Email = "ahmedesm415@gmai.com",
                        DisplayName = "Ahmed Ismail",
                        PhoneNumber = "1234578911",
                        UserName = "AhmedIsmail"
                    };
                    var user2 = new ApplicationUser
                    {
                        Email = "Ahmed@gmail.com",
                        DisplayName = "Ahmed Esmail",
                        PhoneNumber = "1234578911",
                        UserName = "Ahmed"
                    };
                   
                    // Adding Users
                    var createuser1= await _userManager.CreateAsync(user1, "P@ssw0rd");
                    if(!createuser1.Succeeded)
                        throw new Exception(string.Join(", ", createuser1.Errors.Select(e => e.Description)));
                    var createuser2 = await _userManager.CreateAsync(user2, "P@ssw0rd");
                    if (!createuser2.Succeeded)
                        throw new Exception(string.Join(", ", createuser2.Errors.Select(e => e.Description)));
                    
                    
                    
                    // Confirming Adding Users
                    var confirmuser1 = await _userManager.FindByEmailAsync(user1.Email);
                    var confirmuser2 = await _userManager.FindByEmailAsync(user2.Email);
                    if(confirmuser1 == null || confirmuser2 == null)
                        throw new Exception("User creation failed.");

                    //Adding Roles To users
                    var AdddRole1 = await _userManager.AddToRoleAsync(user1, "Admin");
                    if(!AdddRole1.Succeeded)
                        throw new Exception(string.Join(", ", AdddRole1.Errors.Select(e => e.Description)));
                    var AdddRole2 = await _userManager.AddToRoleAsync(user2, "SuperAdmin");
                    if (!AdddRole2.Succeeded)
                        throw new Exception(string.Join(", ", AdddRole2.Errors.Select(e => e.Description)));
                }
               // _identiyDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
