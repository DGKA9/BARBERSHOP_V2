using BARBERSHOP_V2.Data;
using BARBERSHOP_V2.DTO;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Repository.ExceptionRepo;
using BARBERSHOP_V2.Service;
using BARBERSHOP_V2.Service.EmailService;
using BARBERSHOP_V2.Unit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Text;

namespace BARBERSHOP_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingServiceController : BaseController
    {
        private readonly BarberShopContext _context;
        readonly EndTimeForBookingService _endTimeForBookingService;
        private readonly IEmailService _emailService;
        public BookingServiceController(BarberShopContext context, EndTimeForBookingService endTimeForBookingService, UnitOfWork unitOfWork, IUniqueConstraintHandler uniqueConstraintHandler, IEmailService emailService)
            : base(unitOfWork, uniqueConstraintHandler)
        {
            _context = context;
            _endTimeForBookingService = endTimeForBookingService;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult GetBookingService()
        {
            var bookingService = _unitOfWork.BookingServiceRepository.GetAll<BookingServiceDto>();
            return Ok(bookingService);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookingService(int id)
        {
            var bookingService = _unitOfWork.BookingServiceRepository.GetById<BookingServiceDto>(id);

            if (bookingService == null)
                return NotFound();

            return Ok(bookingService);
        }

        [HttpPost]
        public IActionResult CreateBookingService(BookingServiceDto bookingServiceModel)
        {
            if (bookingServiceModel == null)
                return BadRequest();

            var bookingServiceEntity = _unitOfWork.Mapper.Map<BookingService>(bookingServiceModel);

            _unitOfWork.BookingServiceRepository.Add(bookingServiceEntity);
            _unitOfWork.Commit();

            _endTimeForBookingService.UpdateBookingEndTime(bookingServiceEntity.bookingID);

            StringBuilder emailBodyBuilder = new StringBuilder();
            var booking = _unitOfWork.BookingServiceRepository.GetAll<BookingService>()
                .FirstOrDefault();

            if (booking != null && booking.Booking != null)
            {
                var customer = _unitOfWork.CustomerRepository.GetAll<Customer>()
                    .FirstOrDefault(c => c.customerID == booking.Booking.customerID);
                Console.WriteLine($"Service ID: {booking.Service?.serID}, Service Name: {booking.Service?.serName}");
                Console.WriteLine($"Employee ID: {booking.Employee?.employeeID}, Employee First Name: {booking.Employee?.firstName}");
                if (customer != null)
                {
                    DateTime dateFounded = booking.Booking.dateFounded;
                    TimeSpan startTime = booking.Booking.startTime;

                    string serviceName = booking.Service.serName ?? "Dịch vụ không xác định";
                    string employeeFirstName = booking.Employee.firstName ?? "Nhân viên không xác định";

                    emailBodyBuilder.AppendLine(
                        $"Hệ thống đã ghi nhận lịch đặt của bạn vào ngày {dateFounded} bắt đầu lúc {startTime}.");
                    emailBodyBuilder.AppendLine(
                        $"Dịch vụ: {serviceName}, Phụ trách bởi: {employeeFirstName} {bookingServiceEntity.Employee?.lastName}");

                    _emailService.SendEmail(new EmailDto
                    {
                        To = booking.Booking.customer.email,
                        Subject = "Đặt lịch thành công",
                        Body = emailBodyBuilder.ToString()
                    });
                }
            }

            var bookingServiceDto = _unitOfWork.Mapper.Map<BookingServiceDto>(bookingServiceEntity);

            return CreatedAtAction(nameof(GetBookingService), bookingServiceDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBookingService(int id, BookingServiceDto updatedBookingServiceModel)
        {
            var existingBookingServiceEntity = _unitOfWork.BookingServiceRepository.GetById<BookingServiceDto>(id);

            if (existingBookingServiceEntity == null)
                return NotFound();

            _unitOfWork.BookingServiceRepository.UpdateProperties(id, entity =>
            {
                entity.bookingID = updatedBookingServiceModel.bookingID;
                entity.serID = updatedBookingServiceModel.serID;
            });

            _unitOfWork.Commit();

            return Ok(new { message = "Cập nhật thành công" });
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBookingService(int id)
        {
            var bookingServiceEntity = _unitOfWork.BookingServiceRepository.GetById<BookingServiceDto>(id);

            if (bookingServiceEntity == null)
                return NotFound();

            _unitOfWork.BookingServiceRepository.Delete(id);
            _unitOfWork.Commit();

            return Ok(new { message = "Xóa thành công" });
        }
    }
}
