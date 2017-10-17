using Microsoft.EntityFrameworkCore;

using AdventureWorks.Models.Additional_models;

namespace AdventureWorks.Repositories.EntityConfig
{
    public class ProductListPriceHistoryMap
    {
        public ProductListPriceHistoryMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductListPriceHistory>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.StartDate });

                entity.ToTable("ProductListPriceHistory", "Production");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ListPrice).HasColumnType("money");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductListPriceHistory)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
