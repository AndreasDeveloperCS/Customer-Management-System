using CMS.Data.Models.DTOs;
using System;

namespace CMS.Data.Access.UnitTests.MockData
{
    internal static partial class MockData
    {
        public static OrderDto GetOrderDto (int idCustomer, int idOrder)
        {
            var order = GetOrderDto(idOrder, DateTime.Now, 10000, idCustomer);

            return order;
        }

        public static CustomerDto GetCustomerDto(int id)
        {
            var customer = GetCustomerDto(id, $"Name{id}", $"Surname{id}", "11111", "Street");

            return customer;
        }

        public static CustomerDto GetCustomerDto(int id, string name, string surname, string postal, string address)
        {
            return new CustomerDto
            {
                Id = id,
                FirstName = name,
                LastName = surname,
                PostalCode = postal,
                Address = address
            };
        }

        public static OrderDto GetOrderDto(int id, DateTime orderDate, decimal totalPrice, int customerId)
        {
            return new OrderDto
            {
                Id = id,
                OrderDate = orderDate,
                TotalPrice = totalPrice,
                CustomerId = customerId
            };
        }

        public static ItemDto GetItemDto(int id, int orderId, int productId, OrderDto order, ProductDto product)
        {
            return new ItemDto
            {
                Id = id,
                Order = order,
                OrderId = orderId,
                Product = product,
                ProductId = productId
            };
        }

        public static ProductDto GetProductDto(int id, string name, decimal price, string measuringUnit)
        {
            return new ProductDto
            {
                Id = id,
                Name = name,
                PricePerUnit = price,
                MeasuringUnit = measuringUnit
            };
        }
    }
}
