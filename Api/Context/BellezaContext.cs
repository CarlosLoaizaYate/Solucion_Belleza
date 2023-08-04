using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Context
{
    public class BellezaContext : DbContext
    {
        public DbSet<Product> products { get; set; }
        public DbSet<Order> orders { get; set; }

        public BellezaContext(DbContextOptions<BellezaContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Product> producsInit = new List<Product>();
            producsInit.Add(new Product() { ProductId = Guid.Parse("663c22f2-535d-4d9f-b548-8fc26e821bcd"), Name = "Pestañina", Description = "Pestañina contra agua", CreationDate = DateTime.Now });
            producsInit.Add(new Product() { ProductId = Guid.Parse("2c5370c2-5cb9-4b82-b475-8eb295cb1283"), Name = "lapiz labial", Description = "No se transfiere a ninguna superficie", CreationDate = DateTime.Now });

            modelBuilder.Entity<Product>(product =>
            {
                product.ToTable("Product");
                product.HasKey(p => p.ProductId);
                product.Property(p => p.Name).IsRequired().HasMaxLength(150);
                product.Property(p => p.Description).IsRequired(false);
                product.Property(p => p.CreationDate);
                product.HasData(producsInit);
            });

            List<Order> ordersInit = new List<Order>();
            ordersInit.Add(new Order() { OrderId = Guid.Parse("374f97eb-c2b6-4801-aaf4-467b0cf0897c"), ProducId = Guid.Parse("663c22f2-535d-4d9f-b548-8fc26e821bcd"), Quantity = 2, CreationDate = DateTime.Now });
            ordersInit.Add(new Order() { OrderId = Guid.Parse("374f97eb-c2b6-4801-aaf4-467b0cf0898c"), ProducId = Guid.Parse("2c5370c2-5cb9-4b82-b475-8eb295cb1283"), Quantity = 3, CreationDate = DateTime.Now });
            modelBuilder.Entity<Order>(order =>
            {
                order.ToTable("Order");
                order.HasKey(p => p.OrderId);
                order.HasOne(p => p.Product).WithMany(p => p.Orders).HasForeignKey(p => p.ProducId);
                order.Property(p => p.Quantity).IsRequired();
                order.Property(p => p.CreationDate);
                order.HasData(ordersInit);
            });
        }

    }
}
