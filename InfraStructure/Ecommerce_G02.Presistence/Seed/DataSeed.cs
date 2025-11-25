using Ecommerce_G02.Domain.Contacts.Seed;
using Ecommerce_G02.Domain.Models.Products;
using Ecommerce_G02.Domain.Models.Orders;
using Ecommerce_G02.Presistence.Contexts;
using Ecommerce_G02.Presistence.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce_G02.Presistence.Seed
{
    public class DataSeed(StoreDBContext _context,UserManager<ApplicationUser> usermanager,RoleManager<IdentityRole> rollmanger,StoreIdentityDbContext  identitycontext) : IDataSeed
    {
       // private readonly StoreDBContext _context;
        
        //public DataSeed(StoreDBContext context)
        //{
        //    this._context = context;
        //}

        #region Data Seeding For BD Context

        public async Task DataSeedingAsync()
        {
            var PindingMigration = await _context.Database.GetPendingMigrationsAsync();
            if (PindingMigration.Any())
            {
                _context.Database.Migrate();
            }

            if (!_context.ProductBrands.Any())
            {
                var productbrandData = await File.ReadAllTextAsync(@"..\InfraStructure\Ecommerce_G02.Presistence\Data\brands.json");
                var product_brand = JsonSerializer.Deserialize<List<ProductBrand>>(productbrandData);

                if (product_brand != null && product_brand.Any())
                {
                    _context.ProductBrands.AddRange(product_brand);
                }
            }

            /////////////type///////
            if (!_context.ProductTypes.Any())
            {
                var producttypeData = await File.ReadAllTextAsync(@"..\InfraStructure\Ecommerce_G02.Presistence\Data\types.json");
                var product_type = JsonSerializer.Deserialize<List<ProductType>>(producttypeData);

                if (product_type != null && product_type.Any())
                {
                    _context.ProductTypes.AddRange(product_type);
                }
            }

            ////product
            if (!_context.Products.Any())
            {
                var productData = await File.ReadAllTextAsync(@"..\InfraStructure\Ecommerce_G02.Presistence\Data\products.json");
                var productD = JsonSerializer.Deserialize<List<Product>>(productData);

                if (productD != null && productD.Any())
                {
                    _context.Products.AddRange(productD);
                }
            }

            /////// Delievery Methods
            if (!_context.DeliveryMethods.Any())

            {
                var deliveryData = await File.ReadAllTextAsync(@"..\InfraStructure\Ecommerce_G02.Presistence\Data\delivery.json");
                var DeliveryMethodsData = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                if (DeliveryMethodsData != null && DeliveryMethodsData.Any())
                {
                    _context.DeliveryMethods.AddRange(DeliveryMethodsData);
                }
            }
            _context.SaveChanges();

        }
        #endregion


        // For Identity Data Seed
        #region Identity Data Seeding
        public async Task IdentityDataSeedingAsync()
        {

            //For Rolles
            if (!rollmanger.Roles.Any())
            {
                await rollmanger.CreateAsync(new IdentityRole("Admin"));
                await rollmanger.CreateAsync(new IdentityRole("SuperAdmin"));

               // await identitycontext.SaveChangesAsync();
            }
            // For Users

            if (!usermanager.Users.Any())
                {
                    var user1 = new ApplicationUser()
                    {
                        Email = "Wageeh@yahoo.com",
                        DisplayName = "Wageeh Younan",
                        UserName = "WageehYounan",
                        PhoneNumber = "122335533"

                    };

                    var user2 = new ApplicationUser()
                    {
                        Email = "samir@yahoo.com",
                        DisplayName = "samir saeyd",
                        UserName = "samirsaeyd",
                        PhoneNumber = "020244755"

                    };
                // Assign passord to users
                await usermanager.CreateAsync(user1,"P@ssw0rd");
                await usermanager.CreateAsync(user2,"P@ssw0rd");
                // await identitycontext.SaveChangesAsync();

                ////assign rolles to users
                await usermanager.AddToRoleAsync(user1, "Admin");
                await usermanager.AddToRoleAsync(user2, "SuperAdmin");


            }
           await  identitycontext.SaveChangesAsync();


            #endregion
        }
    }
}
