

using CustomerReviewsAPI_.Models;
using CustomerReviewsAPI_.Services;
using CustomerReviewsAPI_.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace CustomerReviewsAPI_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly CampaignService campaignService;
        public CampaignController(CampaignService _campaignService)
        {
            campaignService = _campaignService;
        }

        // GET: api/Campaign
        [HttpGet("get-all-campaigns-with-comapnies-details")]
        public ActionResult<IEnumerable<CampaignsCompaniesVM>> GetCampaigns()
        {
            return campaignService.GetCampaigns();
        }

        // GET: api/Campaign/5
        [HttpGet("get-a-campaign-with-company-details")]
        public async Task<ActionResult<CampaignsCompaniesVM>> GetCampaign(int id)
        {
            return await campaignService.GetCampaign(id);
        }

        // PUT: api/Campaign/5
        [HttpPut("update-a-campaign-details")]
        public async Task<IActionResult> UpdateCampaign(int id, CampaignVM campaign)
        {
            return await campaignService.UpdateCampaign(id, campaign);
        }

        // POST: api/Campaign
        [HttpPost("add-a-campaign-with-company-and-its-products")]
        public async Task<ActionResult<Campaign>> AddCampaign(CampaignVM campaign)
        {
            await campaignService.AddCampaign(campaign);
            return CreatedAtAction("GetCampaign", new { name = campaign.CampaignName }, campaign);
        }

        //DELETE: api/Campaign/5
        [HttpDelete("delete-a-campaign")]
        public async Task<IActionResult> RemoveCampaign(int id)
        {
            return await campaignService.RemoveCampaign(id);   
        }

        //[HttpDelete("delete-a-campaigns-products")]
        //public async Task<IActionResult> DeleteCampaignProducts(int id, CampaignVM updatedcampaign)
        //{
        //    return await campaignService.DeleteCampaignProducts(id,updatedcampaign);
        //}

        //[HttpDelete("delete-a-companies-products-from-a-campaign")]

    }
}
