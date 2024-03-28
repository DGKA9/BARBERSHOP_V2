using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BARBERSHOP_V2.Entity
{
    public class LocationStore
    {
        [Key]
        public int locationID { get; set; }
        public string? currentAddress { get; set; }
        public string? subDistrict { get; set; }
        public string? district { get; set; }



        #region QH

        // ManyOne
            // City
        public int cityID { get; set; }
        [ForeignKey("cityID")]
        public City city { get; set; } = null!;
            // Store
        public int storeID { get; set; }
        [ForeignKey("storeID")]
        public Store Store { get; set; } = null!;
        #endregion
    }
}
