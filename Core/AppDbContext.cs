using Microsoft.EntityFrameworkCore;

namespace Core
{
    public class AppDbContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; } = null!;
        public DbSet<Factura> Facturas { get; set; } = null!;
        public DbSet<DetalleFactura> DetallesFactura { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-8TB9QBU;Database=SistemaVentas;Trusted_Connection=True;TrustServerCertificate=True;"
            );
        }
    }
}