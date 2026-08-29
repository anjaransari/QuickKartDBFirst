using System;
using DBFirst.DataAccessLayer.Models;

namespace DBFirst.DataAccessLayer
{
    public interface IQuickKartRepository
    {
        List<Category> GetAllCategories();
        List<Product> GetProductsOnCategory(byte categoryId);
        Product FilterProducts(byte categoryId);
        List<Product> FilterProductUsingLike(string pattern);
        bool AddCategory(string catName);
        bool UpdateCategory(byte categoryId, string catName);
        bool DeleteCategory(byte categoryId);
        bool UpdateCategoryUsing(byte categoryId, string catName);
        int AddCategoryUsingUSP(string catName, out byte catId);
        List<ProductCategory> GetProductCategoryUsingTVF(byte catId);
        string GenerateNewProductId();
        bool CheckEmailId(string emialId);
    }
}
