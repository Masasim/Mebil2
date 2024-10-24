namespace Mebil2.Data
{
    using Mebil2.Models;
    using Microsoft.EntityFrameworkCore;

    public class MebilContext : DbContext
    {
        public MebilContext(DbContextOptions<MebilContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; } // Changed to the Ukrainian table name
        public DbSet<Warehouse> Warehouses { get; set; } // Assuming the class name for Warehouse is Склад
        public DbSet<Factory> Factories { get; set; } // Assuming the class name for Factory is Фабрsка
        public DbSet<Order> Orders { get; set; } // Changed to the Ukrainian table name
        public DbSet<Furniture> Furnitures { get; set; } // Assuming the class name for Furniture is Меблі
        public DbSet<FurnitureInWarehouse> FurnitureInWarehouses { get; set; } // Assuming the class name for FurnitureInWarehouse is Меблі_на_складі
        public DbSet<FurnitureInOrder> FurnitureInOrders { get; set; } // Assuming the class name for FurnitureInOrder is Меблі_в_замовленні
        public DbSet<Invoice> Invoices { get; set; } // Assuming the class name for Invoice is Накладна

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ensure each entity has a primary key
            modelBuilder.Entity<Customer>().HasKey(c => c.id); // Adjust the key property accordingly
            modelBuilder.Entity<Warehouse>().HasKey(w => w.id); // Adjust the key property accordingly
            modelBuilder.Entity<Factory>().HasKey(f => f.id); // Adjust the key property accordingly
            modelBuilder.Entity<Order>().HasKey(o => o.id); // Adjust the key property accordingly
            modelBuilder.Entity<Furniture>().HasKey(f => f.id); // Adjust the key property accordingly
            //modelBuilder.Entity<Меблі_на_складі>().HasKey(m => m.id); // Adjust the key property accordingly
            //modelBuilder.Entity<Меблі_в_замовленні>().HasKey(m => m.id); // Adjust the key property accordingly
            modelBuilder.Entity<Invoice>().HasKey(i => i.id); // Adjust the key property accordingly

            // If any entities are keyless, uncomment the lines below
            modelBuilder.Entity<FurnitureInOrder>().HasNoKey();
            modelBuilder.Entity<FurnitureInWarehouse>().HasNoKey();

            base.OnModelCreating(modelBuilder);
        }
    }
}
