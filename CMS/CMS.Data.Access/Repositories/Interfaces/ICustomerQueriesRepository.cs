using CMS.Data.Access.Interfaces;
using CMS.Data.Access.Models;

namespace CMS.Data.Access.Repositories.Interfaces
{
    public interface ICustomerQueriesRepository : IRepository
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);

        Task<IEnumerable<Customer>> GetAllCustomersAsync(CancellationToken token);
        Task<Customer> GetCustomerByIdAsync(int id, CancellationToken token);
    }
}
