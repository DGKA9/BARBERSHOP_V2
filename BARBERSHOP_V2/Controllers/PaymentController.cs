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
    public class PaymentController : BaseController
    {
        public PaymentController(UnitOfWork unitOfWork, IUniqueConstraintHandler uniqueConstraintHandler)
            : base(unitOfWork, uniqueConstraintHandler)
        {

        }

        [HttpGet]
        public IActionResult GetPayment()
        {
            var payment = _unitOfWork.PaymentRepository.GetAll<PaymentDto>();
            return Ok(payment);
        }

        [HttpGet("{id}")]
        public IActionResult GetPayment(int id)
        {
            var payment = _unitOfWork.PaymentRepository.GetById<PaymentDto>(id);

            if (payment == null)
                return NotFound();

            return Ok(payment);
        }

        [HttpPost]
        [Authorize( Roles = "1")]
        public IActionResult CreatePayment(PaymentDto paymentModel)
        {
            if (paymentModel == null)
                return BadRequest();

            var paymentEntity = _unitOfWork.Mapper.Map<Payment>(paymentModel);
            _unitOfWork.PaymentRepository.Add(paymentEntity);
            _unitOfWork.Commit();

            var paymentDto = _unitOfWork.Mapper.Map<PaymentDto>(paymentEntity);

            return CreatedAtAction(nameof(GetPayment), new { id = paymentDto.payID }, paymentDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePayment(int id, PaymentDto updatedPaymentModel)
        {
            var existingPaymentEntity = _unitOfWork.PaymentRepository.GetById<PaymentDto>(id);

            if (existingPaymentEntity == null)
                return NotFound();

            _unitOfWork.PaymentRepository.UpdateProperties(id, entity =>
            {
                entity.payMethod = updatedPaymentModel.payMethod;
                entity.payStatus = updatedPaymentModel.payStatus;
            });

            _unitOfWork.Commit();

            return Ok(new { message = "Cập nhật thành công" });
        }


        [HttpDelete("{id}")]
        public IActionResult DeletePayment(int id)
        {
            var paymentEntity = _unitOfWork.PaymentRepository.GetById<PaymentDto>(id);

            if (paymentEntity == null)
                return NotFound();

            _unitOfWork.PaymentRepository.Delete(id);
            _unitOfWork.Commit();

            return Ok(new { message = "Xóa thành công" });
        }
    }
}
