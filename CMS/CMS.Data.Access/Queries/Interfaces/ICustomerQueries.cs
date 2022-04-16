using CMS.Data.Models.DTOs;

namespace CMS.Data.Access.Queries.Interfaces
{
    public interface ICustomerQueries
    {
        IEnumerable<CustomerDto> GetAllCustomers();
        CustomerDto GetCustomerById(int customerId);

        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync(CancellationToken token);
        Task<CustomerDto> GetCustomerByIdAsync(int customerId, CancellationToken token);

    }
}
