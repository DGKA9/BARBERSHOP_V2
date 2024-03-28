using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BARBERSHOP_V2.Entity
{
    public class Warehouse
    {
        [Key]
        public int warehouseID { get; set; }
        public string? warehouseName { get; set; }
        public float totalAsset { get; set; }
        public int capacity { get; set; }

        #region QH

        // OO address
        public int addressID { get; set; }
        [ForeignKey("addressID")]
        public Address Address { get; set; } = null!;
        // MO srore
        public int storeID { get; set; }
        [ForeignKey("storeID")]
        public Store Store { get; set; } = null!;
        // OM product
        public ICollection<Product> products { get; set; } = new HashSet<Product>();
        #endregion 
    }
}
