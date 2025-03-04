using Entities.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class OrderServiceUnitTests
    {
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<ILogger<OrderService>> _mockLogger;
        private readonly OrderService _orderService;

        public OrderServiceUnitTests()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockProductRepository = new Mock<IProductRepository>();
            _mockLogger = new Mock<ILogger<OrderService>>();
            _orderService = new OrderService(_mockOrderRepository.Object, _mockProductRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task AddOrderAsync_ShouldCorrectOrderSum_WhenOrderSumIsInvalid()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 1,
                UserId = 1,
                OrderSum = 100, // Invalid sum
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { ProductId = 1 },
                    new OrderItem { ProductId = 2 }
                }
            };

            var product1 = new Product { ProductId = 1, Price = 50 };
            var product2 = new Product { ProductId = 2, Price = 60 };

            _mockProductRepository.Setup(repo => repo.GetProductById(1)).ReturnsAsync(product1);
            _mockProductRepository.Setup(repo => repo.GetProductById(2)).ReturnsAsync(product2);

            _mockOrderRepository.Setup(repo => repo.AddOrderAsync(It.IsAny<Order>())).ReturnsAsync(order);

            // Act
            var result = await _orderService.AddOrderAsync(order);

            // Assert
            Assert.Equal(110, result.OrderSum); // Corrected sum

            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"user {order.UserId} tried to change order {order.OrderId} sum")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once
            );
        }

        [Fact]
        public async Task AddOrderAsync_ShouldNotLogWarning_WhenOrderSumIsValid()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 1,
                UserId = 1,
                OrderSum = 110, // Valid sum
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { ProductId = 1 },
                    new OrderItem { ProductId = 2 }
                }
            };

            var product1 = new Product { ProductId = 1, Price = 50 };
            var product2 = new Product { ProductId = 2, Price = 60 };

            _mockProductRepository.Setup(repo => repo.GetProductById(1)).ReturnsAsync(product1);
            _mockProductRepository.Setup(repo => repo.GetProductById(2)).ReturnsAsync(product2);

            _mockOrderRepository.Setup(repo => repo.AddOrderAsync(It.IsAny<Order>())).ReturnsAsync(order);

            // Act
            var result = await _orderService.AddOrderAsync(order);

            // Assert
            Assert.Equal(110, result.OrderSum); // Sum remains valid
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Never
            );
        }
    }
}

