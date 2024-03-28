using BARBERSHOP_V2.DTO;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Repository.ExceptionRepo;
using BARBERSHOP_V2.Unit;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BARBERSHOP_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : BaseController
    {
        public CityController(UnitOfWork unitOfWork, IUniqueConstraintHandler uniqueConstraintHandler)
            : base(unitOfWork, uniqueConstraintHandler)
        {

        }

        [HttpGet]
        public IActionResult GetCity()
        {
            var city = _unitOfWork.CityRepository.GetAll<CityDto>();
            return Ok(city);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var city = _unitOfWork.CityRepository.GetById<CityDto>(id);

            if (city == null)
                return NotFound();

            return Ok(city);
        }

        [HttpPost]
        public IActionResult CreateCity(CityDto cityModel)
        {
            try
            {
                if (cityModel == null)
                    return BadRequest();

                var cityEntity = _unitOfWork.Mapper.Map<City>(cityModel);
                _unitOfWork.CityRepository.Add(cityEntity);
                _unitOfWork.Commit();

                var cityDto = _unitOfWork.Mapper.Map<CityDto>(cityEntity);

                return CreatedAtAction(nameof(GetCity), new { id = cityDto.cityID }, cityDto);
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
        public IActionResult UpdateCity(int id, CityDto updatedCityModel)
        {
            var existingCityEntity = _unitOfWork.CityRepository.GetById<CityDto>(id);

            if (existingCityEntity == null)
                return NotFound();

            _unitOfWork.CityRepository.UpdateProperties(id, entity =>
            {
                entity.cityName = updatedCityModel.cityName;
                entity.countryID = updatedCityModel.countryID;

            });

            _unitOfWork.Commit();

            return Ok(new { message = "Cập nhật thành công" });
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCity(int id)
        {
            var cityEntity = _unitOfWork.CityRepository.GetById<CityDto>(id);

            if (cityEntity == null)
                return NotFound();

            _unitOfWork.CityRepository.Delete(id);
            _unitOfWork.Commit();

            return Ok(new { message = "Xóa thành công" });
        }


        [HttpGet("search")]
        public IActionResult SearchCitys([FromQuery] string citykey)
        {
            var cityes = _unitOfWork.CityRepository.Search<CityDto>(city =>
                            city.cityName != null &&
                            city.cityName.Contains(citykey));
            return Ok(cityes);
        }
    }
}
