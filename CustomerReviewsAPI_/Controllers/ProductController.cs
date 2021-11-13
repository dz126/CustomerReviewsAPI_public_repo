using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerReviewsAPI_.Models;
using CustomerReviewsAPI_.Services;
using CustomerReviewsAPI_.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CustomerReviewsAPI_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ProductService productService;

        public ProductController(ProductService _productService)
        {
            productService = _productService;
        }

        //GET: api/Product
       [HttpGet("get-all-products-with-company-&-campaign-details")]
        public ActionResult<IEnumerable<ProductsCompaniesVM>> GetProducts()
        {
            return productService.GetProducts();
        }

        // GET: api/Product/5
        [HttpGet("get-a-product-with-company-&-campaign-details")]
        public async Task<ActionResult<ProductsCompaniesVM>> GetProduct(int id)
        {
            return await productService.GetProduct(id);
        }

        // PUT: api/Product/5
        [HttpPut("update-a-product-details")]
        public async Task<IActionResult> UpdateProduct(int id, ProductVM updatedproduct)
        {

            return await productService.UpdateProduct(id, updatedproduct);
        }

        //       POST: api/Product
        [HttpPost("add-a-product")]
        public async Task<ActionResult<Product>> AddProduct(ProductVM product)
         {
            return await productService.AddProduct(product);
         }

        // DELETE: api/Product/5
        [HttpDelete("delete-a-product")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return await productService.DeleteProduct(id);
        }


        //[HttpDelete("delete-a-products-companies")]
        //public async Task<IActionResult> DeleteProductsCompanies(int id, ProductVM updatedProduct)
        //{
        //    return await productService.DeleteProductsCompanies(id,updatedProduct);
        //}


        //[HttpDelete("delete-a-products-campaigns")]
        //public async Task<IActionResult> DeleteProductsCampaigns(int id, ProductVM updatedproduct)
        //{ 
        //    return await productService.DeleteProductsCampaigns(id,updatedproduct);
        //}


    }
}
