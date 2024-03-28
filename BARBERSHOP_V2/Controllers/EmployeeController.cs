using BARBERSHOP_V2.DTO;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Repository.ExceptionRepo;
using BARBERSHOP_V2.Unit;
using Microsoft.AspNetCore.Mvc;

namespace BARBERSHOP_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController
    {
        public EmployeeController(UnitOfWork unitOfWork, IUniqueConstraintHandler uniqueConstraintHandler)
            : base(unitOfWork, uniqueConstraintHandler)
        {

        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employee = _unitOfWork.EmployeeRepository.GetAll<EmployeeDto>();
            return Ok(employee);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById<EmployeeDto>(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public IActionResult CreateEmployee(EmployeeDto employeeModel)
        {
            if (employeeModel == null)
                return BadRequest();

            var employeeEntity = _unitOfWork.Mapper.Map<Employee>(employeeModel);
            _unitOfWork.EmployeeRepository.Add(employeeEntity);
            _unitOfWork.Commit();

            var employeeDto = _unitOfWork.Mapper.Map<EmployeeDto>(employeeEntity);

            return CreatedAtAction(nameof(GetEmployee), new { id = employeeDto.employeeID }, employeeDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, EmployeeDto updatedEmployeeModel)
        {
            var existingEmployeeEntity = _unitOfWork.EmployeeRepository.GetById<EmployeeDto>(id);

            if (existingEmployeeEntity == null)
                return NotFound();

            _unitOfWork.EmployeeRepository.UpdateProperties(id, entity =>
            {
                entity.firstName = updatedEmployeeModel.firstName;
                entity.lastName = updatedEmployeeModel.lastName;
                entity.picture = updatedEmployeeModel.picture;
                entity.email = updatedEmployeeModel.email;
                entity.numberphone = updatedEmployeeModel.numberphone;
                entity.dateOfBirth = updatedEmployeeModel.dateOfBirth;
                entity.wordDay = updatedEmployeeModel.wordDay;
                entity.userID = updatedEmployeeModel.userID;
                entity.storeID = updatedEmployeeModel.storeID;
                entity.addressID = updatedEmployeeModel.addressID;
            });

            _unitOfWork.Commit();

            return Ok(new { message = "Cập nhật thành công" });
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employeeEntity = _unitOfWork.EmployeeRepository.GetById<EmployeeDto>(id);

            if (employeeEntity == null)
                return NotFound();

            _unitOfWork.EmployeeRepository.Delete(id);
            _unitOfWork.Commit();

            return Ok(new { message = "Xóa thành công" });
        }


        [HttpGet("search")]
        public IActionResult SearchEmployees([FromQuery] string employeeKey)
        {
            var employee = _unitOfWork.EmployeeRepository.Search<EmployeeDto>(employee =>
                            employee.firstName != null &&
                            employee.firstName.Contains(employeeKey) ||
                            employee.lastName != null && employee.lastName.Contains(employeeKey) ||
                            employee.email != null && employee.email.Contains(employeeKey) ||
                            employee.numberphone.ToString() != null && employee.numberphone.ToString().Contains(employeeKey));

            if (employee != null)
            {
                return Ok(employee);
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy chuỗi cần tìm" });
            }
        }
    }
}
