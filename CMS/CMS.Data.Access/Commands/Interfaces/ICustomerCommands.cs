using CMS.Data.Models.DTOs;

namespace CMS.Data.Access.Commands.Interfaces
{
    public interface ICustomerCommands
    {
        int CreateCustomer(CustomerDto customer);
        int UpdateCustomer(CustomerDto customer);
        int DeleteCustomer(CustomerDto customer);

        Task<int> CreateCustomerAsync(CustomerDto customer, CancellationToken token);
        Task<int> UpdateCustomerAsync(CustomerDto customer, CancellationToken token);
        Task<int> DeleteCustomerAsync(CustomerDto customer, CancellationToken token);
    }
}
