using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BARBERSHOP_V2.Entity
{
    public class Store
    {
        [Key]
        public int storeID { get; set; }
        public string? storeName { get; set; }
        public string? numberphone { get; set; }


        #region QH

        // MO workingHour
        public int workingHourID { get; set; }
        [ForeignKey("workingHourID")]
        public WorkingHour workingHour { get; set; } = null!;
        // OM
        // warehouse
        public ICollection<Warehouse> warehouse { get; set; } = new HashSet<Warehouse>();
        // SerMana
        public ICollection<ServiceManagement> serviceManagement { get; set; } = new HashSet<ServiceManagement>();
        // LocaStore
        public ICollection<LocationStore> locationStore { get; set; } = new HashSet<LocationStore>();
        // evaluate
        public ICollection<Evaluate> evaluates { get; set; } = new HashSet<Evaluate>();
        // employee
        public ICollection<Employee> employees { get; set; } = new HashSet<Employee>();

        public virtual ICollection<Booking>? Bookings { get; set; }
        //public ICollection<ServiceStore> ServiceStore { get; set; } = new HashSet<ServiceStore>();
        #endregion
    }
}
