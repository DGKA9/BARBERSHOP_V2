using BARBERSHOP_V2.DTO;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Repository.ExceptionRepo;
using BARBERSHOP_V2.Unit;
using Microsoft.AspNetCore.Mvc;

namespace BARBERSHOP_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductOrderController : BaseController
    {
        public ProductOrderController(UnitOfWork unitOfWork, IUniqueConstraintHandler uniqueConstraintHandler)
            : base(unitOfWork, uniqueConstraintHandler)
        {

        }

        [HttpGet]
        public IActionResult GetProductOrder()
        {
            var productOrder = _unitOfWork.ProductOrderRepository.GetAll<ProductOrderDto>();
            return Ok(productOrder);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductOrder(int id)
        {
            var productOrder = _unitOfWork.ProductOrderRepository.GetById<ProductOrderDto>(id);

            if (productOrder == null)
                return NotFound();

            return Ok(productOrder);
        }

        [HttpPost]
        public IActionResult CreateProductOrder(ProductOrderDto productOrderModel)
        {
            if (productOrderModel == null)
                return BadRequest();

            var productOrderEntity = _unitOfWork.Mapper.Map<ProductOrder>(productOrderModel);
            _unitOfWork.ProductOrderRepository.Add(productOrderEntity);
            _unitOfWork.Commit();

            var productOrderDto = _unitOfWork.Mapper.Map<ProductOrderDto>(productOrderEntity);

            return CreatedAtAction(nameof(GetProductOrder), new { id = productOrderDto.proOrderID }, productOrderDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProductOrder(int id, ProductOrderDto updatedProductOrderModel)
        {
            var existingProductOrderEntity = _unitOfWork.ProductOrderRepository.GetById<ProductOrderDto>(id);

            if (existingProductOrderEntity == null)
                return NotFound();

            _unitOfWork.ProductOrderRepository.UpdateProperties(id, entity =>
            {
                entity.proOrderQuantity = updatedProductOrderModel.proOrderQuantity;
                entity.orderID = updatedProductOrderModel.orderID;
                entity.Product.proID = updatedProductOrderModel.proID;
            });

            _unitOfWork.Commit();

            return Ok(new { message = "Cập nhật thành công" });
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteProductOrder(int id)
        {
            var productOrderEntity = _unitOfWork.ProductOrderRepository.GetById<ProductOrderDto>(id);

            if (productOrderEntity == null)
                return NotFound();

            _unitOfWork.ProductOrderRepository.Delete(id);
            _unitOfWork.Commit();

            return Ok(new { message = "Xóa thành công" });
        }
    }
}
