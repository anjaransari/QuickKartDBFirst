using DBFirst.DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFirst.DataAccessLayer
{
    public class QuickKartRepository: IQuickKartRepository
    {
        private QuickKartDbContext context;
        public QuickKartRepository(QuickKartDbContext _context)
        {
            this.context = _context;
        }
        public List<Category> GetAllCategories()
        {
            var categoriesList=context.Categories.ToList();
            return categoriesList;
        }
        public List<Product> GetProductsOnCategory(byte categoryId)
        {
            List<Product> lstproducts = new List<Product>();
            try
            {
                lstproducts = context.Products.Where(p=>p.CategoryId==categoryId).ToList();
            }
            catch {
                lstproducts = null;
            }
            return lstproducts;
        }

        public Product FilterProducts(byte categoryId)
        {
            Product product = new Product();
            try
            {
                product = context.Products.Where(p => p.CategoryId == categoryId).FirstOrDefault();
            }
            catch
            {
                product = null; 
            }
            return product;
        }

        public List<Product> FilterProductUsingLike(string pattern)
        {
            List<Product> products = new List<Product>();
            try
            {
                products = context.Products.Where(p => EF.Functions.Like(p.ProductName, pattern)).ToList();
            }
            catch
            {
                products = null;
            }
            return products;
        }

        public bool AddCategory(string catName)
        {
            bool status = false;
            Category category = new Category();
            category.CategoryName = catName;
            try
            {
                context.Categories.Add(category);
                //context.Add<Category>(category);
                context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        public bool UpdateCategory(byte categoryId, string catName)
        {
            bool status = false;
            Category category = context.Categories.Find(categoryId);
            try
            {
                category.CategoryName = catName;
                context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }
        //AddRange and UpdateRange is used to add/update multiple row in table
        public bool UpdateCategoryUsing(byte categoryId, string catName)
        {
            bool status = false;
            Category category = context.Categories.Find(categoryId); //Find() work with Primary key only
            category.CategoryName = catName;
            try
            {
                using(var newContext=new QuickKartDbContext())
                {
                    newContext.Categories.Update(category);
                    newContext.SaveChanges();
                }
               
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        public bool DeleteCategory(byte categoryId)
        {
            Category category= new Category();
            bool status = false;
            try
            {
                category = context.Categories.Find(categoryId);
                if (category != null)
                {
                    context.Categories.Remove(category);//removeRange is used for multiple row deletion
                    context.SaveChanges();
                    status = true;
                }
                else
                {
                    status= false;
                }
            }
            catch (Exception ex) 
            { 
                status = false;
            }
            return status;
        }

        // stored procedure

        public int AddCategoryUsingUSP(string catName,out byte catId)
        {
            catId = 0;
            int noOfRowAffected = 0;
            int returnResult = 0;
            SqlParameter prmCatName = new SqlParameter("@CategoryName",catName);
            SqlParameter prmCatId = new SqlParameter("@CategoryId",System.Data.SqlDbType.TinyInt);
            prmCatId.Direction = System.Data.ParameterDirection.Output;
            SqlParameter prmReturn = new SqlParameter("@ReturnResult",System.Data.SqlDbType.Int);
            prmReturn.Direction = System.Data.ParameterDirection.Output;
            try
            {
                noOfRowAffected = context.Database.ExecuteSqlRaw("EXEC @ReturnResult=usp_AddCategory @CategoryName, @CategoryId out ", prmReturn, prmCatName, prmCatId);
                //context.Database.ExecuteSqlRaw($"INSERT INTO Categories Values({CategoryName})");  used for non query statements
                returnResult = Convert.ToInt32(prmReturn.Value);
                catId = Convert.ToByte(prmCatId.Value);
            }
            catch (Exception ex) {
                catId = 0;
                noOfRowAffected = -1;
                returnResult = -99;
            }
            return returnResult;
        }

        public List<ProductCategory> GetProductCategoryUsingTVF(byte catId)
        {
            List<ProductCategory> lstProductCat=new List<ProductCategory>();
            try
            {
                SqlParameter prmCatId = new SqlParameter("@CategoryId", catId);
                lstProductCat = context.ProductCategories.FromSqlRaw("SELECT * FROM ufn_GetProductCategoryDetails(@CategoryId)", prmCatId).ToList();
            }
            catch (Exception ex)
            {
                lstProductCat = null;
            }
            return lstProductCat;

        }

        public string GenerateNewProductId()
        {
            string prodId = null;
            try
            {
                //SqlParameter prmReturn = new SqlParameter();
                //prmReturn.ParameterName = "@ProductId";
                //prmReturn.Direction = System.Data.ParameterDirection.Output;
                //prmReturn.DbType= System.Data.DbType.String;
                //prmReturn.Size = 4;
                prodId=(from s in context.Products
                        select QuickKartDbContext.ufn_GenerateNewProductId()).FirstOrDefault();
            }
            catch (Exception ex) { 
                prodId = null;
            }
            return prodId;
        }
        public bool CheckEmailId(string emialId)
        {
            bool result;
            try
            {
                result = (from p in context.Users
                          select QuickKartDbContext.CheckMail(emialId)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}

