using Microsoft.EntityFrameworkCore;

using AdventureWorks.Models;

namespace AdventureWorks.Repositories.EntityConfig
{
    public class ProductPhotoMap
    {
        public ProductPhotoMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductPhoto>(entity =>
            {
                entity.ToTable("ProductPhoto", "Production");

                entity.Property(e => e.ProductPhotoId).HasColumnName("ProductPhotoID");

                entity.Property(e => e.LargePhotoFileName).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ThumbnailPhotoFileName).HasMaxLength(50);
            });
        }
    }
}
