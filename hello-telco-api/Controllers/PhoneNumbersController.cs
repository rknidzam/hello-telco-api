using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using hello_telco_api.Models;
using hello_telco_api.DTOs;

namespace hello_telco_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneNumbersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PhoneNumbersController(AppDbContext context)
        {
            _context = context;
        }

        // Get API Status
        [HttpGet("Check")]
        public ActionResult<string> HealthCheck()
        {
            var healthResult = new { status = "OK" };

            return Ok(healthResult);
        }

        // Get All Phone Numbers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetPhoneNumbers()
        {
            //var res = await _context.PhoneNumbers.Include(cust => cust.Customer).ToListAsync();

            var res = await _context.PhoneNumbers
                .Include(cust => cust.Customer)
                .Select(phone => new { customerId = phone.Customer.Id, name = phone.Customer.Name, phone = phone.Number, status = phone.Status, phoneNumberId = phone.Id })
                .ToListAsync();

            return res;
        }

        // Get All Phone Numbers By Customer.Id
        [HttpGet("PhoneNumberByCustomer/{customerId}")]
        public async Task<ActionResult<IEnumerable<PhoneNumber>>> GetPhoneNumbersByCustomerId(int customerId)
        {
            return await _context.PhoneNumbers.Where(p => p.CustomerId == customerId).ToListAsync();
        }

        // Get single Phone Number by PhoneNumber.Id
        [HttpGet("{phoneNumberId}")]
        public async Task<ActionResult<PhoneNumber>> GetPhoneNumber(int phoneNumberId)
        {
            var phoneNumber = await _context.PhoneNumbers.Include(p => p.Customer).FirstOrDefaultAsync(p => p.Id == phoneNumberId);

            if (phoneNumber == null)
            {
                return NotFound();
            }

            return phoneNumber;
        }

        // Add New Phone Number with Customer.Id
        [HttpPost]
        public async Task<ActionResult<PhoneNumber>> PostPhoneNumber(PhoneNumberCreateDTO phoneNumberDTO)
        {
            var phoneNumber = new PhoneNumber
            {
                Number = phoneNumberDTO.Number,
                Status = phoneNumberDTO.Status,
                CustomerId = phoneNumberDTO.CustomerId
            };

            _context.PhoneNumbers.Add(phoneNumber);

            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetPhoneNumber", new { phoneNumberId = phoneNumber.Id }, phoneNumber);

            var response = new
            {
                success = true,
                message = "Phone number saved successfully."
            };

            return Ok(response);
        }

        // Activate Phone Number with PhoneNumber.Id        
        [HttpPut("{phoneNumberId}")]
        public async Task<IActionResult> PutPhoneNumber(int phoneNumberId, bool status)
        {
            // Find numbers
            var phoneNumberFromDb = await _context.PhoneNumbers.FirstOrDefaultAsync(n => n.Id == phoneNumberId);

            // Check if exist
            if (phoneNumberFromDb == null)
            {
                return NotFound();
            }
            else
            {
                phoneNumberFromDb.Status = status;

                await _context.SaveChangesAsync();

                return Ok();
            }
        }
    }
}