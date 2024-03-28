using BARBERSHOP_V2.DTO;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Repository.ExceptionRepo;
using BARBERSHOP_V2.Service.ExceptionService;
using BARBERSHOP_V2.Unit;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BARBERSHOP_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        public CategoryController(UnitOfWork unitOfWork, IUniqueConstraintHandler uniqueConstraintHandler)
            : base(unitOfWork, uniqueConstraintHandler)
        {

        }

        [HttpGet]
        public IActionResult GetCategory()
        {
            var category = _unitOfWork.CategoryRepository.GetAll<CategoryDto>();
            return Ok(category);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetById<CategoryDto>(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryDto categoryModel)
        {
            try
            {
                if (categoryModel == null)
                    return BadRequest();

                var categoryEntity = _unitOfWork.Mapper.Map<Category>(categoryModel);
                _unitOfWork.CategoryRepository.Add(categoryEntity);
                _unitOfWork.Commit();

                var categoryDto = _unitOfWork.Mapper.Map<CategoryDto>(categoryEntity);

                return CreatedAtAction(nameof(GetCategory), new { id = categoryDto.cateID }, categoryDto);
            }
            catch (Exception e)
            {
                if (_uniqueConstraintHandler.IsUniqueConstraintViolation(e))
                {
                    Log.Error(e, "Vi phạm trùng lặp!");
                    return BadRequest(new { ErrorMessage = "Vi phạm trùng lặp!", ErrorCode = "DUPLICATE_KEY" });
                }
                else
                {
                    return StatusCode(500, "Internal Server Error");
                }
            }
            
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, CategoryDto updatedCategoryModel)
        {
            try
            {
                var existingCategoryEntity = _unitOfWork.CategoryRepository.GetById<CategoryDto>(id);

                if (existingCategoryEntity == null)
                    return NotFound();

                _unitOfWork.CategoryRepository.UpdateProperties(id, entity =>
                {
                    entity.cateName = existingCategoryEntity.cateName;
                });

                _unitOfWork.Commit();

                return Ok(new { message = "Cập nhật thành công" });
            }
            catch (Exception e)
            {
                if (_uniqueConstraintHandler.IsUniqueConstraintViolation(e))
                {
                    Log.Error(e, "Vi phạm trùng lặp!");
                    return BadRequest(new { ErrorMessage = "Vi phạm trùng lặp!", ErrorCode = "DUPLICATE_KEY" });
                }
                else
                {
                    return StatusCode(500, "Internal Server Error");
                }
            }

        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var categoryEntity = _unitOfWork.CategoryRepository.GetById<CategoryDto>(id);

            if (categoryEntity == null)
                return NotFound();

            _unitOfWork.CategoryRepository.Delete(id);
            _unitOfWork.Commit();

            return Ok(new { message = "Xóa thành công" });
        }


        [HttpGet("search")]
        public IActionResult SearchCategorys([FromQuery] string categorykey)
        {
            var categoryes = _unitOfWork.CategoryRepository.Search<CategoryDto>(category =>
                            category.cateName != null &&
                            category.cateName.Contains(categorykey));
            return Ok(categoryes);
        }
    }
}
