using DEADLOCK.APP.Models;
using Microsoft.EntityFrameworkCore;

namespace DEADLOCK.APP.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<FacturaItem> FacturaItems { get; set; }


    }
}
