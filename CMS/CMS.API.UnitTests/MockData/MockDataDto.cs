using CMS.Data.Models.DTOs;
using System;

namespace CMS.API.UnitTests.MockData
{
    internal static partial class MockData
    {
        public static OrderDto GetOrderDto (int idCustomer, int idOrder)
        {
            var customer = GetCustomerDto(idCustomer, $"Name{idCustomer}", $"Surname{idCustomer}", "11111", "Street", new OrderDto[0]);

            var order = GetOrderDto(idOrder, DateTime.Now, 10000, idCustomer, customer, new ItemDto[0]);
            var item1 = GetItemDto(1, "Item1", idOrder, order);
            var item2 = GetItemDto(2, "Item2", idOrder, order);
            var item3 = GetItemDto(3, "Item3", idOrder, order);
            order.Items = new[] { item1, item2, item3 };

            customer.Orders = new[] { order };

            return order;
        }

        public static CustomerDto GetCustomerDto(int id)
        {
            var customer = GetCustomerDto(id, $"Name{id}", $"Surname{id}", "11111", "Street", new OrderDto[1]);

            var order1 = GetOrderDto(1, DateTime.Now, 10000, id, customer, new ItemDto[0]);
            var item1 = GetItemDto(1, "Item1", 1, order1);
            var item2 = GetItemDto(2, "Item2", 1, order1);
            order1.Items = new[] { item1, item2 };

            var order2 = GetOrderDto(1, DateTime.Now, 10000, id, customer, new ItemDto[1]);
            var item3 = GetItemDto(3, "Item3", 2, order2);
            var item4 = GetItemDto(4, "Item4", 2, order2);
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

        public static ItemDto GetItemDto(int id, string name, int orderId, OrderDto order)
        {
            return new ItemDto
            {
                Id = id,
                Order = order,
                OrderId = orderId
            };
        }
    }
}
