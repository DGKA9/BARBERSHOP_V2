using BARBERSHOP_V2.DTO;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Repository.ExceptionRepo;
using BARBERSHOP_V2.Unit;
using Microsoft.AspNetCore.Mvc;

namespace BARBERSHOP_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressController : BaseController
    {
        public CustomerAddressController(UnitOfWork unitOfWork, IUniqueConstraintHandler uniqueConstraintHandler)
            : base(unitOfWork, uniqueConstraintHandler)
        {

        }

        [HttpGet]
        public IActionResult GetCustomerAddress()
        {
            var customerAddress = _unitOfWork.CustomerAddressRepository.GetAll<CustomerAddressDto>();
            return Ok(customerAddress);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerAddress(int id)
        {
            var customerAddress = _unitOfWork.CustomerAddressRepository.GetById<CustomerAddressDto>(id);

            if (customerAddress == null)
                return NotFound();

            return Ok(customerAddress);
        }

        [HttpPost]
        public IActionResult CreateCustomerAddress(CustomerAddressDto customerAddressModel)
        {
            if (customerAddressModel == null)
                return BadRequest();

            var customerAddressEntity = _unitOfWork.Mapper.Map<CustomerAddress>(customerAddressModel);
            _unitOfWork.CustomerAddressRepository.Add(customerAddressEntity);
            _unitOfWork.Commit();

            var customerAddressDto = _unitOfWork.Mapper.Map<CustomerAddressDto>(customerAddressEntity);

            return CreatedAtAction(nameof(GetCustomerAddress), new { id = customerAddressDto.cusAddressId }, customerAddressDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomerAddress(int id, CustomerAddressDto updatedCustomerAddressModel)
        {
            var existingCustomerAddressEntity = _unitOfWork.CustomerAddressRepository.GetById<CustomerAddressDto>(id);

            if (existingCustomerAddressEntity == null)
                return NotFound();

            _unitOfWork.CustomerAddressRepository.UpdateProperties(id, entity =>
            {
                entity.Customer.customerID = updatedCustomerAddressModel.customerID;
                entity.Address.addressID = updatedCustomerAddressModel.addressID;

            });

            _unitOfWork.Commit();

            return Ok(new { message = "Cập nhật thành công" });
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCustomerAddress(int id)
        {
            var customerAddressEntity = _unitOfWork.CustomerAddressRepository.GetById<CustomerAddressDto>(id);

            if (customerAddressEntity == null)
                return NotFound();

            _unitOfWork.CustomerAddressRepository.Delete(id);
            _unitOfWork.Commit();

            return Ok(new { message = "Xóa thành công" });
        }
    }
}
