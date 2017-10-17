using Microsoft.EntityFrameworkCore;

using AdventureWorks.Models.Additional_models;

namespace AdventureWorks.Repositories.EntityConfig
{
    public class WorkOrderRoutingMap
    {
        public WorkOrderRoutingMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkOrderRouting>(entity =>
            {
                entity.HasKey(e => new { e.WorkOrderId, e.ProductId, e.OperationSequence });

                entity.ToTable("WorkOrderRouting", "Production");

                entity.HasIndex(e => e.ProductId);

                entity.Property(e => e.WorkOrderId).HasColumnName("WorkOrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ActualCost).HasColumnType("money");

                entity.Property(e => e.ActualEndDate).HasColumnType("datetime");

                entity.Property(e => e.ActualResourceHrs).HasColumnType("decimal(9, 4)");

                entity.Property(e => e.ActualStartDate).HasColumnType("datetime");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PlannedCost).HasColumnType("money");

                entity.Property(e => e.ScheduledEndDate).HasColumnType("datetime");

                entity.Property(e => e.ScheduledStartDate).HasColumnType("datetime");

                //entity.HasOne(d => d.Location)
                //    .WithMany(p => p.WorkOrderRouting)
                //    .HasForeignKey(d => d.LocationId)
                //    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.WorkOrder)
                    .WithMany(p => p.WorkOrderRouting)
                    .HasForeignKey(d => d.WorkOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
