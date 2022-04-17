using CMS.Data.Access.Models;
using System;

namespace CMS.Data.Access.UnitTests.MockData
{
    internal static partial class  MockData
    {
        public static Order GetOrder (int idCustomer, int idOrder)
        {
            var order = GetOrder(idOrder, DateTime.Now, 10000, idCustomer);

            return order;
        }

        public static Customer GetCustomer(int id)
        {
            var customer = GetCustomer(id, $"Name{id}", $"Surname{id}", "11111", "Street");

            return customer;
        }

        public static Customer GetCustomer(int id, string name, string surname, string postal, string address)
        {
            return new Customer
            {
                Id = id,
                FirstName = name,
                LastName = surname,
                PostalCode = postal,
                Address = address,
                UserCreated = "Developer"
            };
        }

        public static Order GetOrder(int id, DateTime orderDate, decimal totalPrice, int customerId)
        {
            return new Order
            {
                Id = id,
                OrderDate = orderDate,
                TotalPrice = totalPrice,
                OrderCustomerId = customerId,
                UserCreated = "Developer"
            };
        }

        public static Item GetItem(int id, int orderId, int productId)
        {
            return new Item
            {
                Id = id,
                Quantity = 100.0m,
                OrderId = orderId,
                ProductId = productId,
                UserCreated = "Developer"
            };
        }

        public static Product GetProduct(int id, string name, decimal price, string measuringUnit)
        {
            return new Product
            {
                Id = id,
                Name = name,
                PricePerUnit = price,
                MeasuringUnit = measuringUnit,
                UserCreated = "Developer"
            };
        }
    }
}
