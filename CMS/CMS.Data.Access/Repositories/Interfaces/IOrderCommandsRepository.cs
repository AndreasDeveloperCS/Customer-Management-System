using CMS.Data.Access.Interfaces;
using CMS.Data.Access.Models;

namespace CMS.Data.Access.Repositories.Interfaces
{
    public interface IOrderCommandsRepository : IRepository
    {
        int CreateOrder(Order order);
        int UpdateOrder(Order order);
        int DeleteOrder(Order order);
        IEnumerable<int> DeleteRangeOrders(IEnumerable<Order> enumerable);

        Task<int> CreateOrderAsync(Order order, CancellationToken token);
        Task<int> UpdateOrderAsync(Order order, CancellationToken token);
        Task<int> DeleteOrderAsync(Order order, CancellationToken token);
        Task<IEnumerable<int>> DeleteRangeOrdersAsync(IEnumerable<Order> enumerable, CancellationToken token);
       
    }
}
