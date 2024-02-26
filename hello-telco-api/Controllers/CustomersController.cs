using hello_telco_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hello_telco_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        // Get all customer list
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.Include(c => c.PhoneNumbers).ToListAsync();
        }

        // Get customer by Customer.Id
        [HttpGet("{customerId}")]
        public async Task<ActionResult<Customer>> GetCustomer(int customerId)
        {
            var customer = await _context.Customers
                .Include(p => p.PhoneNumbers)
                .FirstOrDefaultAsync(c => c.Id == customerId);
                //.FindAsync(customerId);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }
    }
}