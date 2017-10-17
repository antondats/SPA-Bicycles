using Microsoft.EntityFrameworkCore;

using AdventureWorks.Models.Additional_models;

namespace AdventureWorks.Repositories.EntityConfig
{
    public class ProductReviewMap
    {
        public ProductReviewMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductReview>(entity =>
            {
                entity.ToTable("ProductReview", "Production");

                entity.HasIndex(e => new { e.Comments, e.ProductId, e.ReviewerName })
                    .HasName("IX_ProductReview_ProductID_Name");

                entity.Property(e => e.ProductReviewId).HasColumnName("ProductReviewID");

                entity.Property(e => e.Comments).HasMaxLength(3850).HasColumnType("nvarchar(3850)");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(50).HasColumnType("nvarchar(50)");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ReviewDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ReviewerName)
                    .IsRequired()
                    .HasColumnType("Name");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductReview)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
