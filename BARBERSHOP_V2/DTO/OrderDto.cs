namespace BARBERSHOP_V2.DTO
{
    public class OrderDto
    {
        public int orderID { get; set; }
        public DateTime orderDate { get; set; }
        
        public DateTime deliveryDate { get; set; }
        public string? orderStatus { get; set; }
        public int totalInvoice { get; set; }
        public int customerID { get; set; }
        public int payID { get; set; }
    }
}
