using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.DataAccess.Data
{
    public static class PrepareDb
    {

        public static void Initialize(CatalogDbContext context)
        {
            //Test amaçlı:
            context.Database.Migrate();

            SeedCategories(context);
            SeedProducts(context);


        }

        private static void SeedCategories(CatalogDbContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                 {
                      new Category {  Name="Elektronik"},
                      new Category { Name="Giyim"},

                 };

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }
        }

        public static void SeedProducts(CatalogDbContext context)
        {
            if (!context.Products.Any())
            {
                var products = new List<Product>
                 {
                      new Product {  Name="Ses Sistemi", CategoryId=1, Description="Açıklama 1", Price=500},
                      new Product { Name="Tişört", CategoryId=2, Description="Açıklama 2", Price=50}

                 };

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}
