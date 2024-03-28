using BARBERSHOP_V2.DTO;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Repository.ExceptionRepo;
using BARBERSHOP_V2.Unit;
using Microsoft.AspNetCore.Mvc;

namespace BARBERSHOP_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingStateDescriptionController : BaseController
    {
        public BookingStateDescriptionController(UnitOfWork unitOfWork, IUniqueConstraintHandler uniqueConstraintHandler)
            : base(unitOfWork, uniqueConstraintHandler)
        {

        }

        [HttpGet]
        public IActionResult GetBookingStateDescription()
        {
            var bookingStateDescription = _unitOfWork.BookingStateDescriptionRepository.GetAll<BookingStateDescriptionDto>();
            return Ok(bookingStateDescription);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookingStateDescription(int id)
        {
            var bookingStateDescription = _unitOfWork.BookingStateDescriptionRepository.GetById<BookingStateDescriptionDto>(id);

            if (bookingStateDescription == null)
                return NotFound();

            return Ok(bookingStateDescription);
        }

        [HttpPost]
        public IActionResult CreateBookingStateDescription(BookingStateDescriptionDto bookingStateDescriptionModel)
        {
            if (bookingStateDescriptionModel == null)
                return BadRequest();

            var bookingStateDescriptionEntity = _unitOfWork.Mapper.Map<BookingStateDescription>(bookingStateDescriptionModel);
            _unitOfWork.BookingStateDescriptionRepository.Add(bookingStateDescriptionEntity);
            _unitOfWork.Commit();

            var bookingStateDescriptionDto = _unitOfWork.Mapper.Map<BookingStateDescriptionDto>(bookingStateDescriptionEntity);

            return CreatedAtAction(nameof(GetBookingStateDescription), new { id = bookingStateDescriptionDto.stateID }, bookingStateDescriptionDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBookingStateDescription(int id, BookingStateDescriptionDto updatedBookingStateDescriptionModel)
        {
            var existingBookingStateDescriptionEntity = _unitOfWork.BookingStateDescriptionRepository.GetById<BookingStateDescriptionDto>(id);

            if (existingBookingStateDescriptionEntity == null)
                return NotFound();

            _unitOfWork.BookingStateDescriptionRepository.UpdateProperties(id, entity =>
            {
                entity.description = updatedBookingStateDescriptionModel.description;
                entity.bookingID = updatedBookingStateDescriptionModel.bookingID;
            });

            _unitOfWork.Commit();

            return Ok(new { message = "Cập nhật thành công" });
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBookingStateDescription(int id)
        {
            var bookingStateDescriptionEntity = _unitOfWork.BookingStateDescriptionRepository.GetById<BookingStateDescriptionDto>(id);

            if (bookingStateDescriptionEntity == null)
                return NotFound();

            _unitOfWork.BookingStateDescriptionRepository.Delete(id);
            _unitOfWork.Commit();

            return Ok(new { message = "Xóa thành công" });
        }
    }
}
