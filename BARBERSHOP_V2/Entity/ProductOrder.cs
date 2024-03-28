using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BARBERSHOP_V2.Entity
{
    public class ProductOrder
    {
        [Key]
        public int proOrderID { get; set; }
        public int proOrderQuantity { get; set; }

        #region QH

        // MO
        // Order
        public int orderID { get; set; }
        [ForeignKey("orderID")]
        public Order Order { get; set; } = null!;
        // product
        public int proID { get; set; }
        [ForeignKey("proID")]
        public Product Product { get; set; } = null!;
        #endregion
    }
}
