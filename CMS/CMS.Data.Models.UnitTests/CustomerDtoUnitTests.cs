using CMS.Data.Models.DTOs;
using NUnit.Framework;
using System;
using System.Linq;

namespace CMS.Data.Models.UnitTests
{
    /// <summary>
    /// CustomerDtoUnitTests covers the CustomerDto with Unit tests 
    /// </summary>
    [TestFixture]
    public class CustomerDtoUnitTests
    {
        [Test]
        public void CustomerDto_Instance_CreatedSuccessfully()
        {
            var customer = new CustomerDto()
            {
                Id = 1,
                FirstName = "FirstName1",
                LastName = "LastName1",
                PostalCode = "12345",
                Address = "Adress1"
            };

            var product = new ProductDto()
            {
                Id = 1,
                Name = "Product 1",
                PricePerUnit = 10,
                MeasuringUnit = "per/unit"
            };
            var item = new ItemDto
            {
                Id = 1,
                Quantity = 100,
                OrderId = 1,
                ProductId = 1,
                Product = product
            };
            var items = new ItemDto[1]
            {
                item
            };
            var order = new OrderDto()
            {
                Id = 1,
                CustomerId = 1,
                Customer = customer,
                OrderDate = DateTime.Now,
                TotalPrice = 200.0m,
                Items = items
            };
            item.Order = order;

            Assert.NotNull(order.Items);
            Assert.NotNull(order.Items.First().Order);
            Assert.AreEqual(1, order.Items.Count());
            Assert.AreEqual("Product 1", order.Items.First().Product.Name);
            Assert.AreEqual(100, order.Items.First().Quantity);
        }
    }
}