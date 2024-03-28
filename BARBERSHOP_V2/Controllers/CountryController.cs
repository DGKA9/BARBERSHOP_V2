using BARBERSHOP_V2.DTO;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Repository.ExceptionRepo;
using BARBERSHOP_V2.Unit;
using Microsoft.AspNetCore.Mvc;

namespace BARBERSHOP_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : BaseController
    {
        public CountryController(UnitOfWork unitOfWork, IUniqueConstraintHandler uniqueConstraintHandler)
            : base(unitOfWork, uniqueConstraintHandler)
        {

        }

        [HttpGet]
        public IActionResult GetCountry()
        {
            var country = _unitOfWork.CountryRepository.GetAll<CountryDto>();
            return Ok(country);
        }

        [HttpGet("{id}")]
        public IActionResult GetCountry(int id)
        {
            var country = _unitOfWork.CountryRepository.GetById<CountryDto>(id);

            if (country == null)
                return NotFound();

            return Ok(country);
        }

        [HttpPost]
        public IActionResult CreateCountry(CountryDto countryModel)
        {
            if (countryModel == null)
                return BadRequest();

            var countryEntity = _unitOfWork.Mapper.Map<Country>(countryModel);
            _unitOfWork.CountryRepository.Add(countryEntity);
            _unitOfWork.Commit();

            var countryDto = _unitOfWork.Mapper.Map<CountryDto>(countryEntity);

            return CreatedAtAction(nameof(GetCountry), new { id = countryDto.countryID }, countryDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCountry(int id, CountryDto updatedCountryModel)
        {
            var existingCountryEntity = _unitOfWork.CountryRepository.GetById<CountryDto>(id);

            if (existingCountryEntity == null)
                return NotFound();

            _unitOfWork.CountryRepository.UpdateProperties(id, entity =>
            {
                entity.countryName = updatedCountryModel.countryName;

            });

            _unitOfWork.Commit();

            return Ok(new { message = "Cập nhật thành công" });
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCountry(int id)
        {
            var countryEntity = _unitOfWork.CountryRepository.GetById<CountryDto>(id);

            if (countryEntity == null)
                return NotFound();

            _unitOfWork.CountryRepository.Delete(id);
            _unitOfWork.Commit();

            return Ok(new { message = "Xóa thành công" });
        }


        [HttpGet("search")]
        public IActionResult SearchCountrys([FromQuery] string countrykey)
        {
            var countryes = _unitOfWork.CountryRepository.Search<CountryDto>(country =>
                            country.countryName != null &&
                            country.countryName.Contains(countrykey) );
            return Ok(countryes);
        }
    }
}
