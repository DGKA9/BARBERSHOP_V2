using System.ComponentModel.DataAnnotations;

namespace BARBERSHOP_V2.Entity
{
    public class Country
    {
        [Key]
        public int countryID { get; set; }
        public string? countryName { get; set; }

        #region QH

        //OneMany City
        public ICollection<City> city { get; set; } = new HashSet<City>();
        #endregion
    }
}
