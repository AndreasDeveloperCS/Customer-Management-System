using CMS.Data.Models.DTOs;
using CMS.Data.Access.Models;

namespace CMS.Data.Access.Helpers
{
    public static class ConverterHelper
    {
        public static CustomerDto ToCustomerDto(this Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }

            var dto = new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PostalCode = customer.PostalCode,
                Address = customer.Address,
                Orders = customer.Orders?.Select(s => s.ToOrderDto()),
            };
            return dto;
        }

        public static OrderDto ToOrderDto(this Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException("order");
            }
            var dto =  new OrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalPrice = order.Items?.Sum(el => el.Product.PricePerUnit * el.Quantity) ?? 0.0m,
                Items = order.Items?.Select(s => s.ToItemDto()),
                CustomerId = order.OrderCustomerId
            };
            return dto;
        }

        public static ItemDto ToItemDto(this Item item)
        {
            if(item == null)
            {
                throw new ArgumentNullException("item");
            }
            var dto = new ItemDto
            {
                Id = item.Id,
                Quantity = item.Quantity,
                Product = item.Product?.ToProductDto(),
                OrderId = item.OrderId,
            };

            return dto;
        }

        public static ProductDto ToProductDto(this Product item)
        {
            var dto = new ProductDto
            {
                Id = item.Id,
                Name = item.Name,
                PricePerUnit = item.PricePerUnit,
                MeasuringUnit = item.MeasuringUnit
            };
            return dto;
        }

        public static Customer ToCustomer(this CustomerDto customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }
            return new Customer
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PostalCode = customer.PostalCode,
                Address = customer.Address,
                UserCreated = customer.UserCreated,
                UserModified = customer.UserModified
            };
        }

        public static Order ToOrder(this OrderDto order)
        {
            if (order == null)
            {
                throw new ArgumentNullException("order");
            }
            return new Order
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Items = order.Items?.Select(s => s.ToItem()),
                OrderCustomerId = order.CustomerId,
                UserCreated = order.UserCreated,
                UserModified = order.UserModified
            };
        }

        public static Item ToItem(this ItemDto item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            return new Item
            {
                Id = item.Id,
                Quantity = item.Quantity,
                Order = item.Order.ToOrder(),
                OrderId = item.OrderId,
                UserCreated = item.UserCreated,
                UserModified = item.UserModified
            };
        }
    }
}
