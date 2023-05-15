using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Settings;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext()
        {
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {           
            options.UseSqlite(StoreSetting.Instance.SqliteConnectionString);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
    }
}