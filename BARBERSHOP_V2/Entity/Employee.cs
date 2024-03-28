using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BARBERSHOP_V2.Entity
{
    public class Employee
    {
        [Key]
        public int employeeID { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? picture { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? email { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Phone Number")]
        public string? numberphone { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime dateOfBirth { get; set; }
        public DateTime wordDay { get; set; }



        #region QH

        // OneOne User
        public int userID { get; set; }
        [ForeignKey("userID")]
        public User user { get; set; } = null!;
        // ManyOne 
        // Store
        public int storeID { get; set; }
        [ForeignKey("storeID")]
        public Store Store { get; set; } = null!;
        // Address
        public int addressID { get; set; }
        [ForeignKey("addressID")]
        public Address Address { get; set; } = null!;

        public virtual ICollection<Booking>? Bookings { get; set; }
        public ICollection<ServiceEmployee> ServiceEmployee { get; set; } = new HashSet<ServiceEmployee>();
        #endregion
    }
}
