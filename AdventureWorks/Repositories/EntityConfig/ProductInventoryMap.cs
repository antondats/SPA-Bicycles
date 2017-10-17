using Microsoft.EntityFrameworkCore;

using AdventureWorks.Models.Additional_models;

namespace AdventureWorks.Repositories.EntityConfig
{
    public class ProductInventoryMap
    {
        public ProductInventoryMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductInventory>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.LocationId });

                entity.ToTable("ProductInventory", "Production");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((0))");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Shelf)
                    .IsRequired()
                    .HasMaxLength(10).HasColumnType("nvarchar(10)");

                //entity.HasOne(d => d.Location)
                //    .WithMany(p => p.ProductInventory)
                //    .HasForeignKey(d => d.LocationId)
                //    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductInventory)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
