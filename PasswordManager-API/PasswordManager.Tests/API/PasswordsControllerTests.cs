using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PasswordManager.API.Controllers;
using PasswordManager.Application.Interfaces;
using PasswordManager.API.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using PasswordManager.Domain.Entities;

namespace PasswordManager.Tests.API.Controllers
{
    [TestClass]
    public class PasswordControllerTests
    {
        private Mock<IPasswordService> _mockService;
        private PasswordsController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IPasswordService>();
            _controller = new PasswordsController(_mockService.Object);
        }

        [TestMethod]
        public async Task GetAllPasswordRecordAsync_ReturnsOk_WithResultsAsync()
        {
            // Arrange
            var mockData = new List<Password>
            {
                new Password { Id = 1, Category = "Email", App = "Gmail", UserName = "testUser", EncryptedPassword = "encryptedPassword" }
            };

            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(mockData);

            // Act
            var result = await _controller.GetAllPasswordRecordAsync();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsInstanceOfType(okResult.Value, typeof(List<Password>));
        }

        [TestMethod]
        public async Task GetPasswordRecordByIdAsync_ReturnsNotFound_WhenNullAsync()
        {
            // Arrange
            _mockService.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Password)null);

            // Act
            var result = await _controller.GetPasswordRecordByIdAsync(99);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task CreatePasswordRecordAsync_ReturnsOk_WhenCreatedAsync()
        {
            // Arrange
            var dto = new PasswordDto { Category = "Social", App = "Twitter", UserName = "user1", Password = "pass" };
            var createdPassword = new Password
            {
                Id = 1,
                Category = "Social",
                App = "Twitter",
                UserName = "user1",
                EncryptedPassword = "encryptedPassword"
            };

            _mockService.Setup(s => s.AddAsync(dto)).ReturnsAsync(createdPassword);

            // Act
            var result = await _controller.CreatePasswordRecordAsync(dto);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsInstanceOfType(okResult.Value, typeof(Password));
        }

        [TestMethod]
        public async Task DeletePasswordRecordByIdAsync_ReturnsNoContent_WhenDeletedAsync()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeletePasswordRecordByIdAsync(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
    }
}
