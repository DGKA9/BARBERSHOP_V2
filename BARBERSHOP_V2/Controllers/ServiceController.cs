using BARBERSHOP_V2.DTO;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Repository.ExceptionRepo;
using BARBERSHOP_V2.Unit;
using Microsoft.AspNetCore.Mvc;

namespace BARBERSHOP_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : BaseController
    {
        public ServiceController(UnitOfWork unitOfWork, IUniqueConstraintHandler uniqueConstraintHandler)
            : base(unitOfWork, uniqueConstraintHandler)
        {

        }

        [HttpGet]
        public IActionResult GetService()
        {
            var service = _unitOfWork.ServiceRepository.GetAll<ServicesDto>();
            return Ok(service);
        }

        [HttpGet("{id}")]
        public IActionResult GetService(int id)
        {
            var service = _unitOfWork.ServiceRepository.GetById<ServicesDto>(id);

            if (service == null)
                return NotFound();

            return Ok(service);
        }

        [HttpPost]
        public IActionResult CreateService(ServicesDto serviceModel)
        {
            if (serviceModel == null)
                return BadRequest();

            var serviceEntity = _unitOfWork.Mapper.Map<Services>(serviceModel);
            _unitOfWork.ServiceRepository.Add(serviceEntity);
            _unitOfWork.Commit();

            var serviceDto = _unitOfWork.Mapper.Map<ServicesDto>(serviceEntity);

            return CreatedAtAction(nameof(GetService), new { id = serviceDto.serID }, serviceDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateService(int id, ServicesDto updatedServiceModel)
        {
            var existingServiceEntity = _unitOfWork.ServiceRepository.GetById<ServicesDto>(id);

            if (existingServiceEntity == null)
                return NotFound();

            _unitOfWork.ServiceRepository.UpdateProperties(id, entity =>
            {
                entity.serName = updatedServiceModel.serName;
                entity.serDescription = updatedServiceModel.serDescription;
                entity.serPrice = updatedServiceModel.serPrice; 
                entity.serTime = updatedServiceModel.serTime;
                entity.ServiceCategory.serCateID = updatedServiceModel.serCateID;
                

            });

            _unitOfWork.Commit();

            return Ok(new { message = "Cập nhật thành công" });
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteService(int id)
        {
            var serviceEntity = _unitOfWork.ServiceRepository.GetById<ServicesDto>(id);

            if (serviceEntity == null)
                return NotFound();

            _unitOfWork.ServiceRepository.Delete(id);
            _unitOfWork.Commit();

            return Ok(new { message = "Xóa thành công" });
        }


        [HttpGet("search")]
        public IActionResult SearchServices([FromQuery] string servicekey)
        {
            var servicees = _unitOfWork.ServiceRepository.Search<ServicesDto>(service =>
                            service.serName != null &&
                            service.serName.Contains(servicekey));
            return Ok(servicees);
        }
    }
}
