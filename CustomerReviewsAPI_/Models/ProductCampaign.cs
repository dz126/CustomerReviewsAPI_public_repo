

namespace CustomerReviewsAPI_.Models
{
    
    #region Campaigns and its products 
    public class ProductCampaign
    {
        public int ID { get; set; }

        public int? ProductID { get; set; }
        public Product Product { get; set; }

        public int? CampaignID { get; set; }
        public Campaign Campaign { get; set; }

    }

    #endregion
}
