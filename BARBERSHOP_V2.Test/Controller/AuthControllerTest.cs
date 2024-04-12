using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BARBERSHOP_V2.Controllers;
using BARBERSHOP_V2.Data;
using BARBERSHOP_V2.DTO;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Repository.ExceptionRepo;
using BARBERSHOP_V2.Service.Validator;
using BARBERSHOP_V2.Test.Helpers;
using BARBERSHOP_V2.Unit;
using FakeItEasy;
using FakeItEasy.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BARBERSHOP_V2.Test.Controller
{
    public class AuthControllerTest
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthControllerTest()
        {
            _configuration = A.Fake<IConfiguration>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async Task Register_ExistingUserName_ReturnsBadRequest()
        {
            var options = new DbContextOptionsBuilder<BarberShopContext>()
                .UseInMemoryDatabase(databaseName: "barberShopv2")
                .Options;

            using (var context = new BarberShopContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var mockUniqueConstraintHandler = A.Fake<IUniqueConstraintHandler>();
                var mockPasswordValidator = A.Fake<PasswordValidator>();
                A.CallTo(() => mockPasswordValidator.IsStrongPassword(A<string>._)).Returns(true);

                var existingUser = new User { userName = "dangkhoaooo" };
                context.Users.Add(existingUser);
                context.SaveChanges();

                var unitOfWork = new UnitOfWork(context, _mapper);

                var controller = new AuthController(context, _configuration, unitOfWork, mockUniqueConstraintHandler, mockPasswordValidator);

                var result = await controller.Register(new UserDto { userName = "dangkhoaooo", password = "Abc123", roleID = 1 });

                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
                Assert.Equal("User already exists.", badRequestResult.Value);
            }
        }

        [Fact]
        public async Task Register_NullUserName_ReturnsBadRequest()
        {
            var options = new DbContextOptionsBuilder<BarberShopContext>()
                .UseInMemoryDatabase(databaseName: "barberShopv2_NullUserName")
                .Options;

            using (var context = new BarberShopContext(options))
            {
                var mockUniqueConstraintHandler = A.Fake<IUniqueConstraintHandler>();
                var mockPasswordValidator = A.Fake<PasswordValidator>();
                var unitOfWork = new UnitOfWork(context, _mapper);
                var controller = new AuthController(context, _configuration, unitOfWork, mockUniqueConstraintHandler, mockPasswordValidator);

                var result = await controller.Register(new UserDto { userName = null, password = "Abc123", roleID = 1 });

                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
                Assert.Equal("Username is required.", badRequestResult.Value);
            }
        }

        [Fact]
        public async Task Register_NullPassword_ReturnsBadRequest()
        {
            var options = new DbContextOptionsBuilder<BarberShopContext>()
                .UseInMemoryDatabase(databaseName: "barberShopv2_NullPassword")
                .Options;

            using (var context = new BarberShopContext(options))
            {
                var mockUniqueConstraintHandler = A.Fake<IUniqueConstraintHandler>();
                var mockPasswordValidator = A.Fake<PasswordValidator>();
                var unitOfWork = new UnitOfWork(context, _mapper);
                var controller = new AuthController(context, _configuration, unitOfWork, mockUniqueConstraintHandler, mockPasswordValidator);

                var result = await controller.Register(new UserDto { userName = "newuser", password = null, roleID = 1 });

                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
                Assert.Equal("Password is required.", badRequestResult.Value);
            }
        }
    }
}
