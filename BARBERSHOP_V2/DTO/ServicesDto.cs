namespace BARBERSHOP_V2.DTO
{
    public class ServicesDto
    {
        public int serID { get; set; }
        public string? serName { get; set; }
        public string? serDescription { get; set; }
        public float serPrice { get; set; }
        public int serCateID { get; set; }
        public TimeSpan serTime { get; set; }
    }
}
