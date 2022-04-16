using CMS.Data.Access.Models;
using CMS.Data.Access.Repositories.Interfaces;

namespace CMS.Data.Access.Repositories
{
    public class CustomerCommandsRepository : ICustomerCommandsRepository
    {
        private readonly EntitiesContext _context;

        public CustomerCommandsRepository(EntitiesContext context)
        {
            _context = context;
        }

        public int CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
             _context.SaveChanges();
            return customer.Id;
        }

        public int DeleteCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return customer.Id;
        }

        public int UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
            return customer.Id;
        }

        public async Task<int> CreateCustomerAsync(Customer customer, CancellationToken token)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync(token);
            return customer.Id;
        }

        public async Task<int> UpdateCustomerAsync(Customer customer, CancellationToken token)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync(token);
            return customer.Id;
        }

        public async Task<int> DeleteCustomerAsync(Customer customer, CancellationToken token)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync(token);
            return customer.Id;
        }
    }
}
