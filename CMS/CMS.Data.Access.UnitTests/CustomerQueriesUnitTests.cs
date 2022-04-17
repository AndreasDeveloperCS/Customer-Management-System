using CMS.Data.Access.Interfaces;
using CMS.Data.Access.Queries;
using CMS.Data.Access.Repositories;
using CMS.Data.Access.Repositories.Interfaces;
using CMS.Data.Access.UnitTests.MockData;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CMS.Data.Access.UnitTests
{
    /// <summary>
    /// Unit Tests for Customer Queries
    /// </summary>
    [TestFixture]
    public class CustomerQueriesUnitTests
    {
        private CustomerQueries _customerQueries;

        /// <summary>
        /// Initial preparation of mocks for the testing
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            TestEntities entitiesContext = DbPreparationHelper.GetTestEntities();

            ICustomerCommandsRepository customerCommandRepository = new CustomerCommandsRepository(entitiesContext);
            ICustomerQueriesRepository customerQueriesRepository = new CustomerQueriesRepository(entitiesContext);
            IOrderCommandsRepository orderCommandRepository = new OrderCommandsRepository(entitiesContext);
            IOrderQueriesRepository orderQueriesRepository = new OrderQueriesRepository(entitiesContext);

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();

            unitOfWorkMock.Setup(s => s.GetRepository<ICustomerCommandsRepository>()).Returns(customerCommandRepository);
            unitOfWorkMock.Setup(s => s.GetRepository<ICustomerQueriesRepository>()).Returns(customerQueriesRepository);
            unitOfWorkMock.Setup(s => s.GetRepository<IOrderCommandsRepository>()).Returns(orderCommandRepository);
            unitOfWorkMock.Setup(s => s.GetRepository<IOrderQueriesRepository>()).Returns(orderQueriesRepository);

            _customerQueries = new CustomerQueries(unitOfWorkMock.Object);

        }

        [Test]
        public void GetAll_RetrivesRecordsSuccessfully()
        {
            var customers = _customerQueries.GetAllCustomers();

            Assert.AreEqual(3, customers.Count());
        }

        [Test]
        public async Task GetAllAsync_RetrivesRecordsSuccessfully()
        {
            var customers = await  _customerQueries.GetAllCustomersAsync(CancellationToken.None);

            Assert.AreEqual(3, customers.Count());
        }

        [Test]
        public void GetId_RetrivesRecordsSuccessfully()
        {
            var customer = _customerQueries.GetCustomerById(1);

            Assert.AreEqual(1, customer.Id);
            Assert.AreEqual("Name1", customer.FirstName);
            Assert.AreEqual("Surname1", customer.LastName);
            Assert.AreEqual("11111", customer.PostalCode);
            Assert.AreEqual("Street", customer.Address);
        }

        [Test]
        public async Task GetIdAsync_RetrivesRecordsSuccessfully()
        {
            var customer = await _customerQueries.GetCustomerByIdAsync(1, CancellationToken.None);

            Assert.AreEqual(1, customer.Id);
            Assert.AreEqual("Name1", customer.FirstName);
            Assert.AreEqual("Surname1", customer.LastName);
            Assert.AreEqual("11111", customer.PostalCode);
            Assert.AreEqual("Street", customer.Address);
        }
    }
}