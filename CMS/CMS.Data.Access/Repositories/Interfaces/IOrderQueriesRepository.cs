using CMS.Data.Models.DTOs;
using CMS.Data.Access.Models;
using CMS.Data.Access.Interfaces;

namespace CMS.Data.Access.Repositories.Interfaces
{
    public interface IOrderQueriesRepository : IRepository
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(int id);

        Task<IEnumerable<Order>> GetAllOrdersAsync(CancellationToken token);
        Task<Order> GetOrderByIdAsync(int id, CancellationToken token);

        IEnumerable<Order> GetOrdersByDateRange(DateTime start, DateTime end);
        IEnumerable<Order> GetOrdersByCustomer(CustomerDto customer);
        IEnumerable<Order> GetOrdersByCustomerDateRange(CustomerDto customer, DateTime start, DateTime end);

        Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime start, DateTime end, CancellationToken token);
        Task<IEnumerable<Order>> GetOrdersByCustomerAsync(CustomerDto customer, CancellationToken token);
        Task<IEnumerable<Order>> GetOrdersByCustomerDateRangeAsync(CustomerDto customer, DateTime start, DateTime end, CancellationToken token);
    }
}
