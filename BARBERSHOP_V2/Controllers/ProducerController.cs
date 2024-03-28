using BARBERSHOP_V2.DTO;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Repository.ExceptionRepo;
using BARBERSHOP_V2.Unit;
using Microsoft.AspNetCore.Mvc;

namespace BARBERSHOP_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : BaseController
    {
        public ProducerController(UnitOfWork unitOfWork, IUniqueConstraintHandler uniqueConstraintHandler)
            : base(unitOfWork, uniqueConstraintHandler)
        {

        }

        [HttpGet]
        public IActionResult GetProducer()
        {
            var producer = _unitOfWork.ProducerRepository.GetAll<ProducerDto>();
            return Ok(producer);
        }

        [HttpGet("{id}")]
        public IActionResult GetProducer(int id)
        {
            var producer = _unitOfWork.ProducerRepository.GetById<ProducerDto>(id);

            if (producer == null)
                return NotFound();

            return Ok(producer);
        }

        [HttpPost]
        public IActionResult CreateProducer(ProducerDto producerModel)
        {
            if (producerModel == null)
                return BadRequest();

            var producerEntity = _unitOfWork.Mapper.Map<Producer>(producerModel);
            _unitOfWork.ProducerRepository.Add(producerEntity);
            _unitOfWork.Commit();

            var producerDto = _unitOfWork.Mapper.Map<ProducerDto>(producerEntity);

            return CreatedAtAction(nameof(GetProducer), new { id = producerDto.producerID }, producerDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProducer(int id, ProducerDto updatedProducerModel)
        {
            var existingProducerEntity = _unitOfWork.ProducerRepository.GetById<ProducerDto>(id);

            if (existingProducerEntity == null)
                return NotFound();

            _unitOfWork.ProducerRepository.UpdateProperties(id, entity =>
            {
                entity.producerName = updatedProducerModel.producerName;
                entity.numberphone = updatedProducerModel.numberphone;
                entity.addressID = updatedProducerModel.addressID;

            });

            _unitOfWork.Commit();

            return Ok(new { message = "Cập nhật thành công" });
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteProducer(int id)
        {
            var producerEntity = _unitOfWork.ProducerRepository.GetById<ProducerDto>(id);

            if (producerEntity == null)
                return NotFound();

            _unitOfWork.ProducerRepository.Delete(id);
            _unitOfWork.Commit();

            return Ok(new { message = "Xóa thành công" });
        }


        [HttpGet("search")]
        public IActionResult SearchProducers([FromQuery] string producerkey)
        {
            var produceres = _unitOfWork.ProducerRepository.Search<ProducerDto>(producer =>
                            producer.producerName != null &&
                            producer.producerName.Contains(producerkey) ||
                            producer.numberphone != null && 
                            producer.numberphone.Contains(producerkey));
            return Ok(produceres);
        }
    }
}
