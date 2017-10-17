using System.Collections.Generic;

using AdventureWorks.Models;


namespace AdventureWorks.Abstract
{
    public interface IAWRepository
    {
        IEnumerable<ModelForProductDetail> GetAllProductsDetail();
        IEnumerable<ModelForProductsList> GetTopPopularProducts();
        ModelForProductDetail GetProductDetail(int id);
        bool AddProduct(ModelForProductDetail product);
        bool DeleteProduct(int id);
        IEnumerable<ModelForProductsList> SearchProduct(string detail);
    }
}
