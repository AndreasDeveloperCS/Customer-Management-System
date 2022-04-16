using CMS.Data.Access.Commands.Interfaces;
using CMS.Data.Access.Queries.Interfaces;
using CMS.Data.Models.DTOs;

namespace CMS.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderCommands _commands;
        private readonly IOrderQueries _queries;

        public OrderService(IOrderCommands commands, IOrderQueries queries)
        {
            _commands = commands;
            _queries = queries;
        }

        public IEnumerable<OrderDto> GetAllOrders()
        {
            return _queries.GetAllOrders();
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync(CancellationToken token)
        {
            return await _queries.GetAllOrdersAsync(token);
        }

        public OrderDto GetOrderById(int id)
        {
            return _queries.GetOrderById(id);
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id, CancellationToken token)
        {
            return await _queries.GetOrderByIdAsync(id, token);
        }

        public IEnumerable<OrderDto> GetOrdersByCustomer(CustomerDto customer)
        {
            return _queries.GetOrdersByCustomer(customer);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerAsync(CustomerDto customer, CancellationToken token)
        {
            return await _queries.GetOrdersByCustomerAsync(customer, token);
        }

        public IEnumerable<OrderDto> GetOrdersByCustomerDateRange(CustomerDto customer, DateTime start, DateTime end)
        {
            return _queries.GetOrdersByCustomerDateRange(customer, start, end);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerDateRangeAsync(CustomerDto customer, DateTime start, DateTime end, CancellationToken token)
        {
            return await _queries.GetOrdersByCustomerDateRangeAsync(customer, start, end, token);
        }

        public IEnumerable<OrderDto> GetOrdersByDateRange(DateTime start, DateTime end)
        {
            return _queries.GetOrdersByDateRange(start, end);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByDateRangeAsync(DateTime start, DateTime end, CancellationToken token)
        {
            return await _queries.GetOrdersByDateRangeAsync(start, end, token);
        }

        public int CreateOrder(OrderDto order)
        {
            return _commands.CreateOrder(order);
        }

        public int UpdateOrder(OrderDto order)
        {
            return _commands.UpdateOrder(order);
        }

        public int DeleteOrder(OrderDto order)
        {
            return _commands.DeleteOrder(order);
        }

        public IEnumerable<int> DeleteRangeOrders(IEnumerable<OrderDto> enumerable)
        {
            return _commands.DeleteRangeOrders(enumerable);
        }

        public async Task<int> CreateOrderAsync(OrderDto order, CancellationToken token)
        {
            return await _commands.CreateOrderAsync(order, token);
        }

        public async Task<int> UpdateOrderAsync(OrderDto order, CancellationToken token)
        {
            return await _commands.UpdateOrderAsync(order, token);
        }

        public async Task<int> DeleteOrderAsync(OrderDto order, CancellationToken token)
        {
            return await _commands.DeleteOrderAsync(order, token);
        }

        public async Task<IEnumerable<int>> DeleteRangeOrdersAsync(IEnumerable<OrderDto> orders, CancellationToken token)
        {
            return await _commands.DeleteRangeOrdersAsync(orders, token);
        }

    }
}
