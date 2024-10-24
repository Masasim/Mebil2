using Mebil2.Models;
using Mebil2.Services;
using System.Text.Json;

namespace Mebil2
{
    public class CustomerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ICustomerService customerService)
        {
            if (!context.Request.Path.StartsWithSegments("/customers"))
            {
                await _next(context);
                return;
            }

            switch (context.Request.Method)
            {
                case "GET":
                    await HandleGetCustomersAsync(context, customerService);
                    break;

                case "POST":
                    await HandleAddCustomerAsync(context, customerService);
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status405MethodNotAllowed; // Method Not Allowed
                    break;
            }
        }

        private async Task HandleGetCustomersAsync(HttpContext context, ICustomerService customerService)
        {
            var customers = await customerService.GetCustomersAsync();
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(customers);
        }

        private async Task HandleAddCustomerAsync(HttpContext context, ICustomerService customerService)
        {
            try
            {
                var customer = await JsonSerializer.DeserializeAsync<Customer>(context.Request.Body);

                // Validate the customer object
                if (customer == null || string.IsNullOrWhiteSpace(customer.name))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest; // Bad Request
                    await context.Response.WriteAsync("Invalid customer data.");
                    return;
                }

                await customerService.AddCustomerAsync(customer);
                context.Response.StatusCode = StatusCodes.Status201Created; // Created
            }
            catch (JsonException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest; // Bad Request
                await context.Response.WriteAsync("Invalid JSON format.");
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError; // Internal Server Error
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
            }
        }
    }
}
