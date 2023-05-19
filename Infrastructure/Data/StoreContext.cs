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

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(StoreSetting.Instance.SqliteConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (StoreSetting.Instance.DatabaseProvider.ToLower().EndsWith("sqlite"))
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(property => property.PropertyType == typeof(decimal));

                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }
                }
            }

        }
    }
}