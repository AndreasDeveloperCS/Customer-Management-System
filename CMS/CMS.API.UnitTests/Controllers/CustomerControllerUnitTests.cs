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
    ///  Customer Controller Unit Tests
    /// </summary>
    [TestFixture]
    public class CustomerControllerUnitTests
    {
        private Mock<ILogger<CustomerController>> _loggerMock;
        private Mock<ICustomerService> _customerServiceMock;
        private CustomerDto[] _mockedCustomersData;

        public CustomerController _customerController { get; set; }

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<CustomerController>>();
            _customerServiceMock = new Mock<ICustomerService>();
            PrepareMockData();
            _customerController = new CustomerController(_loggerMock.Object, _customerServiceMock.Object);
        }

        /// <summary>
        /// Check whether Customers are Retreived correctly by Id Asyncronously
        /// </summary>
        [Test]
        public async Task CheckGetAllAsync_WorksCorrectly()
        {
            var result = await _customerController.GetAllAsync(CancellationToken.None) as OkObjectResult;

            //// Expected Quantity
            Assert.AreEqual(200, result?.StatusCode);

            var actualCustomers = result?.Value as CustomerDto[];

            Assert.AreEqual(3, actualCustomers?.Length);

            Assert.AreEqual(_mockedCustomersData[0].FirstName, actualCustomers[0].FirstName);
            Assert.AreEqual(_mockedCustomersData[1].FirstName, actualCustomers[1].FirstName);
            Assert.AreEqual(_mockedCustomersData[2].FirstName, actualCustomers[2].FirstName);

            Assert.AreEqual(_mockedCustomersData[0].LastName, actualCustomers[0].LastName);
            Assert.AreEqual(_mockedCustomersData[1].LastName, actualCustomers[1].LastName);
            Assert.AreEqual(_mockedCustomersData[2].LastName, actualCustomers[2].LastName);

            Assert.AreEqual(_mockedCustomersData[0].Orders.Count(), actualCustomers[0].Orders.Count());
            Assert.AreEqual(_mockedCustomersData[1].Orders.Count(), actualCustomers[1].Orders.Count());
            Assert.AreEqual(_mockedCustomersData[2].Orders.Count(), actualCustomers[2].Orders.Count());
        }

        /// <summary>
        /// Check whether Customer is Retreived correctly by Id Asyncronously
        /// </summary>
        [Test]
        public async Task CheckGetByIdAsync_WorksCorrectly()
        {
            var result1 = await _customerController.GetByIdAsync(1, CancellationToken.None) as OkObjectResult;

            //// Expected Quantity
            Assert.AreEqual(200, result1.StatusCode);
            var actualCustomers1 = result1?.Value as CustomerDto;

            ////Expected Quantity
            Assert.AreEqual(2, actualCustomers1.Orders.Count());
            Assert.AreEqual(_mockedCustomersData[0].FirstName, actualCustomers1.FirstName);
            Assert.AreEqual(_mockedCustomersData[0].LastName, actualCustomers1.LastName);

            Assert.AreEqual(_mockedCustomersData[0].Address, actualCustomers1.Address);
            Assert.AreEqual(_mockedCustomersData[0].PostalCode, actualCustomers1.PostalCode);
        }

        /// <summary>
        /// Check whether Customer is Created correctly Asyncronously
        /// </summary>
        [Test]
        public async Task CheckCustomerCreationAsync_WorksCorrectly()
        {
            var result = await _customerController.CreateAsync(_mockedCustomersData[2], CancellationToken.None) as OkObjectResult;

            Assert.AreEqual(200, result.StatusCode);

            var actualCustomers = result?.Value as string;

            //Expected Quantity
            Assert.AreEqual("New Customer ID = 3 Has been created.", actualCustomers);
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(),
                                          It.IsAny<EventId>(),
                                          It.IsAny<It.IsAnyType>(),
                                          It.IsAny<Exception>(),
                                          (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));
        }

        /// <summary>
        /// Check whether Customer is Updated correctly Asyncronously
        /// </summary>
        [Test]
        public async Task CheckCustomerUpdateAsync_WorksCorrectly()
        {
            var result = await _customerController.UpdateAsync(_mockedCustomersData[2], CancellationToken.None) as OkObjectResult;

            Assert.AreEqual(200, result.StatusCode);
            var actualCustomers = result?.Value as string;

            //Expected Quantity
            Assert.AreEqual("Customer Id = 3 Has been updated.", actualCustomers);
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(),
                                          It.IsAny<EventId>(),
                                          It.IsAny<It.IsAnyType>(),
                                          It.IsAny<Exception>(),
                                          (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));
        }

        /// <summary>
        /// Check whether Customer is Deleted correctly Asyncronously
        /// </summary>
        [Test]
        public async Task CheckCustomerDeleteAsync_WorksCorrectly()
        {
            var result = await _customerController.DeleteAsync(_mockedCustomersData[2], CancellationToken.None) as OkObjectResult;

            Assert.AreEqual(200, result.StatusCode);
            var actualCustomers = result?.Value as string;

            //Expected Quantity
            Assert.AreEqual("Customer Id = 3 Has been deleted.", actualCustomers);
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(),
                                          It.IsAny<EventId>(),
                                          It.IsAny<It.IsAnyType>(),
                                          It.IsAny<Exception>(),
                                          (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));
        }

        private void PrepareMockData()
        {
            var customer1 = MockData.MockData.GetCustomerDto(1);
            var customer2 = MockData.MockData.GetCustomerDto(2);
            var customer3 = MockData.MockData.GetCustomerDto(3);

            _mockedCustomersData = new[] { customer1, customer2, customer3 };

            _customerServiceMock.Setup(s => s.CreateCustomer(customer1)).Returns(1);
            _customerServiceMock.Setup(s => s.UpdateCustomer(customer1)).Returns(1);
            _customerServiceMock.Setup(s => s.DeleteCustomer(customer1)).Returns(1);

            _customerServiceMock.Setup(s => s.CreateCustomer(customer2)).Returns(2);
            _customerServiceMock.Setup(s => s.UpdateCustomer(customer2)).Returns(2);
            _customerServiceMock.Setup(s => s.DeleteCustomer(customer2)).Returns(2);

            _customerServiceMock.Setup(s => s.CreateCustomer(customer3)).Returns(3);
            _customerServiceMock.Setup(s => s.UpdateCustomer(customer3)).Returns(3);
            _customerServiceMock.Setup(s => s.DeleteCustomer(customer3)).Returns(3);

            _customerServiceMock.Setup(s => s.CreateCustomerAsync(customer1, CancellationToken.None)).ReturnsAsync(1);
            _customerServiceMock.Setup(s => s.UpdateCustomerAsync(customer1, CancellationToken.None)).ReturnsAsync(1);
            _customerServiceMock.Setup(s => s.DeleteCustomerAsync(customer1, CancellationToken.None)).ReturnsAsync(1);

            _customerServiceMock.Setup(s => s.CreateCustomerAsync(customer2, CancellationToken.None)).ReturnsAsync(2);
            _customerServiceMock.Setup(s => s.UpdateCustomerAsync(customer2, CancellationToken.None)).ReturnsAsync(2);
            _customerServiceMock.Setup(s => s.DeleteCustomerAsync(customer2, CancellationToken.None)).ReturnsAsync(2);

            _customerServiceMock.Setup(s => s.CreateCustomerAsync(customer3, CancellationToken.None)).ReturnsAsync(3);
            _customerServiceMock.Setup(s => s.UpdateCustomerAsync(customer3, CancellationToken.None)).ReturnsAsync(3);
            _customerServiceMock.Setup(s => s.DeleteCustomerAsync(customer3, CancellationToken.None)).ReturnsAsync(3);

            _customerServiceMock.Setup(s => s.GetAllCustomers()).Returns(new[] { customer1, customer2, customer3 });
            _customerServiceMock.Setup(s => s.GetAllCustomersAsync(CancellationToken.None)).ReturnsAsync(_mockedCustomersData);

            _customerServiceMock.Setup(s => s.GetCustomerById(1)).Returns(customer1);
            _customerServiceMock.Setup(s => s.GetCustomerByIdAsync(1, CancellationToken.None)).ReturnsAsync(customer1);

            _customerServiceMock.Setup(s => s.GetCustomerById(2)).Returns(customer2);
            _customerServiceMock.Setup(s => s.GetCustomerByIdAsync(2, CancellationToken.None)).ReturnsAsync(customer2);

            _customerServiceMock.Setup(s => s.GetCustomerById(3)).Returns(customer3);
            _customerServiceMock.Setup(s => s.GetCustomerByIdAsync(3, CancellationToken.None)).ReturnsAsync(customer3);
        }
    }
}