using BARBERSHOP_V2.DTO;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Repository.ExceptionRepo;
using BARBERSHOP_V2.Unit;
using Microsoft.AspNetCore.Mvc;

namespace BARBERSHOP_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : BaseController
    {
        public StoreController(UnitOfWork unitOfWork, IUniqueConstraintHandler uniqueConstraintHandler)
            : base(unitOfWork, uniqueConstraintHandler)
        {

        }

        [HttpGet]
        public IActionResult GetStores()
        {
            var store = _unitOfWork.StoreRepository.GetAll<StoreDto>();
            return Ok(store);
        }

        [HttpGet("{id}")]
        public IActionResult GetStore(int id)
        {
            var store = _unitOfWork.StoreRepository.GetById<StoreDto>(id);

            if (store == null)
                return NotFound();

            return Ok(store);
        }

        [HttpPost]
        public IActionResult CreateStore(StoreDto storeModel)
        {
            if (storeModel == null)
                return BadRequest();

            var storeEntity = _unitOfWork.Mapper.Map<Store>(storeModel);
            _unitOfWork.StoreRepository.Add(storeEntity);
            _unitOfWork.Commit();

            var storeDto = _unitOfWork.Mapper.Map<StoreDto>(storeEntity);

            return CreatedAtAction(nameof(GetStore), new { id = storeDto.storeID }, storeDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStore(int id, StoreDto updatedStoreModel)
        {
            var existingStoreEntity = _unitOfWork.StoreRepository.GetById<StoreDto>(id);

            if (existingStoreEntity == null)
                return NotFound();

            _unitOfWork.StoreRepository.UpdateProperties(id, entity =>
            {
                entity.storeName = updatedStoreModel.storeName;
                entity.numberphone = updatedStoreModel.numberphone;
                entity.workingHourID= updatedStoreModel.workingHourID;
            });

            _unitOfWork.Commit();

            return Ok(new { message = "Cập nhật thành công" });
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteStore(int id)
        {
            var storeEntity = _unitOfWork.StoreRepository.GetById<StoreDto>(id);

            if (storeEntity == null)
                return NotFound();

            _unitOfWork.StoreRepository.Delete(id);
            _unitOfWork.Commit();

            return Ok(new { message = "Xóa thành công" });
        }


        [HttpGet("search")]
        public IActionResult SearchStores([FromQuery] string storeName)
        {
            var store = _unitOfWork.StoreRepository.Search<StoreDto>(store => store.storeName!.Contains(storeName));

            if (store != null)
            {
                return Ok(store);
            }
            else
            {
                return Ok(new { message = "Không tìm thấy chuỗi cần tìm" });
            }
        }
    }
}
