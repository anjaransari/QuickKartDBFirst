using DBFirst.DataAccessLayer;
using DBFirst.DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuickKartServices.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IQuickKartRepository _repository;
        public ProductController(IQuickKartRepository repository)
        {
            this._repository = repository;
        }
        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _repository.GetAllCategories();

            if (categories == null || categories.Count == 0)
            {
                return NotFound("No categories found.");
            }

            return Ok(categories);
        }
        [HttpGet("category/{categoryId}")]
        public IActionResult GetProductsOnCategory(byte categoryId)
        {
            var product=_repository.GetProductsOnCategory(categoryId);
            if (product == null || product.Count == 0)
            {
                return NotFound("No Products found.");
            }

            return Ok(product);
        }
        [HttpGet("FilterProducts/{categoryId}")]
        public IActionResult FilterProducts(byte categoryId)
        {
            var product=_repository.FilterProducts(categoryId);
            if (product == null)
            {
                return NotFound("No Products found.");
            }

            return Ok(product);
        }
        [HttpGet("FilterProductUsingLike/{pattern}")]
        public IActionResult FilterProductUsingLike(string pattern){
            var products=_repository.FilterProductUsingLike(pattern);
            if (products == null || products.Count == 0)
            {
                return NotFound("No categories found.");
            }

            return Ok(products);

        }
        [HttpPost("AddCat/{catName}")]
        public IActionResult AddCategory(string catName)
        {
            var status=_repository.AddCategory(catName);
            if(status==false)
            {
                return NotFound("not added.");
            }
            return Ok(status);
        }
        [HttpPut]
        public IActionResult UpdateCategory(byte categoryId, string catName)
        {
            var status=_repository.UpdateCategory(categoryId, catName);
            if(status==false)
            {
                return NotFound("not updated.");
            }
            return Ok(status);
        }
        [HttpDelete]
        public IActionResult DeleteCategory(byte categoryId)
        {
            var status= _repository.DeleteCategory(categoryId);
            if (status == false)
            {
                return NotFound("not deleted.");
            }
            return Ok(status);
        }
        [HttpPut]
        public IActionResult UpdateCategoryUsing(byte categoryId, string catName)
        {
            var status=_repository.UpdateCategoryUsing(categoryId,catName);
            return Ok(status);
        }
        [HttpPost]
        public IActionResult AddCategoryUsingUSP(string catName,byte catId)
        {
            var result=_repository.AddCategoryUsingUSP(catName, out catId);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetProductCategoryUsingTVF(byte catId)
        {
            var category = _repository.GetProductCategoryUsingTVF(catId);
            return Ok(category);
        }
        [HttpGet]
        public IActionResult GenerateNewProductId()
        {
            var result=_repository.GenerateNewProductId();
            if (result==null)
            {
                return NotFound("No categories found.");
            }
            return Ok(result);
        }
        [HttpGet]
        public IActionResult CheckEmailId(string emialId)
        {
            var status=_repository.CheckEmailId(emialId);
            return Ok(status);
        }

    }
}
