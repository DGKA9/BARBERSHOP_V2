using BARBERSHOP_V2.DTO;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Repository.ExceptionRepo;
using BARBERSHOP_V2.Unit;
using Microsoft.AspNetCore.Mvc;

namespace BARBERSHOP_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(UnitOfWork unitOfWork, IUniqueConstraintHandler uniqueConstraintHandler)
            : base(unitOfWork, uniqueConstraintHandler)
        {

        }

        [HttpGet]
        public IActionResult GetUser()
        {
            var user = _unitOfWork.UserRepository.GetAll<UserDto>();
            return Ok(user);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _unitOfWork.UserRepository.GetById<UserDto>(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser(UserDto userModel)
        {
            if (userModel == null)
                return BadRequest();

            var userEntity = _unitOfWork.Mapper.Map<User>(userModel);
            _unitOfWork.UserRepository.Add(userEntity);
            _unitOfWork.Commit();

            var userDto = _unitOfWork.Mapper.Map<UserDto>(userEntity);

            return CreatedAtAction(nameof(GetUser), new { id = userDto.userID }, userDto);
        }

        //[HttpPut("{id}")]
        //public IActionResult UpdateUser(int id, UserDto updatedUserModel)
        //{
        //    var existingUserEntity = _unitOfWork.UserRepository.GetById<UserDto>(id);

        //    if (existingUserEntity == null)
        //        return NotFound();

        //    _unitOfWork.UserRepository.UpdateProperties(id, entity =>
        //    {
        //        entity.userName = updatedUserModel.userName;
        //        entity.PasswordSalt = updatedUserModel.password;
        //        entity.roleID = updatedUserModel.roleID;
        //    });

        //    _unitOfWork.Commit();

        //    return Ok(new { message = "Cập nhật thành công" });
        //}


        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var userEntity = _unitOfWork.UserRepository.GetById<UserDto>(id);

            if (userEntity == null)
                return NotFound();

            _unitOfWork.UserRepository.Delete(id);
            _unitOfWork.Commit();

            return Ok(new { message = "Xóa thành công" });
        }
    }
}
