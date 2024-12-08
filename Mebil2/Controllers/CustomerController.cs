﻿using Mebil2.Data;
using Mebil2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public async Task<IActionResult> Index(
            string searchString,
            string sortOrder = "id_desc",
            int pageSize = 5,
            int page = 1)
        {
            // Підготовка запиту з усіх Customer
            var customers = _context.Customers.AsQueryable();

            // Пошук
            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c =>
                    c.name.Contains(searchString) ||
                    c.address.Contains(searchString) ||
                    c.bankDetails.Contains(searchString));
            }

            // Сортування
            customers = sortOrder switch
            {
                "id_asc" => customers.OrderBy(c => c.id),
                "id_desc" => customers.OrderByDescending(c => c.id),
                "name_asc" => customers.OrderBy(c => c.name),
                "name_desc" => customers.OrderByDescending(c => c.name),
                _ => customers.OrderByDescending(c => c.id)
            };

            // Пагінація
            var totalCustomers = await customers.CountAsync();
            var pagedCustomers = await customers
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var viewModel = new CustomerIndexViewModel
            {
                Customers = pagedCustomers,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalCustomers / (double)pageSize),
                PageSize = pageSize,
                SearchString = searchString,
                SortOrder = sortOrder
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public async Task<IActionResult> Details(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

    public class CustomerIndexViewModel
    {
        public List<Customer> Customers { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string SortOrder { get; set; }
    }
}