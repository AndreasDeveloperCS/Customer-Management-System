using CMS.Data.Access.Models;
using CMS.Data.Access.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CMS.Data.Access.Repositories
{
    public class CustomerQueriesRepository : ICustomerQueriesRepository
    {
        private readonly EntitiesContext _context;

        public CustomerQueriesRepository(EntitiesContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return GetIncluded();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync(CancellationToken token)
        {
            var customersArray = await GetIncluded().ToArrayAsync(token);

            return await GetIncluded().ToArrayAsync(token);
        }

        public Customer GetCustomerById(int id)
        {
            var product = GetIncluded().FirstOrDefault(a => a.Id == id);
            return product;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id, CancellationToken token)
        {
            var product = await GetIncluded().FirstOrDefaultAsync(a => a.Id == id, token);
            return product;
        }

        private IQueryable<Customer> GetIncluded()
        {
            return _context.Customers
                           .Include<Customer, IEnumerable<Order>>((ww) => ww.Orders)
                           .ThenInclude<Customer, Order, IEnumerable <Item>>(s=>s.Items)
                           .ThenInclude<Customer, Item, Product>(s => s.Product)
                           .AsNoTracking();
        }
    }
}
