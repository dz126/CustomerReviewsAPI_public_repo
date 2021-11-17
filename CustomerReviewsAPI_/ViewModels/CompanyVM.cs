

namespace CustomerReviewsAPI_.ViewModels
{

    #region View model to add a company with its products 
    public class CompanyVM
    {
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string POCName { get; set; }
        public string PhoneNumber { get; set; }
        public Boolean IsActive { get; set; }

        public List<int?> ProductID { get; set; }
        //public List<int?> CampaignID { get; set; }



    }

    #endregion

    
  #region View model to retirieve a company with its products details
    public class CompanyProductsVM
    {
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string POCName { get; set; }
        public string PhoneNumber { get; set; }
        public Boolean IsActive { get; set; }
        public List<string> ProductName { get; set; }
        public List<string> CampaignName { get; set; }

    }

    #endregion


}
