using BARBERSHOP_V2.DTO;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Repository.ExceptionRepo;
using BARBERSHOP_V2.Unit;
using Microsoft.AspNetCore.Mvc;

namespace BARBERSHOP_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkingHourController : BaseController
    {
        public WorkingHourController(UnitOfWork unitOfWork, IUniqueConstraintHandler uniqueConstraintHandler)
            : base(unitOfWork, uniqueConstraintHandler)
        {

        }

        [HttpGet]
        public IActionResult GetWorkingHour()
        {
            var workingHours = _unitOfWork.WorkingHourRepository.GetAll<WorkingHourDto>();
            return Ok(workingHours);
        }

        [HttpGet("{id}")]
        public IActionResult GetWorkingHour(int id)
        {
            var workingHours = _unitOfWork.WorkingHourRepository.GetById<WorkingHourDto>(id);

            if (workingHours == null)
                return NotFound();

            return Ok(workingHours);
        }

        [HttpPost]
        public IActionResult CreateWorkingHour(WorkingHourDto workingHoursModel)
        {
            if (workingHoursModel == null)
                return BadRequest();

            var workingHoursEntity = _unitOfWork.Mapper.Map<WorkingHour>(workingHoursModel);
            _unitOfWork.WorkingHourRepository.Add(workingHoursEntity);
            _unitOfWork.Commit();

            var workingHoursDto = _unitOfWork.Mapper.Map<WorkingHourDto>(workingHoursEntity);

            return CreatedAtAction(nameof(GetWorkingHour), new { id = workingHoursDto.workingHourID }, workingHoursDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateWorkingHour(int id, WorkingHourDto updatedWorkingHourModel)
        {
            var existingWorkingHourEntity = _unitOfWork.WorkingHourRepository.GetById<WorkingHourDto>(id);

            if (existingWorkingHourEntity == null)
                return NotFound();

            _unitOfWork.WorkingHourRepository.UpdateProperties(id, entity =>
            {
                entity.startTime = updatedWorkingHourModel.startTime;
                entity.endTime = updatedWorkingHourModel.endTime;
            });

            _unitOfWork.Commit();

            return Ok(new { message = "Cập nhật thành công" });
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteWorkingHour(int id)
        {
            var workingHoursEntity = _unitOfWork.WorkingHourRepository.GetById<WorkingHourDto>(id);

            if (workingHoursEntity == null)
                return NotFound();

            _unitOfWork.WorkingHourRepository.Delete(id);
            _unitOfWork.Commit();

            return Ok(new { message = "Xóa thành công" });
        }
    }
}
