using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BARBERSHOP_V2.Entity
{
    public class Producer
    {
        [Key]
        public int producerID { get; set; }
        public string? producerName { get; set; }
        public string? numberphone { get; set; }


        #region QH

        // OO
        // address
        public int addressID { get; set; }
        [ForeignKey("addressID")]
        public Address Address { get; set; } = null!;
        // OM
        // product
        public ICollection<Product> Product { get; } = new HashSet<Product>();
        #endregion
    }
}
