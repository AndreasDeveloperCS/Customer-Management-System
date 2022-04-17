using CMS.Data.Models.DTOs;
using NUnit.Framework;
using System.Linq;

namespace CMS.Data.Models.UnitTests
{
    /// <summary>
    /// OrderDtoUnitTests covers the OrderDto with Unit tests 
    /// </summary>
    [TestFixture]
    public class OrderDtoUnitTests
    {
        [Test]
        public void OrderDto_Instance_CreatedSuccessfully()
        {
            var orders = new OrderDto[1] { new OrderDto() };
            var customer = new CustomerDto()
            {
                Id = 1,
                FirstName = "FirstName1",
                LastName = "LastName1",
                PostalCode = "12345",
                Address = "Adress1",
                Orders = orders
            };

            Assert.NotNull(customer.Orders);
            Assert.AreEqual(1, customer.Orders.Count());
        }
    }
}