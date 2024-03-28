namespace BARBERSHOP_V2.DTO
{
    public class CustomerDto
    {
        public int customerID { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? picture { get; set; }
        public string? email { get; set; }
        public string? numberphone { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int userID { get; set; }
    }
}
