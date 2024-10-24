using Mebil2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mebil2.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task AddCustomerAsync(Customer customer);
    }
}
