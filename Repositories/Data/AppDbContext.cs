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
            modelBuilder.Entity<Ordenes>().ToTable("Ordenes");
            modelBuilder.Entity<Activos>().ToTable("Activos");
        }

    }
}
