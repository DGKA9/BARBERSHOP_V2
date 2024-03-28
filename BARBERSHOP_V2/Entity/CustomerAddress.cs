using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BARBERSHOP_V2.Entity
{
    public class CustomerAddress
    {
        [Key]
        public int cusAddressId { get; set; }

        public int customerID { get; set; }
        [ForeignKey("customerID")]
        public Customer Customer { get; set; } = null!;
        // product
        public int addressID { get; set; }
        [ForeignKey("addressID")]
        public Address Address { get; set; } = null!;
    }
}
