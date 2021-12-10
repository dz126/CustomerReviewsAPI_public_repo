

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerReviewsAPI_.Models
{
    #region Company and its Campaigns

    [Table(name: "CompaniesCampaigns")]
    public class CompanyCampaign
    {
        [Key]
        public int ID { get; set; }

        public int? CompanyID { get; set; }
        public Company Company { get; set; }

        public int? ProductID { get; set; }
        public Product Product { get; set; }

        public int? CampaignID { get; set; }
        public Campaign Campaign { get; set; }
    }

    #endregion

}
