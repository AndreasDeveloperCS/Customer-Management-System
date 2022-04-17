using CMS.Data.Access.Commands;
using CMS.Data.Access.Interfaces;
using CMS.Data.Access.Repositories;
using CMS.Data.Access.Repositories.Interfaces;
using CMS.Data.Access.UnitTests.MockData;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CMS.Data.Access.UnitTests
{
    /// <summary>
    /// Unit Tests for the OrderCommands
    /// </summary>
    [TestFixture]
    public class OrderCommandsUnitTests
    {
        private OrderCommands _orderCommands;
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

            unitOfWorkMock.Setup(s => s.GetRepository<ICustomerCommandsRepository>())
                                       .Returns(customerCommandRepository);

            unitOfWorkMock.Setup(s => s.GetRepository<ICustomerQueriesRepository>())
                                       .Returns(customerQueriesRepository);

            unitOfWorkMock.Setup(s => s.GetRepository<IOrderCommandsRepository>())
                                       .Returns(orderCommandRepository);

            unitOfWorkMock.Setup(s => s.GetRepository<IOrderQueriesRepository>())
                                       .Returns(orderQueriesRepository);

            _orderCommands = new OrderCommands(unitOfWorkMock.Object);

        }

        [Test]
        public void CreateOrder_AddedSuccessfully()
        {
            var order = MockData.MockData.GetOrderDto(1, 4);
            order.UserCreated = "Developer";

            _orderCommands.CreateOrder(order);

            var orders = _entitiesContext.Orders.ToArray();

            Assert.AreEqual(4, orders.Length);
            Assert.AreEqual(4, orders[3].Id);
        }

        [Test]
        public async Task CreateOrderAsync_AddedSuccessfully()
        {
            var order = MockData.MockData.GetOrderDto(1, 3);

            order.UserCreated = "Developer";

            await _orderCommands.CreateOrderAsync(order, CancellationToken.None);

            var orders = _entitiesContext.Orders.ToArray();

            Assert.AreEqual(4, orders.Length);
            Assert.AreEqual(4, orders[3].Id);
        }

        [Test]
        public void UpdateCustomer_UpdatedSuccessfully()
        {
            var order = MockData.MockData.GetOrderDto(1, 3);

            var expectedOrderDate = DateTime.Now.AddDays(-10);
            order.OrderDate = expectedOrderDate;
            order.UserCreated = "Developer";

            var id = _orderCommands.UpdateOrder(order);

            var customers = _entitiesContext.Orders.ToArray();

            Assert.AreEqual(3, id);
            Assert.AreEqual(3, customers.Length);
            Assert.AreEqual(3, customers[2].Id);

            Assert.AreEqual(expectedOrderDate, customers[2].OrderDate);
        }

        [Test]
        public async Task UpdateCustomerAsync_UpdatedSuccessfully()
        {
            var order = MockData.MockData.GetOrderDto(1, 3);

            var expectedOrderDate = DateTime.Now.AddDays(-10);
            order.OrderDate = expectedOrderDate;
            order.UserCreated = "Developer";

            var id = await _orderCommands.UpdateOrderAsync(order, CancellationToken.None);

            var customers = _entitiesContext.Orders.ToArray();

            Assert.AreEqual(3, id);
            Assert.AreEqual(3, customers.Length);
            Assert.AreEqual(3, customers[2].Id);

            Assert.AreEqual(expectedOrderDate, customers[2].OrderDate);
        }

        [Test]
        public void DeleteCustomer_DeletedSuccessfully()
        {
            var order = MockData.MockData.GetOrderDto(1, 1);

            var id = _orderCommands.DeleteOrder(order);

            var leftOrders = _entitiesContext.Orders.ToArray();

            Assert.AreEqual(2, _entitiesContext.Orders.Count());
            Assert.IsNull(leftOrders.FirstOrDefault(s => s.Id == 1));
        }

        [Test]
        public async Task DeleteCustomerAsync_DeletedSuccessfully()
        {
            var order = MockData.MockData.GetOrderDto(1, 1);

            var id =  await _orderCommands.DeleteOrderAsync(order, CancellationToken.None);

            var leftOrders = _entitiesContext.Orders.ToArray();

            Assert.AreEqual(2, _entitiesContext.Orders.Count());
            Assert.IsNull(leftOrders.FirstOrDefault(s => s.Id == 1));
        }
    }
}