using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BARBERSHOP_V2.Entity
{
    public class Order
    {
        [Key]
        public int orderID { get; set; }
        public DateTime orderDate { get; set; }
        public DateTime deliveryDate { get; set; }
        public string? orderStatus { get; set; }
        public int totalInvoice { get; set; }

        #region QH

        // MO
        // Customer
        public int customerID { get; set; }
        [ForeignKey("customerID")]
        public Customer customer { get; set; } = null!;
        // Payment
        public int payID { get; set; }
        [ForeignKey("payID")]
        public Payment Payment { get; set; } = null!;
        //OM productOrder
        public ICollection<ProductOrder> ProductOrders { get; set; } = new HashSet<ProductOrder>();
        #endregion
    }
}
