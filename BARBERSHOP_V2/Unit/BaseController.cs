using BARBERSHOP_V2.Repository.ExceptionRepo;
using Microsoft.AspNetCore.Mvc;

namespace BARBERSHOP_V2.Unit
{
    public class BaseController : ControllerBase
    {
        protected readonly UnitOfWork _unitOfWork;
        protected readonly IUniqueConstraintHandler _uniqueConstraintHandler;

        public BaseController(UnitOfWork unitOfWork, IUniqueConstraintHandler uniqueConstraintHandler)
        {
            _unitOfWork = unitOfWork;
            _uniqueConstraintHandler = uniqueConstraintHandler;
        }

    }
}
