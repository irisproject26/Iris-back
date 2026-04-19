using Microsoft.EntityFrameworkCore;
using IRIS_API.Models; 

namespace IRIS_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Visita> Visitas { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Report>()
                .Property(r => r.Urgency)
                .HasConversion<string>();
        }
    }
}