using CMS.Data.Access.Configuration;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CMS.Data.Access.UnitTests.MockData
{
    internal class DbPreparationHelper
    {
        public static TestEntities GetTestEntities()
        {
             DbContextOptions<TestEntities> _dbContextOptions = new DbContextOptionsBuilder<TestEntities>()
                                             .UseInMemoryDatabase(databaseName: "CMSDB")
                                             .EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: true)
                                             .Options;

            Mock<IConfigurationManagerService> configurationService = new Mock<IConfigurationManagerService>();
            configurationService.Setup(s => s.GetDbContextOptions(It.IsAny<string>())).Returns(_dbContextOptions);
            var testEntities = new TestEntities(configurationService.Object);

            InitTestData(testEntities);
            return testEntities;
        }

        private static void InitTestData(TestEntities entities)
        {
            var customer1 = MockData.GetCustomer(1);
            var customer2 = MockData.GetCustomer(2);
            var customer3 = MockData.GetCustomer(3);

            var order1 = MockData.GetOrder(1, 1);
            var order2 = MockData.GetOrder(2, 2);
            var order3 = MockData.GetOrder(3, 3);

            var product1 = MockData.GetProduct(1, "Item1", 100.0m, "per/item");
            var product2 = MockData.GetProduct(2, "Item2", 200.0m, "per/item");
            var product3 = MockData.GetProduct(3, "Item3", 300.0m, "per/item");

            var item1 = MockData.GetItem(1, 1, 1);
            var item2 = MockData.GetItem(2, 2, 2);
            var item3 = MockData.GetItem(3, 3, 3);

            entities.Products.AddRange(new Models.Product[] { product1, product2, product3 });
            entities.Customers.AddRange(new Models.Customer[] { customer1, customer2, customer3 });
            entities.Items.AddRange(new Models.Item[] { item1, item2, item3 });
            entities.Orders.AddRange(new Models.Order[] { order1, order2, order3 });


            entities.SaveChanges();
        }
    }
      
}
