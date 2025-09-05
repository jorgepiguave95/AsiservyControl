using Microsoft.EntityFrameworkCore;
using Products.Domain.Entities;

namespace Products.Infraestructure.Persistence
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options)
            : base(options) { }

        public DbSet<ProductControl> ProductControls { get; set; }
        public DbSet<ProductControlDetail> ProductControlDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ProductControl
            modelBuilder.Entity<ProductControl>(entity =>
            {
                entity.ToTable("ProductControls");

                entity.HasKey(pc => pc.Id);

                entity.Property(pc => pc.Fecha)
                      .IsRequired();

                entity.Property(pc => pc.Producto)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(pc => pc.NombreCliente)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(pc => pc.Marca)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(pc => pc.PorcentajeMiga)
                      .HasColumnType("decimal(5,2)")
                      .IsRequired();

                entity.Property(pc => pc.PesoDrenado)
                      .HasColumnType("decimal(18,3)")
                      .IsRequired();

                entity.Property(pc => pc.PesoEnvase)
                      .HasColumnType("decimal(18,3)")
                      .IsRequired();

                entity.Property(pc => pc.EstaActivo)
                      .IsRequired();

                // VO: PesoNeto 
                entity.OwnsOne(pc => pc.PesoNeto, vo =>
                {
                    vo.Property(p => p.Value)
                      .HasColumnName("PesoNeto")
                      .HasColumnType("decimal(18,3)")
                      .IsRequired();
                });

                // Relación 1..N con detalles
                entity.HasMany(pc => pc.Detalles)
                      .WithOne()
                      .HasForeignKey(d => d.ProductControlId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ProductControlDetail
            modelBuilder.Entity<ProductControlDetail>(entity =>
            {
                entity.ToTable("ProductControlDetails");

                entity.HasKey(d => d.Id);

                entity.Property(d => d.ProductControlId)
                      .IsRequired();

                entity.Property(d => d.Fecha)
                      .IsRequired();

                entity.Property(d => d.Peso)
                      .HasColumnType("decimal(18,3)")
                      .IsRequired();

                // VO: TipoControl 
                entity.OwnsOne(d => d.TipoControl, vo =>
                {
                    vo.Property(p => p.Value)
                      .HasColumnName("TipoControl")
                      .HasMaxLength(100)
                      .IsRequired();
                });
            });
        }
    }
}
