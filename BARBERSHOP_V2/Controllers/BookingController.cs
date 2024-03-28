using System.Security.Claims;
using BARBERSHOP_V2.DTO;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Repository.ExceptionRepo;
using BARBERSHOP_V2.Unit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BARBERSHOP_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : BaseController
    {
        public BookingController(UnitOfWork unitOfWork, IUniqueConstraintHandler uniqueConstraintHandler)
            : base(unitOfWork, uniqueConstraintHandler)
        {

        }

        [HttpGet]
        public IActionResult GetBooking()
        {
            var booking = _unitOfWork.BookingRepository.GetAll<BookingDto>();
            return Ok(booking);
        }

        [HttpGet("{id}")]
        public IActionResult GetBooking(int id)
        {
            var booking = _unitOfWork.BookingRepository.GetById<BookingDto>(id);

            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        [HttpPost]
        public IActionResult CreateBooking(BookingDto bookingModel)
        {
            if (bookingModel == null)
                return BadRequest();

            var bookingEntity = _unitOfWork.Mapper.Map<Booking>(bookingModel);
            _unitOfWork.BookingRepository.Add(bookingEntity);
            _unitOfWork.Commit();

            var bookingDto = _unitOfWork.Mapper.Map<BookingDto>(bookingEntity);

            return CreatedAtAction(nameof(GetBooking), new { id = bookingDto.bookingID }, bookingDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBooking(int id, BookingDto updatedBookingModel)
        {
            var existingBookingEntity = _unitOfWork.BookingRepository.GetById<BookingDto>(id);

            if (existingBookingEntity == null)
                return NotFound();

            _unitOfWork.BookingRepository.UpdateProperties(id, entity =>
            {
                entity.startTime = updatedBookingModel.startTime;
                entity.customer.customerID = updatedBookingModel.customerID;
                //entity.payment.payID = updatedBookingModel.payID;

            });

            _unitOfWork.Commit();

            return Ok(new { message = "Cập nhật thành công" });
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            var bookingEntity = _unitOfWork.BookingRepository.GetById<BookingDto>(id);

            if (bookingEntity == null)
                return NotFound();

            _unitOfWork.BookingRepository.Delete(id);
            _unitOfWork.Commit();

            return Ok(new { message = "Xóa thành công" });
        }



        [HttpGet("userinfo")]
        [Authorize]
        public IActionResult GetUserInfo()
        {
            try
            {
                var userId = User.FindFirst("userID")?.Value;
                var userName = User.FindFirst(ClaimTypes.Name)?.Value;
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
                

                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userRole) || string.IsNullOrEmpty(userId))
                {
                    return BadRequest("Thông tin người dùng không hợp lệ.");
                }

                return Ok(new { UserId = userId,  UserName = userName, UserRole = userRole});
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Đã xảy ra lỗi: {ex.Message}");
            }
        }
    }
}
