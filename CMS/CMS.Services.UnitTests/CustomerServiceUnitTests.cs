using CMS.Data.Access.Commands.Interfaces;
using CMS.Data.Access.Queries.Interfaces;
using CMS.Data.Models.DTOs;
using CMS.Services.UnitTests.MockData;
using CMS.Services;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CMS.Data.Services.UnitTests
{
    [TestFixture]
    internal class CustomerServiceUnitTests
    {
        private Mock<ICustomerCommands> CommandsMock;
        private Mock<ICustomerQueries> QueriesMock;

        private CustomerService _customerService;
        private CustomerDto[] _mockedCustomersData;

        [SetUp]
        public void SetUp()
        {
            CommandsMock = new Mock<ICustomerCommands>();
            QueriesMock = new Mock<ICustomerQueries>();
            PrepareMockData();
            _customerService = new CustomerService(CommandsMock.Object, QueriesMock.Object);
        }

        [Test]
        public void CheckGetAll_WorksCorrectly()
        {
            var result = _customerService.GetAllCustomers();
            var resultArray = result.ToArray();

            // Expected Quantity
            Assert.AreEqual(3, result.Count());

            Assert.AreEqual(_mockedCustomersData[0].FirstName, resultArray[0].FirstName);
            Assert.AreEqual(_mockedCustomersData[1].FirstName, resultArray[1].FirstName);
            Assert.AreEqual(_mockedCustomersData[2].FirstName, resultArray[2].FirstName);

            Assert.AreEqual(_mockedCustomersData[0].LastName, resultArray[0].LastName);
            Assert.AreEqual(_mockedCustomersData[1].LastName, resultArray[1].LastName);
            Assert.AreEqual(_mockedCustomersData[2].LastName, resultArray[2].LastName);

            Assert.AreEqual(_mockedCustomersData[0].Orders.Count(), resultArray[0].Orders.Count());
            Assert.AreEqual(_mockedCustomersData[1].Orders.Count(), resultArray[1].Orders.Count());
            Assert.AreEqual(_mockedCustomersData[2].Orders.Count(), resultArray[2].Orders.Count());
        }

        [Test]
        public async Task CheckGetAllAsync_WorksCorrectly()
        {
            var result = await _customerService.GetAllCustomersAsync(CancellationToken.None);
            var resultArray = result.ToArray();

            //Expected Quantity
            Assert.AreEqual(3, result.Count());

            Assert.AreEqual(_mockedCustomersData[0].FirstName, resultArray[0].FirstName);
            Assert.AreEqual(_mockedCustomersData[1].FirstName, resultArray[1].FirstName);
            Assert.AreEqual(_mockedCustomersData[2].FirstName, resultArray[2].FirstName);

            Assert.AreEqual(_mockedCustomersData[0].LastName, resultArray[0].LastName);
            Assert.AreEqual(_mockedCustomersData[1].LastName, resultArray[1].LastName);
            Assert.AreEqual(_mockedCustomersData[2].LastName, resultArray[2].LastName);

            Assert.AreEqual(_mockedCustomersData[0].Orders.Count(), resultArray[0].Orders.Count());
            Assert.AreEqual(_mockedCustomersData[1].Orders.Count(), resultArray[1].Orders.Count());
            Assert.AreEqual(_mockedCustomersData[2].Orders.Count(), resultArray[2].Orders.Count());
        }

        [Test]
        public void CheckGetById_WorksCorrectly()
        {
            var result1 =  _customerService.GetCustomerById(1);
           
            //Expected Quantity
            Assert.AreEqual(2, result1.Orders.Count());
            Assert.AreEqual(_mockedCustomersData[0].FirstName, result1.FirstName);
            Assert.AreEqual(_mockedCustomersData[0].LastName,  result1.LastName);
        }

        [Test]
        public async Task CheckGetByIdAsync_WorksCorrectly()
        {
            var result1 = await  _customerService.GetCustomerByIdAsync(1, CancellationToken.None);

            //Expected Quantity
            Assert.AreEqual(2, result1.Orders.Count());
            Assert.AreEqual(_mockedCustomersData[0].FirstName, result1.FirstName);
            Assert.AreEqual(_mockedCustomersData[0].LastName, result1.LastName);
        }

        [Test]
        public void CheckCustomerCreation_WorksCorrectly()
        {
            var result3 = _customerService.CreateCustomer(_mockedCustomersData[2]);

            //Expected Quantity
            Assert.AreEqual(3, result3);
        }

        [Test]
        public async Task CheckCustomerCreationAsync_WorksCorrectly()
        {
            var result3 = await _customerService.CreateCustomerAsync(_mockedCustomersData[2], CancellationToken.None);

            //Expected Quantity
            Assert.AreEqual(3, result3);
        }

        [Test]
        public void CheckCustomerUpdate_WorksCorrectly()
        {
            var result3 = _customerService.UpdateCustomer(_mockedCustomersData[2]);

            //Expected Quantity
            Assert.AreEqual(3, result3);
        }

        [Test]
        public async Task CheckCustomerUpdateAsync_WorksCorrectly()
        {
            var result3 = await _customerService.UpdateCustomerAsync(_mockedCustomersData[2], CancellationToken.None);

            //Expected Quantity
            Assert.AreEqual(3, result3);
        }

        [Test]
        public void CheckCustomerDelete_WorksCorrectly()
        {
            var result3 = _customerService.DeleteCustomer(_mockedCustomersData[2]);

            //Expected Quantity
            Assert.AreEqual(3, result3);
        }

        [Test]
        public async Task CheckCustomerDeleteAsync_WorksCorrectly()
        {
            var result3 = await _customerService.DeleteCustomerAsync(_mockedCustomersData[2], CancellationToken.None);

            //Expected Quantity
            Assert.AreEqual(3, result3);
        }

        private void PrepareMockData()
        {
            var customer1 = MockData.GetCustomerDto(1);
            var customer2 = MockData.GetCustomerDto(2);
            var customer3 = MockData.GetCustomerDto(3);
            _mockedCustomersData = new[] { customer1, customer2, customer3 };

            CommandsMock.Setup(s => s.CreateCustomer(customer3)).Returns(3);
            CommandsMock.Setup(s => s.UpdateCustomer(customer3)).Returns(3);
            CommandsMock.Setup(s => s.DeleteCustomer(customer3)).Returns(3);

            CommandsMock.Setup(s => s.CreateCustomerAsync(customer3, CancellationToken.None)).ReturnsAsync(3);
            CommandsMock.Setup(s => s.UpdateCustomerAsync(customer3, CancellationToken.None)).ReturnsAsync(3);
            CommandsMock.Setup(s => s.DeleteCustomerAsync(customer3, CancellationToken.None)).ReturnsAsync(3);

            QueriesMock.Setup(s => s.GetAllCustomers()).Returns(new[] { customer1, customer2, customer3 });
            QueriesMock.Setup(s => s.GetAllCustomersAsync(CancellationToken.None)).ReturnsAsync(_mockedCustomersData);

            QueriesMock.Setup(s => s.GetCustomerById(1)).Returns(customer1);
            QueriesMock.Setup(s => s.GetCustomerByIdAsync(1, CancellationToken.None)).ReturnsAsync(customer1);

            QueriesMock.Setup(s => s.GetCustomerById(2)).Returns(customer2);
            QueriesMock.Setup(s => s.GetCustomerByIdAsync(2, CancellationToken.None)).ReturnsAsync(customer2);

            QueriesMock.Setup(s => s.GetCustomerById(3)).Returns(customer3);
            QueriesMock.Setup(s => s.GetCustomerByIdAsync(3, CancellationToken.None)).ReturnsAsync(customer3);
        }
    }
}