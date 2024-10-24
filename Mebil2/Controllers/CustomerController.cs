using Mebil2.Data;
using Mebil2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Mebil2.Controllers
{
    public class CustomerController : Controller
    {
        private readonly MebilContext _context;

        public CustomerController(MebilContext context)
        {
            _context = context;
        }

        // Action to list all customers
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers.ToListAsync();
            return View(customers); // Pass the list of customers to the view
        }

        // Action to edit a specific customer by ID
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound(); // Return a 404 if customer not found
            }
            return View(customer); // Pass the customer to the edit view
        }

        // Action to save changes made to the customer
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromForm] Customer updatedCustomer)
        {
            var form = await HttpContext.Request.ReadFormAsync();

            var name = form["name"].ToString();
            var address = form["address"].ToString();
            var bankDetails = form["bankDetails"].ToString();

            // Find the customer by id
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            // Update the customer with the new form values
            customer.name = name;
            customer.address = address;
            customer.bankDetails = bankDetails;

            // Save changes to the database
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}