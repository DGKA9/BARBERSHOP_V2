using AutoMapper;
using BARBERSHOP_V2.Controllers;
using BARBERSHOP_V2.Data;
using BARBERSHOP_V2.DTO;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Repository.ExceptionRepo;
using BARBERSHOP_V2.Unit;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BARBERSHOP_V2.Test.Controller
{
    public class ProductControllerTest
    {
        private readonly ProductController _controller;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUniqueConstraintHandler _uniqueConstraintHandler;
        private readonly BarberShopContext _context;

        public ProductControllerTest()
        {
            var options = new DbContextOptionsBuilder<BarberShopContext>()
                .UseInMemoryDatabase("barberShopv2").Options;

            _context = new BarberShopContext(options);

            _mapper = A.Fake<IMapper>();
            _unitOfWork = new UnitOfWork(_context, _mapper);
            _uniqueConstraintHandler = A.Fake<IUniqueConstraintHandler>();
            _controller = new ProductController(_unitOfWork, _uniqueConstraintHandler);
        }

        [Fact]
        public void CreateProduct_ValidProduct_ReturnsCreatedAtAction()
        {
            // Arrange
            var validProductDto = new ProductDto
            {
                proName = "Sieu keo vuot toc Nam",
                proImage = "image.jpg",
                price = 1356,
                quantity = 10,
                proDescription = "Sieu keo vuot toc Nam",
                producerID = 1,
                warehouseID = 1,
                cateID = 1
            };
            var productEntity = A.Fake<Product>();
            A.CallTo(() => _mapper.Map<Product>(validProductDto)).Returns(productEntity);
            A.CallTo(() => _mapper.Map<ProductDto>(productEntity)).Returns(validProductDto);

            // Act
            var result = _controller.CreateProduct(validProductDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetProduct", createdAtActionResult.ActionName);
            Assert.Equal(validProductDto, createdAtActionResult.Value);
        }

        [Fact]
        public void CreateProduct_InvalidCategory_ReturnsBadRequest()
        {
            // Arrange
            var invalidCategoryProductDto = new ProductDto
            {
                proName = "Sieu keo vuot toc Nam 2",
                proImage = "image2.jpg",
                price = 1356,
                quantity = 10,
                proDescription = "Sieu keo vuot toc Nam 2",
                producerID = 1,
                warehouseID = 1,
                cateID = -1
            };
            A.CallTo(() => _unitOfWork.Mapper.Map<Product>(invalidCategoryProductDto))
                .Throws(new Exception("Invalid category ID"));

            // Act & Assert
            var result = Assert.Throws<Exception>(() => _controller.CreateProduct(invalidCategoryProductDto));
            Assert.Equal("Invalid category ID", result.Message);
        }

        [Fact]
        public void CreateProduct_InvalidQuantity_ReturnsBadRequest()
        {
            // Arrange
            var invalidQuantityProductDto = new ProductDto
            {
                proName = "Sieu keo vuot toc Nam 3",
                proImage = "image3.jpg",
                price = 1356,
                quantity = -10,
                proDescription = "Sieu keo vuot toc Nam 3",
                producerID = 1,
                warehouseID = 1,
                cateID = 1
            };
            A.CallTo(() => _unitOfWork.Mapper.Map<Product>(invalidQuantityProductDto))
                .Throws(new Exception("Invalid quantity"));

            // Act & Assert
            var result = Assert.Throws<Exception>(() => _controller.CreateProduct(invalidQuantityProductDto));
            Assert.Equal("Invalid quantity", result.Message);
        }
    }
}
