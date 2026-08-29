using DBFirst.DataAccessLayer;
using DBFirst.DataAccessLayer.Models;
using System.Linq;

namespace DBFirst.ConsoleApp
{
    internal class Program
    {
        static QuickKartDbContext _context;
        static IQuickKartRepository repository;
        static Program()
        {
            _context = new QuickKartDbContext();
            repository = new QuickKartRepository(_context);
        }
        static void Main(string[] args)
        {
            //var categorie = repository.GetAllCategories();
            //foreach (var category in categorie)
            //{
            //    Console.WriteLine(category.CategoryId + " " + category.CategoryName);
            //}

            //byte categoryId = 2;
            //List<Product> lstProducts = repository.GetProductsOnCategory(categoryId);
            //if (lstProducts.Count == 0)
            //{
            //    Console.WriteLine("no Products available under the category=" + categoryId);
            //}
            //else
            //{
            //    foreach (var product in lstProducts)
            //    {
            //        Console.WriteLine(product.ProductName + " " + product.CategoryId + " " + product.Price + " " + product.QuantityAvailable);
            //    }
            //}

            //Product product=repository.FilterProducts(categoryId);
            //if (product == null)
            //{
            //    Console.WriteLine("no Products available under the category=" + categoryId);
            //}
            //else {
            //    Console.WriteLine(product.ProductName + " " + product.CategoryId + " " + product.Price + " " + product.QuantityAvailable);
            //}

            string pattern = "BMW%";
            List<Product> lstProduct = repository.FilterProductUsingLike(pattern);
            if (lstProduct.Count == 0)
            {
                Console.WriteLine("No products are available with the= " + pattern);
            }
            else
            {
                foreach (var product in lstProduct)
                {
                    Console.WriteLine(product.ProductName + " " + product.CategoryId + " " + product.Price + " " + product.QuantityAvailable);
                }
            }
            //bool result = repository.AddCategory("Books");
            //if (result)
            //{
            //    Console.WriteLine("New Category added successfully");
            //}
            //else
            //{
            //    Console.WriteLine("Something went wrong. try again");
            //}

            //bool result = repository.UpdateCategoryUsing(8, "Book");
            //if (result)
            //{
            //    Console.WriteLine("Category details updated successfully");
            //}
            //else
            //{
            //    Console.WriteLine("Something went wrong. Try again");
            //}

            //bool status = repository.DeleteCategory(11);
            //if (status)
            //{
            //    Console.WriteLine("Deleted successfully category");
            //}
            //else
            //{
            //    Console.WriteLine("some error found while deletion");
            //}

            //byte catId = 0;
            //int returnResult = repository.AddCategoryUsingUSP("Footwear", out catId);
            //if (returnResult > 0)
            //{
            //    Console.WriteLine("Category Added successfully with catId=" + catId);
            //}
            //else
            //{
            //    Console.WriteLine("some error occured");
            //}

            //byte cateId = 7;
            //var ProductsCat = repository.GetProductCategoryUsingTVF(cateId);
            //if (ProductsCat == null || ProductsCat.Count == 0)
            //{
            //    Console.WriteLine("No product avaialble under given category ID");
            //}
            //else
            //{
            //    foreach (var item in ProductsCat) { 
            //        Console.WriteLine(item.ProductName+" "+item.ProductId+" "+item.CategoryName+" "+item.Price);
            //    }
            //}

            //string prodId = repository.GenerateNewProductId();
            //Console.WriteLine("ProductId is:"+prodId);

            //bool result = repository.CheckEmailId("Anabela@gmail.com");
            //if (result) {
            //    Console.WriteLine("Email can be used to register");
            //}
            //else
            //{
            //    Console.WriteLine("allready exist");
            //}
        }


    }
}
