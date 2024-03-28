using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BARBERSHOP_V2.Entity
{
    public class Evaluate
    {
        [Key]
        public int evaluateID { get; set; }
        public int rating { get; set; }
        public string? comment { get; set; }
        public DateTime lastUpdate { get; set; } = DateTime.Now;


        #region QH

        // ManyOne 
        // Customer
        public int customerID { get; set; }
        [ForeignKey("customerID")]
        public Customer customer { get; set; } = null!;
        // Store
        public int storeID { get; set; }
        [ForeignKey("storeID")]
        public Store store { get; set; } = null!;
        #endregion
    }
}
