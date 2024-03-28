using BARBERSHOP_V2.DTO;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Repository.ExceptionRepo;
using BARBERSHOP_V2.Unit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BARBERSHOP_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceEmployeeController : BaseController
    {
        public ServiceEmployeeController(UnitOfWork unitOfWork, IUniqueConstraintHandler uniqueConstraintHandler)
            : base(unitOfWork, uniqueConstraintHandler)
        {

        }

        [HttpGet]
        public IActionResult GetServiceEmployee()
        {
            var serviceEmployee = _unitOfWork.ServiceEmployeeRepository.GetAll<ServiceEmployeeDto>();
            return Ok(serviceEmployee);
        }

        [HttpGet("{id}")]
        public IActionResult GetServiceEmployee(int id)
        {
            var serviceEmployee = _unitOfWork.ServiceEmployeeRepository.GetById<ServiceEmployeeDto>(id);

            if (serviceEmployee == null)
                return NotFound();

            return Ok(serviceEmployee);
        }

        [HttpPost]
        public IActionResult CreateServiceEmployee(ServiceEmployeeDto serviceEmployeeModel)
        {
            if (serviceEmployeeModel == null)
                return BadRequest();

            var serviceEmployeeEntity = _unitOfWork.Mapper.Map<ServiceEmployee>(serviceEmployeeModel);
            _unitOfWork.ServiceEmployeeRepository.Add(serviceEmployeeEntity);
            _unitOfWork.Commit();

            var serviceEmployeeDto = _unitOfWork.Mapper.Map<ServiceEmployeeDto>(serviceEmployeeEntity);

            return CreatedAtAction(nameof(GetServiceEmployee), serviceEmployeeDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateServiceEmployee(int id, ServiceEmployeeDto updatedServiceEmployeeModel)
        {
            var existingServiceEmployeeEntity = _unitOfWork.ServiceEmployeeRepository.GetById<ServiceEmployeeDto>(id);

            if (existingServiceEmployeeEntity == null)
                return NotFound();

            _unitOfWork.ServiceEmployeeRepository.UpdateProperties(id, entity =>
            {
                entity.serID = updatedServiceEmployeeModel.serID;
                entity.employeeID = updatedServiceEmployeeModel.employeeID;

            });

            _unitOfWork.Commit();

            return Ok(new { message = "Cập nhật thành công" });
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteServiceEmployee(int id)
        {
            var serviceEmployeeEntity = _unitOfWork.ServiceEmployeeRepository.GetById<ServiceEmployeeDto>(id);

            if (serviceEmployeeEntity == null)
                return NotFound();

            _unitOfWork.ServiceEmployeeRepository.Delete(id);
            _unitOfWork.Commit();

            return Ok(new { message = "Xóa thành công" });
        }

    }
}
