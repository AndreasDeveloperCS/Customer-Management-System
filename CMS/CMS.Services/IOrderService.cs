using CMS.Data.Models.DTOs;

namespace CMS.Services
{
    public interface IOrderService
    {
        int CreateOrder(OrderDto order);
        Task<int> CreateOrderAsync(OrderDto order, CancellationToken token);
        int DeleteOrder(OrderDto order);
        Task<int> DeleteOrderAsync(OrderDto order, CancellationToken token);
        IEnumerable<int> DeleteRangeOrders(IEnumerable<OrderDto> enumerable);
        Task<IEnumerable<int>> DeleteRangeOrdersAsync(IEnumerable<OrderDto> orders, CancellationToken token);
        IEnumerable<OrderDto> GetAllOrders();
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync(CancellationToken token);
        OrderDto GetOrderById(int id);
        Task<OrderDto> GetOrderByIdAsync(int id, CancellationToken token);
        IEnumerable<OrderDto> GetOrdersByCustomer(CustomerDto customer);
        Task<IEnumerable<OrderDto>> GetOrdersByCustomerAsync(CustomerDto customer, CancellationToken token);
        IEnumerable<OrderDto> GetOrdersByCustomerDateRange(CustomerDto customer, DateTime start, DateTime end);
        Task<IEnumerable<OrderDto>> GetOrdersByCustomerDateRangeAsync(CustomerDto customer, DateTime start, DateTime end, CancellationToken token);
        IEnumerable<OrderDto> GetOrdersByDateRange(DateTime start, DateTime end);
        Task<IEnumerable<OrderDto>> GetOrdersByDateRangeAsync(DateTime start, DateTime end, CancellationToken token);
        int UpdateOrder(OrderDto order);
        Task<int> UpdateOrderAsync(OrderDto order, CancellationToken token);
    }
}