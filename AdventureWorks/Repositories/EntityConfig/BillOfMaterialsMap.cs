using Microsoft.EntityFrameworkCore;

using AdventureWorks.Models.Additional_models;

namespace AdventureWorks.Repositories.EntityConfig
{
    public class BillOfMaterialsMap
    {
        public BillOfMaterialsMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BillOfMaterials>(entity =>
            {
                entity.ToTable("BillOfMaterials", "Production");

                entity.HasIndex(e => e.UnitMeasureCode);

                entity.HasIndex(e => new { e.ProductAssemblyId, e.ComponentId, e.StartDate })
                    .HasName("AK_BillOfMaterials_ProductAssemblyID_ComponentID_StartDate")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.BillOfMaterialsId).HasColumnName("BillOfMaterialsID");

                entity.Property(e => e.Bomlevel).HasColumnName("BOMLevel");

                entity.Property(e => e.ComponentId).HasColumnName("ComponentID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PerAssemblyQty)
                    .HasColumnType("decimal(8, 2)")
                    .HasDefaultValueSql("((1.00))");

                entity.Property(e => e.ProductAssemblyId).HasColumnName("ProductAssemblyID");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UnitMeasureCode)
                    .IsRequired()
                    .HasColumnType("nchar(3)");

                entity.HasOne(d => d.Component)
                    .WithMany(p => p.BillOfMaterialsComponent)
                    .HasForeignKey(d => d.ComponentId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.ProductAssembly)
                    .WithMany(p => p.BillOfMaterialsProductAssembly)
                    .HasForeignKey(d => d.ProductAssemblyId).OnDelete(DeleteBehavior.SetNull);

                //entity.HasOne(d => d.UnitMeasureCodeNavigation)
                //    .WithMany(p => p.BillOfMaterials)
                //    .HasForeignKey(d => d.UnitMeasureCode)
                //    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
