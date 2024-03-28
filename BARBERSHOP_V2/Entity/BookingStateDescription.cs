using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BARBERSHOP_V2.Entity
{
    public class BookingStateDescription
    {
        [Key]
        public int stateID { get; set; }
        public string? description { get; set;}



        #region QH

        //OnetoOne Booking
        public int bookingID { get; set; }
        [ForeignKey("bookingID")]
        public Booking Booking { get; set; } = null!;
        #endregion
    }
}
