
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerReviewsAPI_.Models
{

    #region Product with its Company & Campaign details 
    
    [Table(name: "Products")]
    public class Product
    {

        [Key]
        public int ID { get; set; }

        [Column(name: "ProductName", TypeName = "nvarchar(50)")]
        public string ProductName { get; set; }

        #region Navigational properties
       // public ICollection<ProductCampaign> ProductCampaigns { get; set; }
        public ICollection<ProductCompany> ProductsCompanies { get; set; }

        public ICollection<CompanyCampaign> CompaniesCampaigns { get; set; }

        #endregion

    }

    #endregion
}
