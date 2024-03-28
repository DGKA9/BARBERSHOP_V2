using System.ComponentModel.DataAnnotations.Schema;

namespace BARBERSHOP_V2.Entity
{
    public class ServiceEmployee
    {
        public int employeeID { get; set; }
        [ForeignKey("employeeID")] public Employee Employee { get; set; } = null!;
        public int serID { get; set; }
        [ForeignKey("serID")] public Services Services { get; set; } = null!;
    }
}
