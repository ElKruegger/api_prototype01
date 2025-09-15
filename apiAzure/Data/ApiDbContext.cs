using Microsoft.EntityFrameworkCore;
using apiAzure.Models;

namespace apiAzure.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        public DbSet<Person> People => Set<Person>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("user_infos"); // usar sua tabela existente
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).HasColumnName("id"); // <-- faltava isto
                entity.Property(p => p.FirstName).HasColumnName("first_name").HasMaxLength(50).IsRequired();
                entity.Property(p => p.LastName).HasColumnName("last_name").HasMaxLength(50).IsRequired();
                entity.Property(p => p.Email).HasColumnName("email").HasMaxLength(100).IsRequired();
                entity.Property(p => p.Gender).HasColumnName("gender").HasMaxLength(50);
                entity.Property(p => p.IpAddress).HasColumnName("ip_address").HasMaxLength(50);
                entity.Property(p => p.HouseAddress).HasColumnName("house_address").HasMaxLength(100);
            });
        }
    }
}


