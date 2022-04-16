using CMS.Data.Access.Commands.Interfaces;
using CMS.Data.Access.Queries.Interfaces;
using CMS.Data.Models.DTOs;
using CMS.Services;
using CMS.Services.UnitTests.MockData;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CMS.Data.Services.UnitTests
{
    [TestFixture]
    internal class OrderServiceUnitTests
    {
        private Mock<IOrderCommands> CommandsMock;
        private Mock<IOrderQueries> QueriesMock;

        private OrderService _orderService;
        private OrderDto[] _mockedOrdersData;

        [SetUp]
        public void SetUp()
        {
            CommandsMock = new Mock<IOrderCommands>();
            QueriesMock = new Mock<IOrderQueries>();
            PrepareMockData();
            _orderService = new OrderService(CommandsMock.Object, QueriesMock.Object);
        }

        [Test]
        public void CheckGetAll_WorksCorrectly()
        {
            var result = _orderService.GetAllOrders();
            var resultArray = result.ToArray();

            // Expected Quantity
            Assert.AreEqual(3, result.Count());

            Assert.AreEqual(_mockedOrdersData[0].Customer.FirstName, resultArray[0].Customer.FirstName);
            Assert.AreEqual(_mockedOrdersData[1].Customer.FirstName, resultArray[1].Customer.FirstName);
            Assert.AreEqual(_mockedOrdersData[2].Customer.FirstName, resultArray[2].Customer.FirstName);

            Assert.AreEqual(_mockedOrdersData[0].Customer.LastName, resultArray[0].Customer.LastName);
            Assert.AreEqual(_mockedOrdersData[1].Customer.LastName, resultArray[1].Customer.LastName);
            Assert.AreEqual(_mockedOrdersData[2].Customer.LastName, resultArray[2].Customer.LastName);

            Assert.AreEqual(_mockedOrdersData[0].Items.Count(), resultArray[0].Items.Count());
            Assert.AreEqual(_mockedOrdersData[1].Items.Count(), resultArray[1].Items.Count());
            Assert.AreEqual(_mockedOrdersData[2].Items.Count(), resultArray[2].Items.Count());
        }

        [Test]
        public async Task CheckGetAllAsync_WorksCorrectly()
        {
            var result = await _orderService.GetAllOrdersAsync(CancellationToken.None);
            var resultArray = result.ToArray();

            //Expected Quantity
            Assert.AreEqual(3, result.Count());

            Assert.AreEqual(_mockedOrdersData[0].Customer.FirstName, resultArray[0].Customer.FirstName);
            Assert.AreEqual(_mockedOrdersData[1].Customer.FirstName, resultArray[1].Customer.FirstName);
            Assert.AreEqual(_mockedOrdersData[2].Customer.FirstName, resultArray[2].Customer.FirstName);

            Assert.AreEqual(_mockedOrdersData[0].Customer.LastName, resultArray[0].Customer.LastName);
            Assert.AreEqual(_mockedOrdersData[1].Customer.LastName, resultArray[1].Customer.LastName);
            Assert.AreEqual(_mockedOrdersData[2].Customer.LastName, resultArray[2].Customer.LastName);

            Assert.AreEqual(_mockedOrdersData[0].Items.Count(), resultArray[0].Items.Count());
            Assert.AreEqual(_mockedOrdersData[1].Items.Count(), resultArray[1].Items.Count());
            Assert.AreEqual(_mockedOrdersData[2].Items.Count(), resultArray[2].Items.Count());
        }

        [Test]
        public void CheckGetById_WorksCorrectly()
        {
            var result1 = _orderService.GetOrderById(1);

            //Expected Quantity
            Assert.AreEqual(3, result1.Items.Count());

            Assert.AreEqual(_mockedOrdersData[0].Customer.FirstName, result1.Customer.FirstName);
            Assert.AreEqual(_mockedOrdersData[0].Customer.LastName, result1.Customer.LastName);
        }

        [Test]
        public async Task CheckGetByIdAsync_WorksCorrectly()
        {
            var result1 = await _orderService.GetOrderByIdAsync(1, CancellationToken.None);

            //Expected Quantity
            Assert.AreEqual(3, result1.Items.Count());

            Assert.AreEqual(_mockedOrdersData[0].Customer.FirstName, result1.Customer.FirstName);
            Assert.AreEqual(_mockedOrdersData[0].Customer.LastName, result1.Customer.LastName);
        }

        [Test]
        public void CheckCustomerCreation_WorksCorrectly()
        {
            var result3 = _orderService.CreateOrder(_mockedOrdersData[2]);

            //Expected Quantity
            Assert.AreEqual(3, result3);
        }

        [Test]
        public async Task CheckCustomerCreationAsync_WorksCorrectly()
        {
            var result3 = await _orderService.CreateOrderAsync(_mockedOrdersData[2], CancellationToken.None);

            //Expected Quantity
            Assert.AreEqual(3, result3);
        }

        [Test]
        public void CheckCustomerUpdate_WorksCorrectly()
        {
            var result3 = _orderService.UpdateOrder(_mockedOrdersData[2]);

            //Expected Quantity
            Assert.AreEqual(3, result3);
        }

        [Test]
        public async Task CheckCustomerUpdateAsync_WorksCorrectly()
        {
            var result3 = await _orderService.UpdateOrderAsync(_mockedOrdersData[2], CancellationToken.None);

            //Expected Quantity
            Assert.AreEqual(3, result3);
        }

        [Test]
        public void CheckCustomerDelete_WorksCorrectly()
        {
            var result3 = _orderService.DeleteOrder(_mockedOrdersData[2]);

            //Expected Quantity
            Assert.AreEqual(3, result3);
        }

        [Test]
        public async Task CheckCustomerDeleteAsync_WorksCorrectly()
        {
            var result3 = await _orderService.DeleteOrderAsync(_mockedOrdersData[2], CancellationToken.None);

            //Expected Quantity
            Assert.AreEqual(3, result3);
        }

        private void PrepareMockData()
        {
            var order1 = MockData.GetOrderDto(1, 1);
            var order2 = MockData.GetOrderDto(1, 2);
            var order3 = MockData.GetOrderDto(1, 3);

            _mockedOrdersData = new[] { order1, order2, order3 };

            CommandsMock.Setup(s => s.CreateOrder(order3)).Returns(3);
            CommandsMock.Setup(s => s.UpdateOrder(order3)).Returns(3);
            CommandsMock.Setup(s => s.DeleteOrder(order3)).Returns(3);

            CommandsMock.Setup(s => s.CreateOrderAsync(order3, CancellationToken.None)).ReturnsAsync(3);
            CommandsMock.Setup(s => s.UpdateOrderAsync(order3, CancellationToken.None)).ReturnsAsync(3);
            CommandsMock.Setup(s => s.DeleteOrderAsync(order3, CancellationToken.None)).ReturnsAsync(3);

            QueriesMock.Setup(s => s.GetAllOrders()).Returns(new[] { order1, order2, order3 });
            QueriesMock.Setup(s => s.GetAllOrdersAsync(CancellationToken.None)).ReturnsAsync(_mockedOrdersData);

            QueriesMock.Setup(s => s.GetOrderById(1)).Returns(order1);
            QueriesMock.Setup(s => s.GetOrderByIdAsync(1, CancellationToken.None)).ReturnsAsync(order1);

            QueriesMock.Setup(s => s.GetOrderById(2)).Returns(order2);
            QueriesMock.Setup(s => s.GetOrderByIdAsync(2, CancellationToken.None)).ReturnsAsync(order2);

            QueriesMock.Setup(s => s.GetOrderById(3)).Returns(order3);
            QueriesMock.Setup(s => s.GetOrderByIdAsync(3, CancellationToken.None)).ReturnsAsync(order3);

            QueriesMock.Setup(s => s.GetOrdersByCustomer(order1.Customer)).Returns(_mockedOrdersData);
            QueriesMock.Setup(s => s.GetOrdersByCustomerAsync(order1.Customer, CancellationToken.None)).ReturnsAsync(_mockedOrdersData);

            QueriesMock.Setup(s => s.GetOrdersByDateRange(DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1))).Returns(_mockedOrdersData);
            QueriesMock.Setup(s => s.GetOrdersByDateRangeAsync(DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1), CancellationToken.None)).ReturnsAsync(_mockedOrdersData);

            QueriesMock.Setup(s => s.GetOrdersByCustomerDateRange(order1.Customer, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1))).Returns(_mockedOrdersData);
            QueriesMock.Setup(s => s.GetOrdersByCustomerDateRangeAsync(order1.Customer, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1), CancellationToken.None)).ReturnsAsync(_mockedOrdersData);
        }
    }
}