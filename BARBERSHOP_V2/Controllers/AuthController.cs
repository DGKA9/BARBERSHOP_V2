using BARBERSHOP_V2.Data;
using BARBERSHOP_V2.DTO;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Repository.AuthRepo;
using BARBERSHOP_V2.Repository.ExceptionRepo;
using BARBERSHOP_V2.Service.Authentication;
using BARBERSHOP_V2.Service.Validator;
using BARBERSHOP_V2.Unit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BARBERSHOP_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly BarberShopContext _context;
        private readonly IConfiguration _configuration;
        private readonly PasswordValidator _passwordValidator;
        public AuthController(BarberShopContext context, IConfiguration configuration, UnitOfWork unitOfWork, IUniqueConstraintHandler uniqueConstraintHandler, PasswordValidator passwordValidator)
            : base(unitOfWork, uniqueConstraintHandler)
        {
            _context = context;
            _configuration = configuration;
            _passwordValidator = passwordValidator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            if (!_passwordValidator.IsStrongPassword(request.password))
            {
                return BadRequest("Mật khẩu phải ít nhất 8 ký tự bao gồm chữ hoa, chữ thường, chữ số và ký tự đặc biệt.");
            }

            if (_context.Users != null)
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.userName == request.userName);

                if (existingUser != null)
                {
                    return BadRequest("User already exists.");
                }
            }

            JWT.CreatePasswordHash(request.password!, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = new User
            {
                userName = request.userName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                roleID = request.roleID
            };



            _context.Users?.Add(newUser);
            var userDto = _unitOfWork.Mapper.Map<UserDto>(newUser);
            await _context.SaveChangesAsync();

            return Ok(newUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            if (string.IsNullOrEmpty(request.userName))
            {
                return BadRequest("Username is required.");
            }

            if (string.IsNullOrEmpty(request.password))
            {
                return BadRequest("Password is required.");
            }

            var user = await _context.Users!.FirstOrDefaultAsync(u => u.userName == request.userName);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (!JWT.VerifyPasswordHash(request.password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = JWT.CreateToken(user, _configuration);
            var authResponse = new AuthResponse
            {
                token = token,
                userName = user.userName,
                userID = user.userID
            };

            return Ok(authResponse);
        }


    }
}


