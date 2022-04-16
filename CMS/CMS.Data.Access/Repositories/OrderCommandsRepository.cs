using CMS.Data.Access.Models;
using CMS.Data.Access.Repositories.Interfaces;

namespace CMS.Data.Access.Repositories
{
    public class OrderCommandsRepository : IOrderCommandsRepository
    {
        private readonly EntitiesContext _context;
        public OrderCommandsRepository(EntitiesContext context)
        {
            _context = context;
        }
        public int CreateOrder(Order order)
        {
            _context.Orders.Add(order);
             _context.SaveChanges();
            return order.Id;
        }

        public async Task<int> CreateOrderAsync(Order order, CancellationToken token)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync(token);
            return order.Id;
        }

        public int DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return order.Id;
        }

        public async Task<int> DeleteOrderAsync(Order order, CancellationToken token)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync(token);
            return order.Id;
        }

        public IEnumerable<int> DeleteRangeOrders(IEnumerable<Order> orders)
        {
            _context.Orders.RemoveRange(orders);
             _context.SaveChanges();
            return orders.Select(s=>s.Id);
        }

        public async Task<IEnumerable<int>> DeleteRangeOrdersAsync(IEnumerable<Order> orders, CancellationToken token)
        {
            _context.Orders.RemoveRange(orders);
            await _context.SaveChangesAsync(token);
            return orders.Select(s => s.Id);
        }

        public int UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
             _context.SaveChanges();
            return order.Id;
        }

        public async Task<int> UpdateOrderAsync(Order order, CancellationToken token)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync(token);
            return order.Id;
        }
    }
}
