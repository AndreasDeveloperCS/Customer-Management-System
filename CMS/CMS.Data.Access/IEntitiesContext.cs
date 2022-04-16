using CMS.Data.Access.Models;
using System.Data.Entity;

namespace CMS.Data.Access
{
    public interface IEntitiesContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Item> Items { get; set; }
        DbSet<Order> Orders { get; set; }
    }
}