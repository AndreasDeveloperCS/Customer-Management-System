using CMS.Data.Access.Models;
using CMS.Data.Access.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            var deletingItem = _context.Orders.Find(order.Id);
            if (deletingItem != null)
            {
                if (_context.Entry(deletingItem).State == EntityState.Detached)
                {
                    _context.Orders.Attach(deletingItem);
                }

                _context.Orders.Remove(deletingItem);

                 _context.SaveChanges();
            }
            return order.Id;
        }

        public async Task<int> DeleteOrderAsync(Order order, CancellationToken token)
        {
            var deletingItem = _context.Orders.Find(order.Id);
            if (deletingItem != null)
            {
                if (_context.Entry(deletingItem).State == EntityState.Detached)
                {
                    _context.Orders.Attach(deletingItem);
                }

                _context.Orders.Remove(deletingItem);

                await _context.SaveChangesAsync(token);
            }
         
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
            var trackedOrder = _context.Orders.Find(order.Id);
            if(trackedOrder != null)
            {
                _context.Entry(trackedOrder).CurrentValues.SetValues(order);
            }
            else
            {
                _context.Orders.Add(order);
            }

            _context.SaveChanges();
            return order.Id;
        }

        public async Task<int> UpdateOrderAsync(Order order, CancellationToken token)
        {
            var trackedOrder = _context.Orders.Find(order.Id);
            if (trackedOrder != null)
            {
                _context.Entry(trackedOrder).CurrentValues.SetValues(order);
            }
            else
            {
                _context.Orders.Add(order);
            }

            await _context.SaveChangesAsync(token);
            return order.Id;
        }
    }
}
