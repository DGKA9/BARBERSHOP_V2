using BARBERSHOP_V2.DTO;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Repository.ExceptionRepo;
using BARBERSHOP_V2.Unit;
using Microsoft.AspNetCore.Mvc;

namespace BARBERSHOP_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : BaseController
    {
        public WarehouseController(UnitOfWork unitOfWork, IUniqueConstraintHandler uniqueConstraintHandler)
            : base(unitOfWork, uniqueConstraintHandler)
        {

        }

        [HttpGet]
        public IActionResult GetWarehouses()
        {
            var warehouse = _unitOfWork.WarehouseRepository.GetAll<WarehouseDto>();
            return Ok(warehouse);
        }

        [HttpGet("{id}")]
        public IActionResult GetWarehouse(int id)
        {
            var warehouse = _unitOfWork.WarehouseRepository.GetById<WarehouseDto>(id);

            if (warehouse == null)
                return NotFound();

            return Ok(warehouse);
        }

        [HttpPost]
        public IActionResult CreateWarehouse(WarehouseDto warehouseModel)
        {
            if (warehouseModel == null)
                return BadRequest();

            var warehouseEntity = _unitOfWork.Mapper.Map<Warehouse>(warehouseModel);
            _unitOfWork.WarehouseRepository.Add(warehouseEntity);
            _unitOfWork.Commit();

            var warehouseDto = _unitOfWork.Mapper.Map<WarehouseDto>(warehouseEntity);

            return CreatedAtAction(nameof(GetWarehouse), new { id = warehouseDto.warehouseID }, warehouseDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateWarehouse(int id, WarehouseDto updatedWarehouseModel)
        {
            var existingWarehouseEntity = _unitOfWork.WarehouseRepository.GetById<WarehouseDto>(id);

            if (existingWarehouseEntity == null)
                return NotFound();

            _unitOfWork.WarehouseRepository.UpdateProperties(id, entity =>
            {
                entity.warehouseName = updatedWarehouseModel.warehouseName;
                entity.totalAsset = updatedWarehouseModel.totalAsset;
                entity.capacity = updatedWarehouseModel.capacity;
                entity.addressID = updatedWarehouseModel.addressID;
                entity.storeID = updatedWarehouseModel.storeID;
            });

            _unitOfWork.Commit();

            return Ok(new { message = "Cập nhật thành công" });
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteWarehouse(int id)
        {
            var warehouseEntity = _unitOfWork.WarehouseRepository.GetById<WarehouseDto>(id);

            if (warehouseEntity == null)
                return NotFound();

            _unitOfWork.WarehouseRepository.Delete(id);
            _unitOfWork.Commit();

            return Ok(new { message = "Xóa thành công" });
        }


        [HttpGet("search")]
        public IActionResult SearchWarehouses([FromQuery] string warehouseName)
        {
            var warehouse = _unitOfWork.WarehouseRepository.Search<WarehouseDto>(warehouse => warehouse.warehouseName!.Contains(warehouseName));

            if (warehouse != null)
            {
                return Ok(warehouse);
            }
            else
            {
                return Ok(new { message = "Không tìm thấy chuỗi cần tìm" });
            }
        }
    }
}
