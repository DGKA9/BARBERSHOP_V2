namespace BARBERSHOP_V2.DTO
{
    public class LocationStoreDto
    {
        public int locationID { get; set; }
        public string? currentAddress { get; set; }
        public string? subDistrict { get; set; }
        public string? district { get; set; }
        public int cityID { get; set; }
        public int storeID { get; set; }
    }
}
