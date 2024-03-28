using BARBERSHOP_V2.Repository.ExceptionRepo;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BARBERSHOP_V2.Service.ExceptionService
{
    public class UniqueConstraintService : IUniqueConstraintHandler
    {
        public bool IsUniqueConstraintViolation(Exception ex)
        {
            const int uniqueKeyViolationErrorNumber = 2601;

            var sqlException = ex.InnerException as SqlException;
            return sqlException != null && (sqlException.Number == uniqueKeyViolationErrorNumber);
        }
    }
}
