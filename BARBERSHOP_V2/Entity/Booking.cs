using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BARBERSHOP_V2.Entity
{
    public class Booking
    {
        [Key]
        public int bookingID { get; set; }
        public DateTime startDate { get; set; } = DateTime.Now;
        public DateTime dateFounded { get; set; }
        public TimeSpan startTime { get; set; }
        public TimeSpan endTime { get; set; }
        public string? note { get; set; }

        #region QH

        // ManytoOne
        // Customer
        public int customerID { get; set; }
        [ForeignKey("customerID")]
        public Customer customer { get; set; } = null!;
        // Payment
        //public int payID { get; set; }
        //[ForeignKey("payID")]
        //public Payment payment { get; set; } = null!;

        public int storeID { get; set; }
        [ForeignKey("storeID")]
        public Store Store { get; set; } = null!;


        //public int employeID { get; set; }
        //[ForeignKey("employeID")]
        //public Employee employee { get; set; } = null!;
        

        // OnetoOne BookingSateDescription
        public BookingStateDescription? BookingSateDescription { get; set; }
        public ICollection<BookingService> BookingServices { get; set; } = new HashSet<BookingService>();

        #endregion

    }

}
