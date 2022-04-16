using CMS.Data.Access.Models;
using CMS.Data.Access.Repositories.Interfaces;
using CMS.Data.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CMS.Data.Access.Repositories
{
    public class OrderQueriesRepository : IOrderQueriesRepository
    {
        private readonly EntitiesContext _context;

        public OrderQueriesRepository(EntitiesContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return GetIncluded();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(CancellationToken token)
        {
            return await GetIncluded().ToArrayAsync(token);
        }

        public Order GetOrderById(int id)
        {
            return GetIncluded().FirstOrDefault(s => s.Id == id);
        }

        public async Task<Order> GetOrderByIdAsync(int id, CancellationToken token)
        {
            return await GetIncluded().FirstOrDefaultAsync(a => a.Id == id, token);
        }

        public  IEnumerable<Order> GetOrdersByCustomer(CustomerDto customer)
        {
            return  _context.Orders.AsNoTracking()
                            .Where(s=>s.OrderCustomerId == customer.Id)
                            .OrderBy(s => s.OrderDate);
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(CustomerDto customer, CancellationToken token)
        {
            return await GetIncluded()
                        .Where(s => s.OrderCustomerId == customer.Id)
                        .OrderBy(s => s.OrderDate)
                        .ToArrayAsync(token);
        }

        public IEnumerable<Order> GetOrdersByCustomerDateRange(CustomerDto customer, DateTime start, DateTime end)
        {
            return GetIncluded()
                  .Where(s => s.OrderCustomerId == customer.Id && s.OrderDate >= start && s.OrderDate <= end)
                  .OrderBy(s => s.OrderDate);
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerDateRangeAsync(CustomerDto customer, DateTime start, DateTime end, CancellationToken token)
        {
            return await GetIncluded()
                        .Where(s => s.OrderCustomerId == customer.Id && s.OrderDate >= start && s.OrderDate <= end)
                        .OrderBy(s=>s.OrderDate)
                        .ToArrayAsync(token);
        }

        public IEnumerable<Order> GetOrdersByDateRange(DateTime start, DateTime end)
        {
            return GetIncluded()
                   .Where(s => s.OrderDate >= start && s.OrderDate <= end)
                   .OrderBy(s => s.OrderDate);
        }

        public async Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime start, DateTime end, CancellationToken token)
        {
            return await GetIncluded()
                        .Where(s => s.OrderDate >= start && s.OrderDate <= end)
                        .OrderBy(s => s.OrderDate)
                        .ToArrayAsync(token);
        }

        private IQueryable<Order> GetIncluded()
        {
            return _context.Orders
                           .Include(s=>s.Items)
                           .ThenInclude(s=>s.Product)
                           .AsNoTracking();
        }
    }
}
