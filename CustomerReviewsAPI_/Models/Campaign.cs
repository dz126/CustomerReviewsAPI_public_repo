


namespace CustomerReviewsAPI_.Models
{
    
    #region Campaign with its products details
        
    [Table(name: "Campaigns")]
    public class Campaign
    {
        [Key]
        public int ID { get; set; }


        [Column(name: "CampaignName", TypeName = "nvarchar(50)")]
        public string CampaignName { get; set; }


        [Column(name: "ReviewUrl", TypeName = "nvarchar(max)")]
        public string ReviewUrl { get; set; }


        [Column(name: "IsActive", TypeName = "bit")]
        public Boolean IsActive { get; set; }

        #region Navigation property
        //public ICollection<ProductCampaign> ProductCampaigns { get; set; }
        public ICollection<CompanyCampaign> CompaniesCampaigns { get; set; }

        #endregion
    }
    #endregion
}
