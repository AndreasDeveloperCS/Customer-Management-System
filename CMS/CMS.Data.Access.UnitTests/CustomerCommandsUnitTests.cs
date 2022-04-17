using CMS.Data.Access.Commands;
using CMS.Data.Access.Interfaces;
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
    /// Unit tests for the Customer Commands
    /// </summary>
    [TestFixture]
    public class CustomerCommandsUnitTests
    {
        private CustomerCommands _customerCommands;
        private TestEntities _entitiesContext;

        /// <summary>
        /// Initial preparation of mocks for the testing
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            _entitiesContext = DbPreparationHelper.GetTestEntities();

            ICustomerCommandsRepository customerCommandRepository = new CustomerCommandsRepository(_entitiesContext);
            ICustomerQueriesRepository customerQueriesRepository = new CustomerQueriesRepository(_entitiesContext);
            IOrderCommandsRepository orderCommandRepository = new OrderCommandsRepository(_entitiesContext);
            IOrderQueriesRepository orderQueriesRepository = new OrderQueriesRepository(_entitiesContext);

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();

            unitOfWorkMock.Setup(s => s.GetRepository<ICustomerCommandsRepository>()).Returns(customerCommandRepository);
            unitOfWorkMock.Setup(s => s.GetRepository<ICustomerQueriesRepository>()).Returns(customerQueriesRepository);
            unitOfWorkMock.Setup(s => s.GetRepository<IOrderCommandsRepository>()).Returns(orderCommandRepository);
            unitOfWorkMock.Setup(s => s.GetRepository<IOrderQueriesRepository>()).Returns(orderQueriesRepository);

            _customerCommands = new CustomerCommands(unitOfWorkMock.Object);
        }

        [Test]
        public void CreateCustomer_AddedSuccessfully()
        {
            var customer = MockData.MockData.GetCustomerDto(4);
            customer.UserCreated = "Developer";

            _customerCommands.CreateCustomer(customer);

            var customers = _entitiesContext.Customers.ToArray();

            Assert.AreEqual(4, customers.Length);
            Assert.AreEqual(4, customers[3].Id);
        }

        [Test]
        public async Task CreateCustomerAsync_AddedSuccessfully()
        {
            var customer = MockData.MockData.GetCustomerDto(4);
            customer.UserCreated = "Developer";

            await _customerCommands.CreateCustomerAsync(customer, CancellationToken.None);

            var customers = _entitiesContext.Customers.ToArray();

            Assert.AreEqual(4, customers.Length);
            Assert.AreEqual(4, customers[3].Id);
        }

        [Test]
        public void UpdateCustomer_UpdatedSuccessfully()
        {
            var customer = MockData.MockData.GetCustomerDto(3);
            customer.LastName = "NewSurname";
            customer.UserCreated = "Developer";

            _customerCommands.UpdateCustomer(customer);

            var customers = _entitiesContext.Customers.ToArray();

            Assert.AreEqual(3, customers.Length);
            Assert.AreEqual(3, customers[2].Id);

            Assert.AreEqual("NewSurname", customers[2].LastName);
        }

        [Test]
        public async Task UpdateCustomerAsync_UpdatedSuccessfully()
        {
            var customer = MockData.MockData.GetCustomerDto(3);
            customer.LastName = "NewSurname";
            customer.UserCreated = "Developer";

            await _customerCommands.UpdateCustomerAsync(customer, CancellationToken.None);

            var customers = _entitiesContext.Customers.ToArray();

            Assert.AreEqual(3, customers.Length);
            Assert.AreEqual(3, customers[2].Id);

            Assert.AreEqual("NewSurname", customers[2].LastName);
        }

        [Test]
        public void DeleteCustomer_DeletedSuccessfully()
        {
            var deletingCustomer = MockData.MockData.GetCustomerDto(1);
            
             _customerCommands.DeleteCustomer(deletingCustomer);

            var leftCustomers = _entitiesContext.Customers.ToArray();

            Assert.AreEqual(2, _entitiesContext.Customers.Count());
            Assert.IsNull(leftCustomers.FirstOrDefault(s => s.Id == 1));
        }

        [Test]
        public async Task DeleteCustomerAsync_DeletedSuccessfully()
        {
            var deletingCustomer = MockData.MockData.GetCustomerDto(1);
            deletingCustomer.Orders = null;

            await _customerCommands.DeleteCustomerAsync(deletingCustomer, CancellationToken.None);

            var leftCustomers = _entitiesContext.Customers.ToArray();

            Assert.AreEqual(2, _entitiesContext.Customers.Count());
            Assert.IsNull(leftCustomers.FirstOrDefault(s=>s.Id == 1));
        }
    }
}