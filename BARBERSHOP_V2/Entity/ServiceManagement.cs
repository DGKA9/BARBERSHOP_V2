using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BARBERSHOP_V2.Entity
{
    public class ServiceManagement
    {
        [Key]
        public int serManagerID {get; set;}

        #region QH

        // MO 
            // Store
        public int storeID { get; set;}
        [ForeignKey("storeID")]
        public Store Store { get; set; } = null!;
        // Service
        public int serID { get; set; }
        [ForeignKey("serID")]
        public Services Service { get; set; } = null!;
        #endregion
    }
}
