using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerReviewsAPI_.Services;
using CustomerReviewsAPI_.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CustomerReviewsAPI_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {

        private readonly CompanyService companyService;
        public CompanyController(CompanyService _companyService)
        {
            companyService = _companyService;
        }

        // GET: api/Company
        [HttpGet("get-all-companies-with-products")]
        public ActionResult<IEnumerable<CompanyProductsVM>> GetCompanies()
        {
            return companyService.GetCompanies();
        }

        // GET: api/Company/5
        [HttpGet("get-a-company-with-products")]
        public async Task<ActionResult<CompanyProductsVM>> GetCompany(int id)
        {
            return await companyService.GetCompany(id);
        }

        // PUT: api/Company/5
        [HttpPut("update-a-company-details")]
        public async Task<IActionResult> UpdateCompany(int id, CompanyVM company)
        {
            return await companyService.UpdateCompany(id,company);
        }

        // POST: api/Company
        [HttpPost("add-a-company-with-products")]
        public async Task<ActionResult<CompanyVM>> AddCompany(CompanyVM company)
        {
            await companyService.AddCompany(company);
            return CreatedAtAction("GetCompany", new { id = company.CompanyName }, company);
        }

        // DELETE: api/Company/5
        [HttpDelete("delete-a-company")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            return await companyService.DeleteCompany(id);
        }

        [HttpDelete("delete-a-companies-products")]
        public async Task<IActionResult> DeleteCompanyProducts(int id, CompanyVM updatedcompany)
        {
            return await companyService.DeleteCompanyProducts(id,updatedcompany);
        }



    }
}
