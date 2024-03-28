using System.ComponentModel.DataAnnotations;

namespace BARBERSHOP_V2.Entity
{
    public class ServiceCategory
    {
        [Key]
        public int serCateID { get; set; }
        public string? serCateName { get; set; }
        public string? description { get; set; }


        #region QH
        public ICollection<Services> services { get; set; } = new HashSet<Services>();
        #endregion
    }
}
