using System.ComponentModel.DataAnnotations;

namespace BARBERSHOP_V2.Entity
{
    public class Payment
    {
        [Key]
        public int payID { get; set; }
        public string? payMethod { get; set; }
        public Boolean payStatus { get; set; }


        #region QH

        // OM
            // Order
            ICollection<Order> orders { get; set; } = new HashSet<Order>();
            // Booking
            ICollection<Booking> bookings { get; set; } = new HashSet<Booking>();
        #endregion
    }
}
