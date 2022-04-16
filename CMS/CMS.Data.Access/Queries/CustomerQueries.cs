using CMS.Data.Models.DTOs;
using CMS.Data.Access.Queries.Interfaces;
using CMS.Data.Access.Repositories.Interfaces;
using CMS.Data.Access.Helpers;
using CMS.Data.Access.Interfaces;

namespace CMS.Data.Access.Queries
{
    public class CustomerQueries : ICustomerQueries
    {
        private ICustomerQueriesRepository _repository { get; }

        public CustomerQueries(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<ICustomerQueriesRepository>();
        }

        public IEnumerable<CustomerDto> GetAllCustomers()
        {
            var customers = _repository.GetAllCustomers();
            return customers.Count() > 0 ? customers.Select(customer => customer.ToCustomerDto()) : Enumerable.Empty<CustomerDto>();
        }

        public CustomerDto GetCustomerById(int customerId)
        {
            var customer =  _repository.GetCustomerById(customerId);
            return customer.ToCustomerDto();
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync(CancellationToken token)
        {
            var customers = await _repository.GetAllCustomersAsync(token);
            return customers.Select(customer => customer.ToCustomerDto());
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int customerId, CancellationToken token)
        {
            var customer = await _repository.GetCustomerByIdAsync(customerId, token);
            return customer.ToCustomerDto();
        }
    }
}
