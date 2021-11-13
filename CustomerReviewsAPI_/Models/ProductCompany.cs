

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerReviewsAPI_.Models
{

    #region Companies and its Products

    [Table(name: "ProductsCompanies")]
    public class ProductCompany
    {
        [Key]
        public int ID { get; set; }

        public int? ProductID { get; set; }
        public Product Product { get; set; }

        public int? CompanyID { get; set; }
        public Company Company { get; set; }

    }

    #endregion
}
