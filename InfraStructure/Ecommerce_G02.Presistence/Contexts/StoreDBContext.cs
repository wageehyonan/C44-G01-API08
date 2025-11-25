using Ecommerce_G02.Domain.Models.Orders;
using Ecommerce_G02.Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Presistence.Contexts
{
    public class StoreDBContext:DbContext
    {
        public StoreDBContext(DbContextOptions<StoreDBContext> options):base(options)
        { 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDBContext).Assembly);

           
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand>  ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    }
}
