using BARBERSHOP_V2.DTO;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Repository.ExceptionRepo;
using BARBERSHOP_V2.Unit;
using Microsoft.AspNetCore.Mvc;

namespace BARBERSHOP_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        public CustomerController(UnitOfWork unitOfWork, IUniqueConstraintHandler uniqueConstraintHandler)
            : base(unitOfWork, uniqueConstraintHandler)
        {

        }

        [HttpGet]
        public IActionResult GetCustomer()
        {
            var customer = _unitOfWork.CustomerRepository.GetAll<CustomerDto>();
            return Ok(customer);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = _unitOfWork.CustomerRepository.GetById<CustomerDto>(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public IActionResult CreateCustomer(CustomerDto customerModel)
        {
            if (customerModel == null)
                return BadRequest();

            var customerEntity = _unitOfWork.Mapper.Map<Customer>(customerModel);
            _unitOfWork.CustomerRepository.Add(customerEntity);
            _unitOfWork.Commit();

            var customerDto = _unitOfWork.Mapper.Map<CustomerDto>(customerEntity);

            return CreatedAtAction(nameof(GetCustomer), new { id = customerDto.customerID }, customerDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, CustomerDto updatedCustomerModel)
        {
            var existingCustomerEntity = _unitOfWork.CustomerRepository.GetById<CustomerDto>(id);

            if (existingCustomerEntity == null)
                return NotFound();

            _unitOfWork.CustomerRepository.UpdateProperties(id, entity =>
            {
                entity.firstName = updatedCustomerModel.firstName;
                entity.lastName = updatedCustomerModel.lastName;
                entity.picture = updatedCustomerModel.picture;
                entity.email = updatedCustomerModel.email;
                entity.numberphone = updatedCustomerModel.numberphone;
                entity.dateOfBirth = updatedCustomerModel.dateOfBirth;
                entity.userID = updatedCustomerModel.userID;

            });

            _unitOfWork.Commit();

            return Ok(new { message = "Cập nhật thành công" });
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customerEntity = _unitOfWork.CustomerRepository.GetById<CustomerDto>(id);

            if (customerEntity == null)
                return NotFound();

            _unitOfWork.CustomerRepository.Delete(id);
            _unitOfWork.Commit();

            return Ok(new { message = "Xóa thành công" });
        }


        [HttpGet("search")]
        public IActionResult SearchCustomers([FromQuery] string customerkey)
        {
            var customeres = _unitOfWork.CustomerRepository.Search<CustomerDto>(customer =>
                            customer.firstName != null &&
                            customer.firstName.Contains(customerkey) ||
                            customer.lastName != null && customer.lastName.Contains(customerkey) ||
                            customer.email != null && customer.email.Contains(customerkey));
            return Ok(customeres);
        }
    }
}
