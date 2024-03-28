using BARBERSHOP_V2.DTO;

namespace BARBERSHOP_V2.Service.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);
    }
}
