using CMS.Data.Access.Interfaces;
using CMS.Data.Access.Models;

namespace CMS.Data.Access.Repositories.Interfaces
{
    public interface ICustomerCommandsRepository : IRepository
    {
        int CreateCustomer(Customer customer);
        int UpdateCustomer(Customer customer);
        int DeleteCustomer(Customer customer);

        Task<int> CreateCustomerAsync(Customer customer, CancellationToken token);
        Task<int> UpdateCustomerAsync(Customer customer, CancellationToken token);
        Task<int> DeleteCustomerAsync(Customer customer, CancellationToken token);
    }
}
