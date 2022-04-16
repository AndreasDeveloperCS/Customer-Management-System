using CMS.Data.Models.DTOs;

namespace CMS.Data.Access.Commands.Interfaces
{
    public interface IOrderCommands
    {
        int CreateOrder(OrderDto order);
        int UpdateOrder(OrderDto order);
        int DeleteOrder(OrderDto order);
        IEnumerable<int> DeleteRangeOrders(IEnumerable<OrderDto> orders);

        Task<int> CreateOrderAsync(OrderDto order, CancellationToken token);
        Task<int> UpdateOrderAsync(OrderDto order, CancellationToken token);
        Task<int> DeleteOrderAsync(OrderDto order, CancellationToken token);
        Task<IEnumerable<int>> DeleteRangeOrdersAsync(IEnumerable<OrderDto> orders, CancellationToken token);
    }
}
