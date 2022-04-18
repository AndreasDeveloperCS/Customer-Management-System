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
    /// 
    /// </summary>
    [TestFixture]
    public class OrderQueriesUnitTests
    {
        private OrderQueries _orderQueries;

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

            _orderQueries = new OrderQueries(unitOfWorkMock.Object);

        }


        [Test]
        public void GetAll_RetrivesRecordsSuccessfully()
        {
            var orders = _orderQueries.GetAllOrders();

            Assert.AreEqual(3, orders.Count());
        }

        [Test]
        public async Task GetAllAsync_RetrivesRecordsSuccessfully()
        {
            var orders = await _orderQueries.GetAllOrdersAsync(CancellationToken.None);

            Assert.AreEqual(3, orders.Count());
        }

        [Test]
        public void GetId_RetrivesRecordsSuccessfully()
        {
            var order = _orderQueries.GetOrderById(1);

            Assert.AreEqual(1, order.Id);
            Assert.AreEqual(1, order.Items.Count());
            Assert.AreEqual(10000.00M, order.TotalPrice);
        }

        /// <summary>
        ///  Get By Id Asyncronously
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetByIdAsync_RetrivesRecordsSuccessfully()
        {
            var order = await _orderQueries.GetOrderByIdAsync(1, CancellationToken.None);

            Assert.AreEqual(1, order.Id);
            Assert.AreEqual(1, order.Items.Count());
            Assert.AreEqual(10000.00M, order.TotalPrice);
        }
    }
}