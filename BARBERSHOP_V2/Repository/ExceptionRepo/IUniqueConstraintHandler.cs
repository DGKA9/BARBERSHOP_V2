using Microsoft.EntityFrameworkCore;

namespace BARBERSHOP_V2.Repository.ExceptionRepo
{
    public interface IUniqueConstraintHandler
    {
        bool IsUniqueConstraintViolation(Exception ex);
    }
}
