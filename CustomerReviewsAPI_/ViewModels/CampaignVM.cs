

using System;
using System.Collections.Generic;

namespace CustomerReviewsAPI_.ViewModels
{
   
    #region View model to add a campaign with its products details
    public class CampaignVM
    {
       
        public string CampaignName { get; set; }
        public string ReviewUrl { get; set; }
        public Boolean IsActive { get; set; }
        //public List<int?> ProductIDs { get; set; }
        public List<int?> CompanyIDs { get; set; }
        public List<int?> ProductIDs { get; set; }

    }

    #endregion

   #region View model to retrieve campaign details with its product details
    public class CampaignsCompaniesVM
    {
        public string CampaignName { get; set; }
        public string ReviewUrl { get; set; }
        public Boolean IsActive { get; set; }
        //public List<string> ProductNames { get; set; }
        public List<string> CompanyNames { get; set; }
        public List<string> ProductNames { get; set; }



    }
    #endregion
}
