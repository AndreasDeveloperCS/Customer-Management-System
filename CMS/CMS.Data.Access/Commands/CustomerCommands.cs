using CMS.Data.Access.Commands.Interfaces;
using CMS.Data.Access.Helpers;
using CMS.Data.Access.Interfaces;
using CMS.Data.Access.Repositories.Interfaces;
using CMS.Data.Models.DTOs;

namespace CMS.Data.Access.Commands
{
    public class CustomerCommands : ICustomerCommands
    {
        private ICustomerCommandsRepository _repository;

        public CustomerCommands(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<ICustomerCommandsRepository>();
        }

        public int CreateCustomer(CustomerDto customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            return _repository.CreateCustomer(customer.ToCustomer());
        }

        public async Task<int> CreateCustomerAsync(CustomerDto customer, CancellationToken token)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            return await _repository.CreateCustomerAsync(customer.ToCustomer(), token);
        }

        public int UpdateCustomer(CustomerDto customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            return _repository.UpdateCustomer(customer.ToCustomer());
        }

        public async Task<int> UpdateCustomerAsync(CustomerDto customer, CancellationToken token)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            return await _repository.UpdateCustomerAsync(customer.ToCustomer(), token);
        }

        public int DeleteCustomer(CustomerDto customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            return _repository.DeleteCustomer(customer.ToCustomer());
        }

        public async Task<int> DeleteCustomerAsync(CustomerDto customer, CancellationToken token)
        {
            if(customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            return await _repository.DeleteCustomerAsync(customer.ToCustomer(), token);
        }
    }
}
