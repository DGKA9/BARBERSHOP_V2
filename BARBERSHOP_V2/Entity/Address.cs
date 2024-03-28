using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BARBERSHOP_V2.Entity
{
    public class Address
    {
        [Key]
        public int addressID { get; set; }
        public string? currentAddress { get; set; }
        public string? subDistrict { get; set; }
        public string? district { get; set; }

        #region QH

        // ManyToOne City
        public int cityID { get; set; }
        [ForeignKey("cityID")]
        public City city { get; set; } = null!;
        // OnetoOne
        // Customer
        public ICollection<CustomerAddress> customers { get; set; } = new HashSet<CustomerAddress>();
        // Producer
        public Producer? Producer { get; set; }
        // Warehouse
        public Warehouse? Warehouse { get; set; }
        // OnetoMany Employee
        public ICollection<Employee> Employees { get; } = new HashSet<Employee>();
        #endregion
    }
}
