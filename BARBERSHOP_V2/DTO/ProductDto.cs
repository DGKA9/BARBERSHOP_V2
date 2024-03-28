namespace BARBERSHOP_V2.DTO
{
    public class ProductDto
    {
        public int proID { get; set; }
        public string? proName { get; set; }
        public string? proImage { get; set; }
        public float price { get; set; }
        public int quantity { get; set; }
        public string? proDescription { get; set; }
        public int producerID { get; set; }
        public int warehouseID { get; set; }
        public int cateID { get; set; }
    }
}
