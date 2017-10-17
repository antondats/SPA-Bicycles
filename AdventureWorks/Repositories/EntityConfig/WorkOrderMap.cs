using Microsoft.EntityFrameworkCore;

using AdventureWorks.Models.Additional_models;

namespace AdventureWorks.Repositories.EntityConfig
{
    public class WorkOrderMap
    {
        public WorkOrderMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkOrder>(entity =>
            {
                entity.ToTable("WorkOrder", "Production");

                entity.HasIndex(e => e.ProductId);

                entity.HasIndex(e => e.ScrapReasonId);

                entity.Property(e => e.WorkOrderId).HasColumnName("WorkOrderID");

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ScrapReasonId).HasColumnName("ScrapReasonID");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.StockedQty).HasComputedColumnSql("(isnull([OrderQty]-[ScrappedQty],(0)))");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.WorkOrder)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                //entity.HasOne(d => d.ScrapReason)
                //    .WithMany(p => p.WorkOrder)
                //    .HasForeignKey(d => d.ScrapReasonId);
            });
        }
    }
}
