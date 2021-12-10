

using CustomerReviewsAPI_.Models;
using CustomerReviewsAPI_.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerReviewsAPI_.Services
{
    #region Product service
    public class ProductService
    {

        private readonly ApplicationDBContext _context;
        private readonly OnBeforeSave onBeforeSave;

        public ProductService(ApplicationDBContext context, OnBeforeSave _onBeforeSave)
        {
            _context = context;
            onBeforeSave = _onBeforeSave;
        }

        #region Get all products with its Company and Campaign details


        public ActionResult<IEnumerable<ProductsCompaniesVM>> GetProducts()
        {

            var allproducts = _context.Products.Select(products => new ProductsCompaniesVM()
            {
                ProductName = products.ProductName,
                CampaignName = products.CompaniesCampaigns.Select(e=>e.Campaign.CampaignName).ToList(),
                CompanyName = products.ProductsCompanies.Select(e => e.Company.CompanyName).ToList()

            }).ToList<ProductsCompaniesVM>();

            return allproducts;
        }

        #endregion

        #region Get a product with its company and campaign details
        public async Task<ActionResult<ProductsCompaniesVM>> GetProduct(int id)
        {

            var product = await _context.Products.Where(e=>e.ID.Equals(id)).Select(products => new ProductsCompaniesVM()
            {

                ProductName = products.ProductName,
                CampaignName = products.CompaniesCampaigns.Select(e => e.Campaign.CampaignName).ToList(),
                CompanyName = products.ProductsCompanies.Select(e => e.Company.CompanyName).ToList()

            }).FirstOrDefaultAsync();


            return product;
        }

        #endregion

        #region Update a product and its company & campaign details
        public async Task<IActionResult> UpdateProduct(int id, ProductVM updatedproduct)
        {

            var productToUpdate = _context.Products.FirstOrDefault(e => e.ID.Equals(id));

            #region Update Product Information


            if (id != productToUpdate.ID)
            {
                return new BadRequestResult();
            }
            else
            {

                productToUpdate.ProductName = updatedproduct.ProductName;
                _context.Entry(productToUpdate).State = EntityState.Modified;
                try
                {
                    onBeforeSave.BeforeSave(typeof(Product));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(id))
                    {
                        return new NotFoundResult();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            #endregion

            return new NoContentResult();
        }
        #endregion

        #region Add a product 
        public async Task<ActionResult<Product>> AddProduct(ProductVM product)
        {
            //var e = product with { ProductName="" };


            #region To Product
            var newProduct = new Product()
            {
                ProductName = product.ProductName,
            };


            await _context.Products.AddAsync(newProduct);
            onBeforeSave.BeforeSave(typeof(Product));
            await _context.SaveChangesAsync();
            #endregion

          
            return new CreatedAtActionResult("GetProduct", "", new { id = product.ProductName }, product);
        }

        #endregion

        #region Delete a product 
        public async Task<IActionResult> DeleteProduct(int id)
        {


            //Get Products details to delete
            List<Product> Products = await _context.Products.Where(e => e.ID.Equals(id)).ToListAsync<Product>();

            if (Products!=null)
            {
                //Remove products old data
                foreach (Product e in Products)
                {

                    _context.Products.Remove(e);
                    _context.SaveChanges();

                }
            }


            return new NoContentResult();
        }
        #endregion

        #region Check if a product exists
            public bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
        #endregion



    }
    #endregion
}
