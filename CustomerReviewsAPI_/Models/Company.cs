

namespace CustomerReviewsAPI_.Models
{

    #region Company with its products details
    
    [Table(name: "Companies")]
    public class Company
    {

        [Key]
        public int ID { get; set; }


        [Column(name: "CompanyName", TypeName = "nvarchar(50)")]
        public string CompanyName { get; set; }


        [Column(name: "Address", TypeName = "nvarchar(max)")]
        public string Address { get; set; }


        [Column(name: "POCName", TypeName = "nvarchar(50)")]
        public string POCName { get; set; }

        [Column(name: "PhoneNumber", TypeName = "nvarchar(50)")]
        public string PhoneNumber { get; set; }


        [Column(name: "IsActive", TypeName = "bit")]
        public Boolean IsActive { get; set; }


        #region Navigational property
        public ICollection<ProductCompany> ProductsCompanies { get; set; }

        public ICollection<CompanyCampaign> CompaniesCampaigns { get; set; }

        #endregion

    }
    #endregion
}
