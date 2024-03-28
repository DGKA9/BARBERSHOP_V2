using BARBERSHOP_V2.DTO;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Repository.ExceptionRepo;
using BARBERSHOP_V2.Unit;
using Microsoft.AspNetCore.Mvc;

namespace BARBERSHOP_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluateController : BaseController
    {
        public EvaluateController(UnitOfWork unitOfWork, IUniqueConstraintHandler uniqueConstraintHandler)
            : base(unitOfWork, uniqueConstraintHandler)
        {

        }

        [HttpGet]
        public IActionResult GetEvaluates()
        {
            var evaluate = _unitOfWork.EvaluateRepository.GetAll<EvaluateDto>();
            return Ok(evaluate);
        }

        [HttpGet("{id}")]
        public IActionResult GetEvaluate(int id)
        {
            var evaluate = _unitOfWork.EvaluateRepository.GetById<EvaluateDto>(id);

            if (evaluate == null)
                return NotFound();

            return Ok(evaluate);
        }

        [HttpPost]
        public IActionResult CreateEvaluate(EvaluateDto evaluateModel)
        {
            if (evaluateModel == null)
                return BadRequest();

            var evaluateEntity = _unitOfWork.Mapper.Map<Evaluate>(evaluateModel);
            _unitOfWork.EvaluateRepository.Add(evaluateEntity);
            _unitOfWork.Commit();

            var evaluateDto = _unitOfWork.Mapper.Map<EvaluateDto>(evaluateEntity);

            return CreatedAtAction(nameof(GetEvaluate), new { id = evaluateDto.evaluateID }, evaluateDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEvaluate(int id, EvaluateDto updatedEvaluateModel)
        {
            var existingEvaluateEntity = _unitOfWork.EvaluateRepository.GetById<EvaluateDto>(id);

            if (existingEvaluateEntity == null)
                return NotFound();

            _unitOfWork.EvaluateRepository.UpdateProperties(id, entity =>
            {
                entity.rating = updatedEvaluateModel.rating;
                entity.comment = updatedEvaluateModel.comment;
                entity.customer.customerID = updatedEvaluateModel.customerID;
                entity.storeID = updatedEvaluateModel.storeID;
            });

            _unitOfWork.Commit();

            return Ok(new { message = "Cập nhật thành công" });
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteEvaluate(int id)
        {
            var evaluateEntity = _unitOfWork.EvaluateRepository.GetById<EvaluateDto>(id);

            if (evaluateEntity == null)
                return NotFound();

            _unitOfWork.EvaluateRepository.Delete(id);
            _unitOfWork.Commit();

            return Ok(new { message = "Xóa thành công" });
        }


        [HttpGet("search")]
        public IActionResult SearchEvaluates([FromQuery] string evaluateKey)
        {
            var evaluate = _unitOfWork.EvaluateRepository.Search<EvaluateDto>(evaluate => 
                            evaluate.customer.firstName != null &&
                            evaluate.customer.firstName.Contains(evaluateKey) ||
                            evaluate.customer.lastName != null && evaluate.customer.lastName.Contains(evaluateKey));

            if (evaluate != null)
            {
                return Ok(evaluate);
            }
            else
            {
                return Ok(new { message = "Không tìm thấy chuỗi cần tìm" });
            }
        }
    }
}
