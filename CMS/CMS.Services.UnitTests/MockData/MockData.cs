using CMS.Data.Models.DTOs;
using System;

namespace CMS.Services.UnitTests.MockData
{
    internal static class MockData
    {
        public static OrderDto GetOrderDto (int idCustomer, int idOrder)
        {
            var customer = GetCustomerDto(idCustomer, $"Name{idCustomer}", $"Surname{idCustomer}", "11111", "Street", new OrderDto[0]);

            var order = GetOrderDto(idOrder, DateTime.Now, 10000, idCustomer, customer, new ItemDto[0]);

            var item1 = GetItemDto(1, idOrder, 1, order, GetProductDto(1, "Name1", 0.0m, "per/item"));
            var item2 = GetItemDto(2, idOrder, 2, order, GetProductDto(2, "Name2", 0.0m, "per/item"));
            var item3 = GetItemDto(3, idOrder, 3, order, GetProductDto(3, "Name3", 0.0m, "per/item"));

            order.Items = new[] { item1, item2, item3 };

            customer.Orders = new[] { order };

            return order;
        }

        public static CustomerDto GetCustomerDto(int id)
        {
            var customer = GetCustomerDto(id, $"Name{id}", $"Surname{id}", "11111", "Street", new OrderDto[1]);

            var order1 = GetOrderDto(1, DateTime.Now, 10000, id, customer, new ItemDto[0]);

            var item1 = GetItemDto(1, 1, 1, order1, GetProductDto(1, "Name1", 1.0m, "per/item"));
            var item2 = GetItemDto(2, 1, 2, order1, GetProductDto(2, "Name2", 1.0m, "per/item"));

            order1.Items = new[] { item1, item2 };

            var order2 = GetOrderDto(1, DateTime.Now, 10000, id, customer, new ItemDto[1]);

            var item3 = GetItemDto(1, 2, 3, order2, GetProductDto(3, "Name3", 2.0m, "per/item"));
            var item4 = GetItemDto(2, 2, 4, order2, GetProductDto(4, "Name4", 2.0m, "per/item"));

            order1.Items = new[] { item3, item4 };

            customer.Orders = new[] { order1, order2 };

            return customer;
        }

        public static CustomerDto GetCustomerDto(int id, string name, string surname, string postal, string address, OrderDto[] orders)
        {
            return new CustomerDto
            {
                Id = id,
                FirstName = name,
                LastName = surname,
                PostalCode = postal,
                Address = address,
                Orders = orders,
            };
        }

        public static OrderDto GetOrderDto(int id, DateTime orderDate, decimal totalPrice, int customerId, CustomerDto customer, ItemDto[] item)
        {
            return new OrderDto
            {
                Id = id,
                OrderDate = orderDate,
                TotalPrice = totalPrice,
                Items = item,
                Customer = customer,
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
