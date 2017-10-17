using Microsoft.EntityFrameworkCore;

using AdventureWorks.Models.Additional_models;

namespace AdventureWorks.Repositories.EntityConfig
{
    public class ProductVendorMap
    {
        public ProductVendorMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductVendor>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.BusinessEntityId });

                entity.ToTable("ProductVendor", "Purchasing");

                entity.HasIndex(e => e.BusinessEntityId);

                entity.HasIndex(e => e.UnitMeasureCode);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");

                entity.Property(e => e.LastReceiptCost).HasColumnType("money");

                entity.Property(e => e.LastReceiptDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StandardPrice).HasColumnType("money");

                entity.Property(e => e.UnitMeasureCode)
                    .IsRequired()
                    .HasColumnType("nchar(3)");

                //entity.HasOne(d => d.BusinessEntity)
                //    .WithMany(p => p.ProductVendor)
                //    .HasForeignKey(d => d.BusinessEntityId)
                //    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductVendor)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                //entity.HasOne(d => d.UnitMeasureCodeNavigation)
                //    .WithMany(p => p.ProductVendor)
                //    .HasForeignKey(d => d.UnitMeasureCode)
                //    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
