using CMS.API.Controllers;
using CMS.Data.Models.DTOs;
using CMS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CMS.API.UnitTests.Controllers
{
    /// <summary>
    /// Order Controller Unit Tests
    /// </summary>
    [TestFixture]
    public class OrderControllerUnitTests
    {
        private Mock<ICustomerService> _customerServiceMock;
        private Mock<IOrderService> _orderServiceMock;

        private OrderController _orderController;

        private OrderDto[] _mockedOrdersData;
        private Mock<ILogger<OrderController>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<OrderController>>();
            _customerServiceMock = new Mock<ICustomerService>();
            _orderServiceMock = new Mock<IOrderService>();
          
            PrepareMockData();

            _orderController = new OrderController(_loggerMock.Object, _customerServiceMock.Object, _orderServiceMock.Object);
        }

        /// <summary>
        /// Checks whether All Orders are Retreived correctly Asyncronously
        /// </summary>
        [Test]
        public async Task CheckGetAllAsync_WorksCorrectly()
        {
            var result = await _orderController.GetAllAsync(CancellationToken.None) as OkObjectResult;

            //// Expected Quantity
            Assert.AreEqual(200, result?.StatusCode);
            var actualOrders = result?.Value as OrderDto[];

            ////Expected Quantity
            Assert.AreEqual(3, actualOrders.Length);

            Assert.AreEqual(_mockedOrdersData[0].Customer.FirstName, actualOrders[0].Customer.FirstName);
            Assert.AreEqual(_mockedOrdersData[1].Customer.FirstName, actualOrders[1].Customer.FirstName);
            Assert.AreEqual(_mockedOrdersData[2].Customer.FirstName, actualOrders[2].Customer.FirstName);

            Assert.AreEqual(_mockedOrdersData[0].Customer.LastName, actualOrders[0].Customer.LastName);
            Assert.AreEqual(_mockedOrdersData[1].Customer.LastName, actualOrders[1].Customer.LastName);
            Assert.AreEqual(_mockedOrdersData[2].Customer.LastName, actualOrders[2].Customer.LastName);

            Assert.AreEqual(_mockedOrdersData[0].Items.Count(), actualOrders[0].Items.Count());
            Assert.AreEqual(_mockedOrdersData[1].Items.Count(), actualOrders[1].Items.Count());
            Assert.AreEqual(_mockedOrdersData[2].Items.Count(), actualOrders[2].Items.Count());
        }

        /// <summary>
        /// Checks whether Order is Retreived correctly By Id Asyncronously
        /// </summary>
        [Test]
        public async Task CheckGetByIdAsync_WorksCorrectly()
        {
            var result = await _orderController.GetByIdAsync(1, CancellationToken.None) as OkObjectResult;

            Assert.AreEqual(200, result?.StatusCode);

            var actualOrder = result?.Value as OrderDto;

            ////Expected Quantity
            Assert.AreEqual(3, actualOrder.Items.Count());

            Assert.AreEqual(_mockedOrdersData[0].Customer.FirstName, actualOrder.Customer.FirstName);
            Assert.AreEqual(_mockedOrdersData[0].Customer.LastName, actualOrder.Customer.LastName);

            Assert.AreEqual(_mockedOrdersData[0].Customer.PostalCode, actualOrder.Customer.PostalCode);
            Assert.AreEqual(_mockedOrdersData[0].Customer.Address, actualOrder.Customer.Address);
        }

        /// <summary>
        /// Check whether Order is Created correctly Asyncronously
        /// </summary>
        [Test]
        public async Task CheckCustomerCreationAsync_WorksCorrectly()
        {
            var result = await _orderController.CreateAsync(_mockedOrdersData[2], CancellationToken.None) as OkObjectResult;

            Assert.AreEqual(200, result?.StatusCode);
            var actualResult = result?.Value as string;

            ////Expected Quantity
            Assert.AreEqual("New Order Id=3 has been created.", actualResult);
        }

        /// <summary>
        /// Check whether Order is Updated correctly Asyncronously
        /// </summary>
        [Test]
        public async Task CheckOrderUpdateAsync_WorksCorrectly()
        {
            var result = await _orderController.UpdateAsync(_mockedOrdersData[2], CancellationToken.None) as OkObjectResult;

            Assert.AreEqual(200, result?.StatusCode);
            var actualResult = result?.Value as string;

            ////Expected Quantity
            Assert.AreEqual("New Order Id=3 has been updated.", actualResult);
        }

        /// <summary>
        /// Check whether Order is Deleted correctly Asyncronously
        /// </summary>
        [Test]
        public async Task CheckOrderDeleteAsync_WorksCorrectly()
        {
            var result = await _orderController.DeleteAsync(_mockedOrdersData[2], CancellationToken.None) as OkObjectResult;

            Assert.AreEqual(200, result?.StatusCode);
            var actualResult = result?.Value as string;

            ////Expected Quantity
            Assert.AreEqual("New Order Id=3 has been deleted.", actualResult);
        }

        private void PrepareMockData()
        {
            var order1 = MockData.MockData.GetOrderDto(1, 1);
            var order2 = MockData.MockData.GetOrderDto(1, 2);
            var order3 = MockData.MockData.GetOrderDto(1, 3);

            _mockedOrdersData = new[] { order1, order2, order3 };

            _orderServiceMock.Setup(s => s.CreateOrder(order3)).Returns(3);
            _orderServiceMock.Setup(s => s.UpdateOrder(order3)).Returns(3);
            _orderServiceMock.Setup(s => s.DeleteOrder(order3)).Returns(3);

            _orderServiceMock.Setup(s => s.CreateOrderAsync(order3, CancellationToken.None)).ReturnsAsync(3);
            _orderServiceMock.Setup(s => s.UpdateOrderAsync(order3, CancellationToken.None)).ReturnsAsync(3);
            _orderServiceMock.Setup(s => s.DeleteOrderAsync(order3, CancellationToken.None)).ReturnsAsync(3);

            _orderServiceMock.Setup(s => s.GetAllOrders()).Returns(new[] { order1, order2, order3 });
            _orderServiceMock.Setup(s => s.GetAllOrdersAsync(CancellationToken.None)).ReturnsAsync(_mockedOrdersData);

            _orderServiceMock.Setup(s => s.GetOrderById(1)).Returns(order1);
            _orderServiceMock.Setup(s => s.GetOrderByIdAsync(1, CancellationToken.None)).ReturnsAsync(order1);

            _orderServiceMock.Setup(s => s.GetOrderById(2)).Returns(order2);
            _orderServiceMock.Setup(s => s.GetOrderByIdAsync(2, CancellationToken.None)).ReturnsAsync(order2);

            _orderServiceMock.Setup(s => s.GetOrderById(3)).Returns(order3);
            _orderServiceMock.Setup(s => s.GetOrderByIdAsync(3, CancellationToken.None)).ReturnsAsync(order3);

            _orderServiceMock.Setup(s => s.GetOrdersByCustomer(order1.Customer)).Returns(_mockedOrdersData);
            _orderServiceMock.Setup(s => s.GetOrdersByCustomerAsync(order1.Customer, CancellationToken.None)).ReturnsAsync(_mockedOrdersData);

            _orderServiceMock.Setup(s => s.GetOrdersByDateRange(DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1))).Returns(_mockedOrdersData);
            _orderServiceMock.Setup(s => s.GetOrdersByDateRangeAsync(DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1), CancellationToken.None)).ReturnsAsync(_mockedOrdersData);

            _orderServiceMock.Setup(s => s.GetOrdersByCustomerDateRange(order1.Customer, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1))).Returns(_mockedOrdersData);
            _orderServiceMock.Setup(s => s.GetOrdersByCustomerDateRangeAsync(order1.Customer, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1), CancellationToken.None)).ReturnsAsync(_mockedOrdersData);
        }
    }
}