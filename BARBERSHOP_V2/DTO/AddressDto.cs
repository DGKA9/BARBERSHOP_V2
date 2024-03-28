namespace BARBERSHOP_V2.DTO
{
    public class AddressDto
    {
        public int addressID { get; set; }
        public string? currentAddress { get; set; }
        public string? subDistrict { get; set; }
        public string? district { get; set; }
        public int cityID { get; set; }
    }
}
