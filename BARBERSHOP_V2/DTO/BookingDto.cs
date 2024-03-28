namespace BARBERSHOP_V2.DTO
{
    public class BookingDto
    {
        public int bookingID { get; set; }
        public DateTime startDate { get; set; }
        public DateTime dateFounded { get; set; }
        public TimeSpan startTime { get; set; }
        public TimeSpan endTime { get; set; }
        public string? note { get; set; }
        public int customerID { get; set; }
        public int storeID { get; set; }
        //public int employeID { get; set; }
    }
}
