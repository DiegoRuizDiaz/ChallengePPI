using Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Ordenes> Ordenes { get; set; }
        public DbSet<Activos> Activos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ordenes>().ToTable("Ordenes")
                .Property(e => e.Precio).HasColumnType("decimal(18,4)");

            modelBuilder.Entity<Ordenes>().ToTable("Ordenes")
                .Property(e => e.MontoTotal).HasColumnType("decimal(18,4)");

            modelBuilder.Entity<Activos>().ToTable("Activos")
                .Property(e=> e.PrecioUnitario).HasColumnType("decimal(18,4)");
        }

    }
}
