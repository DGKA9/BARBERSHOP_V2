namespace BARBERSHOP_V2.DTO
{
    public class NotificationDto
    {
        public int notiID { get; set; }
        public string? notiTitle { get; set; }
        public string? notiContent { get; set; }
        public DateTime lastUpdate { get; set; } = DateTime.Now;
    }
}
