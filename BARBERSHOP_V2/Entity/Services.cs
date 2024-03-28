using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BARBERSHOP_V2.Entity
{
    public class Services
    {
        [Key]
        public int serID { get; set; }
        public string? serName { get; set; }
        public string? serDescription { get; set; }
        public float serPrice { get; set; }
        public TimeSpan serTime { get; set; }


        #region QH

        // MO ServiceCate
        public int serCateID { get; set; }
        [ForeignKey("serCateID")]
        public ServiceCategory ServiceCategory { get; set; } = null!;

        // OM
        // BookingSer
        // serMana
        public ICollection<ServiceManagement> ServiceManagement { get; set; } = new HashSet<ServiceManagement>();
        //public ICollection<ServiceStore> ServiceStore { get; set; } = new HashSet<ServiceStore>();
        public ICollection<ServiceEmployee> ServiceEmployee { get; set; } = new HashSet<ServiceEmployee>();
        public ICollection<BookingService> BookingServices { get; set; } = new HashSet<BookingService>();

        #endregion
    }
}
