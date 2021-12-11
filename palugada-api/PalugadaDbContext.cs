using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using palugada_api.Entities;

namespace palugada_api {
    public sealed class PalugadaDbContext : DbContext {
        public DbSet<User> User { get; set; }
        public DbSet<OrderHeader> OrderHeader { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Menu> Menu { get; set; }

        public PalugadaDbContext() {
            Database.EnsureCreated();
            Database.Migrate();
        }

        public PalugadaDbContext(DbContextOptions<PalugadaDbContext> options)
            : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // Ini buat nge-scan Assembly 
            // untuk apply konfigurasi IEntityTypeConfiguration<T>
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
