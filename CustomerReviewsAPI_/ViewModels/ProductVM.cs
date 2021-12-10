


using System.Collections.Generic;

namespace CustomerReviewsAPI_.ViewModels
{
     #region View model to Add a Product with it's company and campaign details 
    public class ProductVM
    {
        public string ProductName { get; set; }
        //public List<int?> CampaignID { get; set; }
        //public List<int?> CompanyID { get; set; }
    }
    #endregion
    
    #region View model for Retrieving  a Product with its Company & Campaign details
    public class ProductsCompaniesVM
    {
        public string ProductName { get; set; }
        public List<string> CampaignName { get; set; }
        public List<string> CompanyName { get; set; }

    }
    #endregion
}
