using CustomerReviewsAPI_.Models;
using CustomerReviewsAPI_.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerReviewsAPI_.Services
{
    #region Campaign service
    public class CampaignService
    {

        //#region DbContext,OnbeforeSave


            private readonly ApplicationDBContext _context;
            private readonly OnBeforeSave onBeforeSave;

        //#endregion
        public CampaignService(ApplicationDBContext context, OnBeforeSave _onbeforesave)
        {
            _context = context;
            onBeforeSave = _onbeforesave;
        }


        #region Get all Companies campaigns details with its product details
        public ActionResult<IEnumerable<CampaignsCompaniesVM>> GetCampaigns()
        {


            var campaigns = _context.Campaigns.Select(campaigns=>new CampaignsCompaniesVM() {
            
                CampaignName=campaigns.CampaignName,
                ReviewUrl=campaigns.ReviewUrl,
                IsActive=campaigns.IsActive,
                CompanyNames=campaigns.CompaniesCampaigns.Select(e=>e.Company.CompanyName).ToList(),
                ProductNames=campaigns.CompaniesCampaigns.Select(e=>e.Product.ProductName).ToList()



            }).ToList<CampaignsCompaniesVM>();

            //var campaigns = _context.Campaigns.Select(campaigns => new CampaignsProductsVM()
            //{

            //    CampaignName = campaigns.CampaignName,
            //    ReviewUrl = campaigns.ReviewUrl,
            //    IsActive = campaigns.IsActive,
            //    ProductNames = campaigns.ProductCampaigns.Select(e => e.Product.ProductName).ToList()

            //}).ToList();

            //return campaigns;
            return campaigns;

        }
        #endregion


        #region Get a Companies campaign details with its products details
        public async Task<ActionResult<CampaignsCompaniesVM>> GetCampaign(int id)
        {

            var campaign = await _context.Campaigns.Where(e => e.ID.Equals(id)).Select(campaigns => new CampaignsCompaniesVM()
            {
                CampaignName=campaigns.CampaignName,
                ReviewUrl=campaigns.ReviewUrl,
                IsActive=campaigns.IsActive,
                CompanyNames=campaigns.CompaniesCampaigns.Select(e => e.Company.CompanyName).ToList(),
                ProductNames=campaigns.CompaniesCampaigns.Select(e => e.Product.ProductName).ToList()
            
            }).FirstOrDefaultAsync();


            //var campaign = await _context.Campaigns.Where(e => e.ID.Equals(id)).Select(campaigns => new CampaignsProductsVM()
            //{

            //    CampaignName = campaigns.CampaignName,
            //    ReviewUrl = campaigns.ReviewUrl,
            //    IsActive = campaigns.IsActive,
            //    ProductNames = campaigns.ProductCampaigns.Select(e => e.Product.ProductName).ToList()

            //}).FirstOrDefaultAsync();

            //return campaign;
            return campaign;

        }

        #endregion


        #region update a campaigns details
        public async Task<IActionResult> UpdateCampaign(int id, CampaignVM updatedcampaign)
        {

            Campaign campaigntoupdate = await _context.Campaigns.FirstOrDefaultAsync(e => e.ID.Equals(id));

            #region update campaign information


            if (id != campaigntoupdate.ID)
            {
                return new BadRequestResult();
            }
            else
            {
                campaigntoupdate.CampaignName = updatedcampaign.CampaignName;
                campaigntoupdate.IsActive = updatedcampaign.IsActive;
                campaigntoupdate.ReviewUrl = updatedcampaign.ReviewUrl;

                _context.Entry(campaigntoupdate).State = EntityState.Modified;

            }

            try
            {
                
                onBeforeSave.BeforeSave(typeof(Campaign));
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampaignExists(id))
                {
                    return new NotFoundResult();
                }
                else
                {
                    throw;
                }
            }

           
            return new NoContentResult();
            #endregion

          
            //return new nocontentresult();
        }

        #endregion


        #region Add Companies to campaign and with its products
        public async Task<ActionResult<Campaign>> AddCampaign(CampaignVM campaign)
        {

            #region Add Campaign details 
            //To Campaign

            var newCampaign = new Campaign()
            {
                CampaignName = campaign.CampaignName,
                ReviewUrl = campaign.ReviewUrl,
                IsActive = campaign.IsActive

            };

            await _context.Campaigns.AddAsync(newCampaign);
            onBeforeSave.BeforeSave(typeof(Campaign));
            await _context.SaveChangesAsync();
            #endregion


            #region Create a Campaign for a Company & it's products 
            //To Campaigns-Companies

            if (campaign.ProductIDs!=null||campaign.CompanyIDs!=null)
            {

                foreach (var i in campaign.CompanyIDs)
                {
                    foreach (var j in campaign.ProductIDs)
                    {
                        var companiesCampaign = new CompanyCampaign()
                        {
                            CompanyID = i,
                            CampaignID = newCampaign.ID,
                            ProductID = j
                        };
                        await _context.CompaniesCampaigns.AddAsync(companiesCampaign);
                        try
                        {
                            await _context.SaveChangesAsync();
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }


                }
            
            }
            #endregion



           

            return new CreatedAtActionResult("GetCampaign", "", new { name = campaign.CampaignName }, campaign);

        }
        #endregion

        #region Remove a campaign
        public async Task<IActionResult> RemoveCampaign(int id) {

            var CampaignsCompaniesToDelete = _context.CompaniesCampaigns.Where(e => e.CampaignID.Equals(id)).ToList();

            foreach (CompanyCampaign e in CampaignsCompaniesToDelete) { 
            
            _context.CompaniesCampaigns.Remove(e);
            
            }

            //Get the Campaign details to delete
            var campaign = await _context.Campaigns.FindAsync(id);
            if (campaign == null)
            {
                return new NotFoundResult();
            }

            _context.Campaigns.Remove(campaign);
            await _context.SaveChangesAsync();
            return new NoContentResult();

        }

        #endregion

        #region Remove Products of Company in a Campaign

        #endregion

        #region Check if a campaign exists
        private bool CampaignExists(int id)
        {
            return _context.Campaigns.Any(e => e.ID == id);
        }

        #endregion
       


    }

    #endregion
}
