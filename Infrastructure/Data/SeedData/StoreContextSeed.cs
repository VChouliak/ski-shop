using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data.SeedData
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), "Infrastructure", "Data", "SeedData", "brands.json"));
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    brands.ForEach(brand => context.ProductBrands.Add(brand));

                    await context.SaveChangesAsync();
                }

                if (!context.ProductTypes.Any())
                {
                    var typessData = File.ReadAllText(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), "Infrastructure", "Data", "SeedData", "types.json"));
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typessData);

                    types.ForEach(type => context.ProductTypes.Add(type));

                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), "Infrastructure", "Data", "SeedData", "products.json"));
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    products.ForEach(product => context.Products.Add(product));

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}