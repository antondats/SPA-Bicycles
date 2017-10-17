using Microsoft.EntityFrameworkCore;

using AdventureWorks.Models;

namespace AdventureWorks.Repositories.EntityConfig
{
    public class SalesOrderDetailMap
    {
        public SalesOrderDetailMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesOrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.SalesOrderId, e.SalesOrderDetailId });

                entity.ToTable("SalesOrderDetail", "Sales");

                entity.HasIndex(e => e.ProductId);

                entity.HasIndex(e => e.Rowguid)
                    .HasName("AK_SalesOrderDetail_rowguid")
                    .IsUnique();

                entity.Property(e => e.SalesOrderId).HasColumnName("SalesOrderID");

                entity.Property(e => e.SalesOrderDetailId)
                    .HasColumnName("SalesOrderDetailID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CarrierTrackingNumber).HasMaxLength(25).HasColumnType("nvarchar(25)");

                entity.Property(e => e.LineTotal)
                    .HasColumnType("numeric(, 6)")
                    .HasComputedColumnSql("(isnull(([UnitPrice]*((1.0)-[UnitPriceDiscount]))*[OrderQty],(0.0)))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.SpecialOfferId).HasColumnName("SpecialOfferID");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.Property(e => e.UnitPriceDiscount)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.0))");
            });
        }
    }
}
