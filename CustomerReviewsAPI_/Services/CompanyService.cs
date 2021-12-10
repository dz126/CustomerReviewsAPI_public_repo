

using CustomerReviewsAPI_.Models;
using CustomerReviewsAPI_.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerReviewsAPI_.Services
{
    #region Company service
    public class CompanyService
    {

        private readonly ApplicationDBContext _context;
        private readonly OnBeforeSave onBeforeSave;

        public CompanyService(ApplicationDBContext context, OnBeforeSave _onBeforeSave)
        {
            _context = context;
            onBeforeSave = _onBeforeSave;
        }

        #region Get all companies and its products
        public ActionResult<IEnumerable<CompanyProductsVM>> GetCompanies()
        {

            var companies = _context.Companies.Select(companies => new CompanyProductsVM()
            {
                CompanyName = companies.CompanyName,
                ProductName = companies.ProductsCompanies.Select(e => e.Product.ProductName).ToList(),
                CampaignName=companies.CompaniesCampaigns.Select(e => e.Campaign.CampaignName).ToList(),
                Address = companies.Address,
                POCName = companies.POCName,
                PhoneNumber = companies.PhoneNumber,
                IsActive = companies.IsActive

            }).ToList<CompanyProductsVM>();

            return companies;

        }

        #endregion


        #region Get a company and its products
        public async Task<ActionResult<CompanyProductsVM>> GetCompany(int id)
        {
            var company = await _context.Companies.Where(e => e.ID.Equals(id)).Select(companies => new CompanyProductsVM()
            {
                CompanyName = companies.CompanyName,
                ProductName = companies.ProductsCompanies.Select(e => e.Product.ProductName).ToList(),
                CampaignName=companies.CompaniesCampaigns.Where(e=>e.CompanyID.Equals(id)).Select(e=>e.Campaign.CampaignName).ToList(),
                Address = companies.Address,
                POCName = companies.POCName,
                PhoneNumber = companies.PhoneNumber,
                IsActive = companies.IsActive


            }).FirstOrDefaultAsync();

            if (company == null)
            {
                return new NotFoundResult();
            }

            return company;
        }

        #endregion


        #region Update a company and its products details
        public async Task<IActionResult> UpdateCompany(int id, CompanyVM updatedcompany)
        {
            #region Update Company Information

            
            var companyToUpdate = _context.Companies.FirstOrDefault(e => e.ID.Equals(id));

            if (id != companyToUpdate.ID)
            {
                return new BadRequestResult();
            }
            else {

                companyToUpdate.CompanyName = updatedcompany.CompanyName;
                companyToUpdate.Address = updatedcompany.Address;
                companyToUpdate.IsActive = updatedcompany.IsActive;
                companyToUpdate.PhoneNumber = updatedcompany.PhoneNumber;
                companyToUpdate.POCName = updatedcompany.POCName;
                            
            }

            _context.Entry(companyToUpdate).State = EntityState.Modified;

            try
            {
                onBeforeSave.BeforeSave(typeof(Company));
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
                {
                    return new NotFoundResult();
                }
                else
                {
                    throw;
                }
            }

            #endregion

            #region Update Companies products

            
            //Update Companies Products

            var ProductsToUpdate = _context.ProductsCompanies.Where(e => e.CompanyID.Equals(id)).Select(e => e.ProductID).ToList();
            var ProductToAdd=updatedcompany.ProductID.ToList();

            var  count = false;
            if (ProductsToUpdate.Except(ProductToAdd).Count().Equals(0)) count = true; else count = false;

            //if (ProductToAdd.Count > ProductsToUpdate.Count)
            //{
            //    foreach (var PTA_ in ProductToAdd)
            //    {

            //        if (ProductsToUpdate.Contains(PTA_))
            //        {
            //            count = true;
            //            //continue;
            //        }
            //        else count = false;

            //    }
            //}
            //else
            //{
            //    foreach (var PTU_ in ProductsToUpdate)
            //    {

            //        if (ProductToAdd.Contains(PTU_))
            //        {
            //            count = true;
            //            //continue;
            //        }
            //        else count = false;

            //    }
            //}


            if (count)
                return new NoContentResult();
            else
                return await DeleteCompanyProducts(id, updatedcompany);

            #endregion

        }

        #endregion


        #region Add a company and it's products
        public async Task<ActionResult<CompanyVM>> AddCompany(CompanyVM company)
        {

            #region To Company


            var newCompany = new Company()
            {

                CompanyName = company.CompanyName,
                Address = company.Address,
                PhoneNumber = company.PhoneNumber,
                POCName = company.POCName,
                IsActive = company.IsActive

            };

            await _context.Companies.AddAsync(newCompany);
            onBeforeSave.BeforeSave(typeof(Company));
            await _context.SaveChangesAsync();
            #endregion

            #region Add Products to Company
            //To Products-Companies

            if (company.ProductID != null)
            {
                foreach (var i in company.ProductID)
                {

                    var productCompany = new ProductCompany()
                    {
                        ProductID = i,
                        CompanyID = newCompany.ID
                    };

                    await _context.ProductsCompanies.AddAsync(productCompany);
                    await _context.SaveChangesAsync();

                }
            }
            else
            {
                foreach (var i in company.ProductID)
                {

                    var productCompany = new ProductCompany()
                    {
                        ProductID = null,
                        CompanyID = newCompany.ID
                    };

                    await _context.ProductsCompanies.AddAsync(productCompany);
                    await _context.SaveChangesAsync();

                }
            }

            #endregion

            return new CreatedAtActionResult("GetCompany","", new { id = company.CompanyName }, company);
        }

        #endregion


        #region Delete a company and it products
        public async Task<IActionResult> DeleteCompany(int id)
        {

            //Get the companies products details to delete
            List<ProductCompany> companiesProducts = _context.ProductsCompanies.Where(e => e.CompanyID.Equals(id)).ToList<ProductCompany>();

            foreach (ProductCompany e in companiesProducts)
            {

                _context.ProductsCompanies.Remove(e);
                _context.SaveChanges();

            }

            //Get the Company details to delete
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return new NotFoundResult();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();


            return new NoContentResult();
        }

        #endregion


        #region Add new products to a company
        public async Task<IActionResult> DeleteCompanyProducts(int id, CompanyVM updatedcompany)
        {
            if (!CompanyExists(id)) { return new NoContentResult(); }

            List<ProductCompany> companiesProducts = _context.ProductsCompanies.Where(e => e.CompanyID.Equals(id)).ToList<ProductCompany>();

            foreach (ProductCompany e in companiesProducts) {

                _context.ProductsCompanies.Remove(e);
                _context.SaveChanges();

            }


            #region Add new products To a Company

            if (updatedcompany.ProductID != null)
            {
                foreach (var i in updatedcompany.ProductID)
                {

                    var productCompany = new ProductCompany()
                    {
                        ProductID = i,
                        CompanyID = id
                    };

                    await _context.ProductsCompanies.AddAsync(productCompany);
                    await _context.SaveChangesAsync();

                }
            }
            else
            {
                foreach (var i in updatedcompany.ProductID)
                {

                    var productCompany = new ProductCompany()
                    {
                        ProductID = null,
                        CompanyID = id
                    };

                    await _context.ProductsCompanies.AddAsync(productCompany);
                    await _context.SaveChangesAsync();

                }
            }

            #endregion

            return new NoContentResult();
        }
        #endregion


        #region Check if a Company exists
        public bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.ID == id);
        }

        #endregion



    }

    #endregion
}
