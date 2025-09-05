using Customers.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customers.Infraestructure.Persistence
{
    public class CustomersDbContext : DbContext
    {
        public CustomersDbContext(DbContextOptions<CustomersDbContext> options)
            : base(options) { }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.FirstName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(p => p.LastName)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                // Value Object: Email
                entity.OwnsOne(c => c.Email, email =>
                {
                    email.Property(e => e.Value)
                         .HasColumnName("Email")
                         .IsRequired()
                         .HasMaxLength(200);
                });

                // Value Object: PhoneNumber
                entity.OwnsOne(c => c.Phone, phone =>
                {
                    phone.Property(p => p.Value)
                         .HasColumnName("PhoneNumber")
                         .IsRequired()
                         .HasMaxLength(15);
                });
            });
        }
    }
}
