using CMS.Data.Access.Models;
using CMS.Data.Access.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            var deletingItem = _context.Customers.Find(customer.Id);
            if(deletingItem != null)
            {
                if(_context.Entry(deletingItem).State == EntityState.Detached)
                {
                    _context.Customers.Attach(deletingItem);
                }
           
                _context.Customers.Remove(deletingItem);

                _context.SaveChanges();
            }
            return customer.Id;
        }

        public int UpdateCustomer(Customer customer)
        {
            var trackedCustomer = _context.Customers.Find(customer.Id);
            if (trackedCustomer != null)
            {
                _context.Entry(trackedCustomer).CurrentValues.SetValues(customer);
            }
            else
            {
                _context.Customers.Add(customer);
            }
            _context.SaveChanges();
            return customer.Id;
        }

        public async Task<int> CreateCustomerAsync(Customer customer, CancellationToken token)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync(token);
            return customer.Id;
        }

        public async Task<int> UpdateCustomerAsync(Customer customer, CancellationToken token)
        {
            var trackedCustomer = _context.Customers.Find(customer.Id);
            if (trackedCustomer != null)
            {
                _context.Entry(trackedCustomer).CurrentValues.SetValues(customer);
            }
            else
            {
                _context.Customers.Add(customer);
            }
         
            await _context.SaveChangesAsync(token);
            return customer.Id;
        }

        public async Task<int> DeleteCustomerAsync(Customer customer, CancellationToken token)
        {
            var deletingItem = _context.Customers.Find(customer.Id);
            if (deletingItem != null)
            {
                if (_context.Entry(deletingItem).State == EntityState.Detached)
                {
                    _context.Customers.Attach(deletingItem);
                }

                _context.Customers.Remove(deletingItem);

                await _context.SaveChangesAsync(token);
            }

            return customer.Id;
        }
    }
}
