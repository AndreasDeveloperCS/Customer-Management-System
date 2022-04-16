using CMS.Data.Access.Models;
using System;

namespace CMS.API.UnitTests.MockData
{
    internal static partial class  MockData
    {
        public static Order GetOrder (int idCustomer, int idOrder)
        {
            var customer = GetCustomer(idCustomer, $"Name{idCustomer}", $"Surname{idCustomer}", "11111", "Street", new Order[0]);

            var order = GetOrder(idOrder, DateTime.Now, 10000, idCustomer, customer, new Item[0]);

            var item1 = GetItem(1, "Item1", idOrder, order);
            var item2 = GetItem(2, "Item2", idOrder, order);
            var item3 = GetItem(3, "Item3", idOrder, order);

            order.Items = new[] { item1, item2, item3 };

            customer.Orders = new[] { order };

            return order;
        }

        public static Customer GetCustomer(int id)
        {
            var customer = GetCustomer(id, $"Name{id}", $"Surname{id}", "11111", "Street", new Order[1]);

            var order1 = GetOrder(1, DateTime.Now, 10000, id, customer, new Item[0]);

            var item1 = GetItem(1, "Item1", 1, order1);
            var item2 = GetItem(2, "Item2", 1, order1);

            order1.Items = new[] { item1, item2 };

            var order2 = GetOrder(1, DateTime.Now, 10000, id, customer, new Item[1]);

            var item3 = GetItem(3, "Item3", 2, order2);
            var item4 = GetItem(4, "Item4", 2, order2);

            order1.Items = new[] { item3, item4 };

            customer.Orders = new[] { order1, order2 };

            return customer;
        }

        public static Customer GetCustomer(int id, string name, string surname, string postal, string address, Order[] orders)
        {
            return new Customer
            {
                Id = id,
                FirstName = name,
                LastName = surname,
                PostalCode = postal,
                Address = address,
                Orders = orders,
            };
        }

        public static Order GetOrder(int id, DateTime orderDate, decimal totalPrice, int customerId, Customer customer, Item[] item)
        {
            return new Order
            {
                Id = id,
                OrderDate = orderDate,
                TotalPrice = totalPrice,
                Items = item,
                Customer = customer,
                OrderCustomerId = customerId
            };
        }

        public static Item GetItem(int id, string name, int orderId, Order order)
        {
            return new Item
            {
                Id = id,
                Order = order,
                OrderId = orderId
            };
        }
    }
}
