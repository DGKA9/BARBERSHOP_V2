using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BARBERSHOP_V2.Entity
{
    public class CustomerNotification
    {
        [Key]
        public int cNotificationID { get; set; }


        #region QH

        // ManyOne 
        // Customer
        public int customerID { get; set; }
        [ForeignKey("customerID")]
        public Customer Customer { get; set; } = null!;
        // Noti
        public int notiID { get; set; }
        [ForeignKey("notiID")]
        public Notification Notification { get; set; } = null!;
        #endregion
    }
}
