using Mebil2.Data;
using Mebil2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mebil2.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly MebilContext _context;  // Removed asterisk, using underscore

        public CustomerService(MebilContext context)
        {
            _context = context;  // Removed asterisk, using underscore
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _context.Customers.ToListAsync();  // Removed asterisk, using underscore
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);  // Removed asterisk, using underscore
            await _context.SaveChangesAsync();  // Already correct
        }
    }
}