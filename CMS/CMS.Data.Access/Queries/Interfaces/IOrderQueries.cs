using CMS.Data.Models.DTOs;

namespace CMS.Data.Access.Queries.Interfaces
{
    public interface IOrderQueries
    {
        IEnumerable<OrderDto> GetAllOrders();
        OrderDto GetOrderById(int orderId);
        
        IEnumerable<OrderDto> GetOrdersByDateRange(DateTime start, DateTime end);
        IEnumerable<OrderDto> GetOrdersByCustomer(CustomerDto customer);
        IEnumerable<OrderDto> GetOrdersByCustomerDateRange(CustomerDto customer, DateTime start, DateTime end);

        Task<IEnumerable<OrderDto>> GetAllOrdersAsync(CancellationToken token);
        Task<OrderDto> GetOrderByIdAsync(int orderId, CancellationToken token);

        Task<IEnumerable<OrderDto>> GetOrdersByDateRangeAsync(DateTime start, DateTime end, CancellationToken token);
        Task<IEnumerable<OrderDto>> GetOrdersByCustomerAsync(CustomerDto customer, CancellationToken token);
        Task<IEnumerable<OrderDto>> GetOrdersByCustomerDateRangeAsync(CustomerDto customer, DateTime start, DateTime end, CancellationToken token);

    }
}
