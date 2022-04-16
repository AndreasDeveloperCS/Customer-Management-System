using CMS.Data.Models.DTOs;

namespace CMS.Services
{
    public interface ICustomerService
    {
        int CreateCustomer(CustomerDto customer);
        Task<int> CreateCustomerAsync(CustomerDto customer, CancellationToken token);
        int DeleteCustomer(CustomerDto customer);
        Task<int> DeleteCustomerAsync(CustomerDto customer, CancellationToken token);
        IEnumerable<CustomerDto> GetAllCustomers();
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync(CancellationToken token);
        CustomerDto GetCustomerById(int customerId);
        Task<CustomerDto> GetCustomerByIdAsync(int customerId, CancellationToken token);
        int UpdateCustomer(CustomerDto customer);
        Task<int> UpdateCustomerAsync(CustomerDto customer, CancellationToken token);
    }
}