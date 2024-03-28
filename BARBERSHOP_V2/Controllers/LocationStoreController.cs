using BARBERSHOP_V2.DTO;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Repository.ExceptionRepo;
using BARBERSHOP_V2.Unit;
using Microsoft.AspNetCore.Mvc;

namespace BARBERSHOP_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationStoreController : BaseController
    {
        public LocationStoreController(UnitOfWork unitOfWork, IUniqueConstraintHandler uniqueConstraintHandler)
            : base(unitOfWork, uniqueConstraintHandler)
        {

        }

        [HttpGet]
        public IActionResult GetLocationStore()
        {
            var locationStore = _unitOfWork.LocationStoreRepository.GetAll<LocationStoreDto>();
            return Ok(locationStore);
        }

        [HttpGet("{id}")]
        public IActionResult GetLocationStore(int id)
        {
            var locationStore = _unitOfWork.LocationStoreRepository.GetById<LocationStoreDto>(id);

            if (locationStore == null)
                return NotFound();

            return Ok(locationStore);
        }

        [HttpPost]
        public IActionResult CreateLocationStore(LocationStoreDto locationStoreModel)
        {
            if (locationStoreModel == null)
                return BadRequest();

            var locationStoreEntity = _unitOfWork.Mapper.Map<LocationStore>(locationStoreModel);
            _unitOfWork.LocationStoreRepository.Add(locationStoreEntity);
            _unitOfWork.Commit();

            var locationStoreDto = _unitOfWork.Mapper.Map<LocationStoreDto>(locationStoreEntity);

            return CreatedAtAction(nameof(GetLocationStore), new { id = locationStoreDto.locationID }, locationStoreDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLocationStore(int id, LocationStoreDto updatedLocationStoreModel)
        {
            var existingLocationStoreEntity = _unitOfWork.LocationStoreRepository.GetById<LocationStoreDto>(id);

            if (existingLocationStoreEntity == null)
                return NotFound();

            _unitOfWork.LocationStoreRepository.UpdateProperties(id, entity =>
            {
                entity.currentAddress = updatedLocationStoreModel.currentAddress;
                entity.subDistrict = updatedLocationStoreModel.subDistrict;
                entity.district = updatedLocationStoreModel.district;
                entity.cityID = updatedLocationStoreModel.cityID;
                entity.storeID = updatedLocationStoreModel.storeID;
            });

            _unitOfWork.Commit();

            return Ok(new { message = "Cập nhật thành công" });
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteLocationStore(int id)
        {
            var locationStoreEntity = _unitOfWork.LocationStoreRepository.GetById<LocationStoreDto>(id);

            if (locationStoreEntity == null)
                return NotFound();

            _unitOfWork.LocationStoreRepository.Delete(id);
            _unitOfWork.Commit();

            return Ok(new { message = "Xóa thành công" });
        }


        [HttpGet("search")]
        public IActionResult SearchLocationStores([FromQuery] string locationStorekey)
        {
            var locationStorees = _unitOfWork.LocationStoreRepository.Search<LocationStoreDto>(locationStore =>
                            locationStore.currentAddress != null &&
                            locationStore.currentAddress.Contains(locationStorekey) ||
                            locationStore.subDistrict != null && locationStore.subDistrict.Contains(locationStorekey) ||
                            locationStore.district!= null && locationStore.district.Contains(locationStorekey));
            return Ok(locationStorees);
        }
    }
}
