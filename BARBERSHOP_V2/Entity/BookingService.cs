using System.ComponentModel.DataAnnotations.Schema;

namespace BARBERSHOP_V2.Entity
{
    public class BookingService
    {

        #region QH

        // ManytoOne
            // Booking
        public int bookingID { get; set; }
        [ForeignKey("bookingID")]
        public Booking Booking { get; set; }
            // Service
        public int serID { get; set; }
        [ForeignKey("serID")]
        public Services Service { get; set; }

        public int? employeeID { get; set; }
        [ForeignKey("employeeID")]
        public Employee Employee { get; set; }
        #endregion
    }
}
