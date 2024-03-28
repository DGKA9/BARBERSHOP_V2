using BARBERSHOP_V2.DTO;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Repository.ExceptionRepo;
using BARBERSHOP_V2.Unit;
using Microsoft.AspNetCore.Mvc;

namespace BARBERSHOP_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCategoryController : BaseController
    {
        public ServiceCategoryController(UnitOfWork unitOfWork, IUniqueConstraintHandler uniqueConstraintHandler)
            : base(unitOfWork, uniqueConstraintHandler)
        {

        }

        [HttpGet]
        public IActionResult GetServiceCategory()
        {
            var serviceCategory = _unitOfWork.ServiceCategoryRepository.GetAll<ServiceCategoryDto>();
            return Ok(serviceCategory);
        }

        [HttpGet("{id}")]
        public IActionResult GetServiceCategory(int id)
        {
            var serviceCategory = _unitOfWork.ServiceCategoryRepository.GetById<ServiceCategoryDto>(id);

            if (serviceCategory == null)
                return NotFound();

            return Ok(serviceCategory);
        }

        [HttpPost]
        public IActionResult CreateServiceCategory(ServiceCategoryDto serviceCategoryModel)
        {
            if (serviceCategoryModel == null)
                return BadRequest();

            var serviceCategoryEntity = _unitOfWork.Mapper.Map<ServiceCategory>(serviceCategoryModel);
            _unitOfWork.ServiceCategoryRepository.Add(serviceCategoryEntity);
            _unitOfWork.Commit();

            var serviceCategoryDto = _unitOfWork.Mapper.Map<ServiceCategoryDto>(serviceCategoryEntity);

            return CreatedAtAction(nameof(GetServiceCategory), new { id = serviceCategoryDto.serCateID }, serviceCategoryDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateServiceCategory(int id, ServiceCategoryDto updatedServiceCategoryModel)
        {
            var existingServiceCategoryEntity = _unitOfWork.ServiceCategoryRepository.GetById<ServiceCategoryDto>(id);

            if (existingServiceCategoryEntity == null)
                return NotFound();

            _unitOfWork.ServiceCategoryRepository.UpdateProperties(id, entity =>
            {
                entity.serCateName = updatedServiceCategoryModel.serCateName;
                entity.description = updatedServiceCategoryModel.description;
            });

            _unitOfWork.Commit();

            return Ok(new { message = "Cập nhật thành công" });
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteServiceCategory(int id)
        {
            var serviceCategoryEntity = _unitOfWork.ServiceCategoryRepository.GetById<ServiceCategoryDto>(id);

            if (serviceCategoryEntity == null)
                return NotFound();

            _unitOfWork.ServiceCategoryRepository.Delete(id);
            _unitOfWork.Commit();

            return Ok(new { message = "Xóa thành công" });
        }


        [HttpGet("search")]
        public IActionResult SearchServiceCategorys([FromQuery] string serviceCategorykey)
        {
            var serviceCategoryes = _unitOfWork.ServiceCategoryRepository.Search<ServiceCategoryDto>(serviceCategory =>
                            serviceCategory.serCateName != null &&
                            serviceCategory.serCateName.Contains(serviceCategorykey));
            return Ok(serviceCategoryes);
        }
    }
}
