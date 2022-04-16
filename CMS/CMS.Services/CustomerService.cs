using CMS.Data.Access.Commands.Interfaces;
using CMS.Data.Models.DTOs;
using CMS.Data.Access.Queries.Interfaces;

namespace CMS.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerCommands _commands;
        private readonly ICustomerQueries _queries;

        public CustomerService(ICustomerCommands commands, ICustomerQueries queries)
        {
            _commands = commands;
            _queries = queries;
        }

        public IEnumerable<CustomerDto> GetAllCustomers()
        {
            return _queries.GetAllCustomers();
        }
        public CustomerDto GetCustomerById(int customerId)
        {
            return _queries.GetCustomerById(customerId);
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync(CancellationToken token)
        {
            return await _queries.GetAllCustomersAsync(token);
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int customerId, CancellationToken token)
        {
            return await _queries.GetCustomerByIdAsync(customerId, token);
        }

        public int CreateCustomer(CustomerDto customer)
        {
            return _commands.CreateCustomer(customer);
        }

        public int DeleteCustomer(CustomerDto customer)
        {
            return _commands.DeleteCustomer(customer);
        }

        public int UpdateCustomer(CustomerDto customer)
        {
            return _commands.UpdateCustomer(customer);
        }

        public async Task<int> CreateCustomerAsync(CustomerDto customer, CancellationToken token)
        {
            return await _commands.CreateCustomerAsync(customer, token);
        }

        public async Task<int> UpdateCustomerAsync(CustomerDto customer, CancellationToken token)
        {
            return await _commands.UpdateCustomerAsync(customer, token);
        }

        public async Task<int> DeleteCustomerAsync(CustomerDto customer, CancellationToken token)
        {
            return await _commands.UpdateCustomerAsync(customer, token);
        }
    }
}