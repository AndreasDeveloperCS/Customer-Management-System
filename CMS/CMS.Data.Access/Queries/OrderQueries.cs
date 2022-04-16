using CMS.Data.Models.DTOs;
using CMS.Data.Access.Queries.Interfaces;
using CMS.Data.Access.Repositories.Interfaces;
using CMS.Data.Access.Helpers;
using CMS.Data.Access.Interfaces;

namespace CMS.Data.Access.Queries
{
    public class OrderQueries : IOrderQueries
    {
        private IOrderQueriesRepository _repository { get; }

        public OrderQueries(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<IOrderQueriesRepository>();
        }

        public IEnumerable<OrderDto> GetAllOrders()
        {
           var orders = _repository.GetAllOrders().Select(ord => ord.ToOrderDto());
           return orders;
        }

        public OrderDto GetOrderById(int orderId)
        {
            var order = _repository.GetOrderById(orderId).ToOrderDto();
            return order;
        }

        public IEnumerable<OrderDto> GetOrdersByDateRange(DateTime start, DateTime end)
        {
            var orders = _repository.GetOrdersByDateRange(start, end);
            return orders.Select(ord => ord.ToOrderDto());
        }

        public IEnumerable<OrderDto> GetOrdersByCustomer(CustomerDto customer)
        {
            var orders = _repository.GetOrdersByCustomer(customer);
            return orders.Select(ord => ord.ToOrderDto());
        }

        public IEnumerable<OrderDto> GetOrdersByCustomerDateRange(CustomerDto customer, DateTime start, DateTime end)
        {
            var orders = _repository.GetOrdersByCustomerDateRange(customer, start, end);
            return orders.Select(ord => ord.ToOrderDto());
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync(CancellationToken token)
        {
            var orders = await _repository.GetAllOrdersAsync(token);
            return orders.Select(ord => ord.ToOrderDto());
        }

        public async Task<OrderDto> GetOrderByIdAsync(int orderId, CancellationToken token)
        {
            var order = await _repository.GetOrderByIdAsync(orderId, token);
            return order.ToOrderDto();
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByDateRangeAsync(DateTime start, DateTime end, CancellationToken token)
        {
            var orders = await _repository.GetOrdersByDateRangeAsync(start, end, token);
            return orders.Select(ord => ord.ToOrderDto());
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerAsync(CustomerDto customer, CancellationToken token)
        {
            var orders = await _repository.GetOrdersByCustomerAsync(customer, token);
            return orders.Select(ord => ord.ToOrderDto());
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerDateRangeAsync(CustomerDto customer, DateTime start, DateTime end, CancellationToken token)
        {
            var orders = await _repository.GetOrdersByCustomerDateRangeAsync(customer, start, end, token);
            return orders.Select(ord => ord.ToOrderDto());
        }
    }
}
