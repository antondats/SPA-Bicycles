using Microsoft.EntityFrameworkCore;

using AdventureWorks.Repositories.EntityConfig;



namespace AdventureWorks.Repositories
{
    public partial class AWContext : DbContext
    {
        public AWContext(DbContextOptions<AWContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ProductMap(modelBuilder);

            new ProductSubCategoryMap(modelBuilder);

            new ProductCategoryMap(modelBuilder);

            new SalesOrderDetailMap(modelBuilder);

            new ProductModelMap(modelBuilder);

            new ProductModelProductDescriptionCultureMap(modelBuilder);

            new ProductDescriptionMap(modelBuilder);

            new ProductProductPhotoMap(modelBuilder);

            new ProductPhotoMap(modelBuilder);

            new CultureMap(modelBuilder);

            new BillOfMaterialsMap(modelBuilder);

            new ProductCostHistoryMap(modelBuilder);

            new ProductInventoryMap(modelBuilder);

            new ProductListPriceHistoryMap(modelBuilder);

            new ProductReviewMap(modelBuilder);

            new ProductVendorMap(modelBuilder);

            new PurchaseOrderDetailMap(modelBuilder);

            new ShoppingCartItemMap(modelBuilder);

            new SpecialOfferProductMap(modelBuilder);

            new TransactionHistoryMap(modelBuilder);

            new WorkOrderMap(modelBuilder);

            new WorkOrderRoutingMap(modelBuilder);
        }
    }
}
