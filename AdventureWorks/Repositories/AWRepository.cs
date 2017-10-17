using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using AdventureWorks.Abstract;
using AdventureWorks.Models;
using AdventureWorks.Models.Additional_models;

namespace AdventureWorks.Repositories
{
    public class AWRepository : IAWRepository
    {
        private AWContext context;
        private DbSet<Product> productEntity;
        private DbSet<SalesOrderDetail> salesOrderDetail;
        private DbSet<ProductModelProductDescriptionCulture> modelDescription;
        private DbSet<ProductProductPhoto> productPhoto;
        private DbSet<BillOfMaterials> billOfMaterials;
        private DbSet<ProductCostHistory> productCostHistory;
        private DbSet<ProductInventory> productInventory;
        private DbSet<ProductListPriceHistory> productListPriceHistory;
        private DbSet<ProductReview> productReview;
        private DbSet<ProductVendor> productVendor;
        private DbSet<PurchaseOrderDetail> purchaseOrderDetail;
        private DbSet<SpecialOfferProduct> specialOfferProduct;
        private DbSet<TransactionHistory> transactionHistory;
        private DbSet<WorkOrder> workOrder;
        private DbSet<WorkOrderRouting> workOrderRouting;
        private DbSet<ProductModel> productModel;
        private DbSet<ProductDescription> productDescription;


        public AWRepository(AWContext context)
        {
            this.context = context;
            productEntity = context.Set<Product>();
            salesOrderDetail = context.Set<SalesOrderDetail>();
            modelDescription = context.Set<ProductModelProductDescriptionCulture>();
            productPhoto = context.Set<ProductProductPhoto>();
            billOfMaterials = context.Set<BillOfMaterials>();
            productCostHistory = context.Set<ProductCostHistory>();
            productInventory = context.Set<ProductInventory>();
            productListPriceHistory = context.Set<ProductListPriceHistory>();
            productReview = context.Set<ProductReview>();
            productVendor = context.Set<ProductVendor>();
            purchaseOrderDetail = context.Set<PurchaseOrderDetail>();
            specialOfferProduct = context.Set<SpecialOfferProduct>();
            transactionHistory = context.Set<TransactionHistory>();
            workOrder = context.Set<WorkOrder>();
            workOrderRouting = context.Set<WorkOrderRouting>();
            productModel = context.Set<ProductModel>();
            productDescription = context.Set<ProductDescription>();
        }

        public IEnumerable<ModelForProductDetail> GetAllProductsDetail()
        {
            var allProducts = productEntity.Include(p => p.ProductSubcategory).ThenInclude(sc => sc.ProductCategory)
                .Where(p => p.ProductSubcategory.ProductCategory.Name == "Bikes")
                .Join(modelDescription.Include(md => md.ProductDescription).Include(md => md.ProductModel).Include(md => md.Culture).Where(md => md.Culture.Name == "English"), p => p.ProductModel.ProductModelId, md => md.ProductModelId, (p, md) =>
                    new ModelForProductDetail
                    {
                        ProductId = p.ProductId,
                        Name = p.Name,
                        Model = p.ProductModel.Name,
                        Color = p.Color,
                        ListPrice = p.ListPrice,
                        ProductNumber = p.ProductNumber,
                        Description = md.ProductDescription.Description,
                        Size = p.Size,
                        Weight = p.Weight,
                        Category = p.ProductSubcategory.Name,
                        CategoryId = p.ProductSubcategory.ProductCategoryId
                    }).ToList();

            return allProducts.AsEnumerable();
        }

        public ModelForProductDetail GetProductDetail(int id)
        {
            return GetAllProductsDetail().Where(p => p.ProductId == id).FirstOrDefault();
        }

        public IEnumerable<ModelForProductsList> GetTopPopularProducts()
        {
            var topFiveBikes = GetAllProductsDetail()
                .Join(salesOrderDetail, p => p.ProductId, s => s.ProductId, (p, s) =>
                new {
                    Item = p,
                    Quantity = s.OrderQty
                }).GroupBy(s => s.Item.Name).OrderByDescending(o => o.Sum(s => s.Quantity)).Take(5).ToList();

            List<ModelForProductsList> bikes = new List<ModelForProductsList>();
            foreach (var item in topFiveBikes)
            {
                bikes.Add(item.ToList()[0].Item);
            }

            return bikes.AsEnumerable();
        }

        private int AddModel(string name)
        {
            ProductModel model = new ProductModel
            {
                Name = name,
                Rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.Now
            };

            context.Entry(model).State = EntityState.Added;
            context.SaveChanges();

            return (productModel.Where(p => p.Name == name).FirstOrDefault()).ProductModelId;
        }

        private int AddDescription(string desc)
        {
            ProductDescription description = new ProductDescription
            {
                Description = desc,
                Rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.Now
            };

            context.Entry(description).State = EntityState.Added;
            context.SaveChanges();

            return (productDescription.Where(p => p.Description == desc).FirstOrDefault()).ProductDescriptionId;
        }

        private void AddProductModelDescription(int model, int description)
        {
            ProductModelProductDescriptionCulture modeldescription = new ProductModelProductDescriptionCulture
            {
                ProductModelId = model,
                ProductDescriptionId = description,
                CultureId = "en",
                ModifiedDate = DateTime.Now
            };

            context.Entry(modeldescription).State = EntityState.Added;
            context.SaveChanges();
        }

        public bool AddProduct(ModelForProductDetail product)
        {
            try
            {
                int modelId = AddModel(product.Model);
                int descId = AddDescription(product.Description);
                AddProductModelDescription(modelId, descId);
                Product addedProduct = new Product()
                {
                    Name = product.Name,
                    ProductNumber = product.ProductNumber,
                    ProductSubcategoryId = product.CategoryId,
                    MakeFlag = true,
                    FinishedGoodsFlag = true,
                    Color = product.Color,
                    SafetyStockLevel = 10,
                    ReorderPoint = 10,
                    StandardCost = product.ListPrice,
                    ListPrice = product.ListPrice,
                    Size = product.Size,
                    Weight = product.Weight,
                    SellStartDate = DateTime.Now,
                    Rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now,
                    ProductModelId = modelId
                };

                context.Entry(addedProduct).State = EntityState.Added;
                context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                Product product = productEntity.FirstOrDefault(p => p.ProductId == id);
                if (product != null)
                {
                    billOfMaterials.RemoveRange(billOfMaterials.Where(b => b.ProductAssemblyId == id));
                    billOfMaterials.RemoveRange(billOfMaterials.Where(b => b.ComponentId == id));
                    productCostHistory.RemoveRange(productCostHistory.Where(p => p.ProductId == id));
                    productInventory.RemoveRange(productInventory.Where(p => p.ProductId == id));
                    productListPriceHistory.RemoveRange(productListPriceHistory.Where(p => p.ProductId == id));
                    productReview.RemoveRange(productReview.Where(p => p.ProductId == id));
                    productVendor.RemoveRange(productVendor.Where(p => p.ProductId == id));
                    purchaseOrderDetail.RemoveRange(purchaseOrderDetail.Where(p => p.ProductId == id));
                    productPhoto.RemoveRange(productPhoto.Where(p => p.ProductId == id));
                    transactionHistory.RemoveRange(transactionHistory.Where(p => p.ProductId == id));
                    workOrderRouting.RemoveRange(workOrderRouting.Where(p => p.ProductId == id));
                    workOrder.RemoveRange(workOrder.Where(p => p.ProductId == id));
                    var sop = specialOfferProduct.Where(p => p.ProductId == id);
                    foreach (var item in sop)
                    {
                        salesOrderDetail.RemoveRange(salesOrderDetail.Where(s => s.ProductId == id).Where(s => s.SpecialOfferId == item.SpecialOfferId));
                        specialOfferProduct.Remove(item);
                    }
 
                    productEntity.Remove(product);
                }

                context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<ModelForProductsList> SearchProduct(string detail)
        {
            detail = detail.Trim(new char[] { '!', '?', '.', '*', ';', '#', '$', '%', '^', ' ', '_' });
            var products = GetAllProductsDetail().Where(p => p.Name.IndexOf(detail, StringComparison.OrdinalIgnoreCase) >= 0 
            || p.Description.IndexOf(detail, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            return products.AsEnumerable();
        }
    }
}