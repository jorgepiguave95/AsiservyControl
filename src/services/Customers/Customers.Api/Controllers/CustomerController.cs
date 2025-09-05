using Customers.Aplication.Interfaces;
using Customers.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(Guid id)
        {
            try
            {
                var customer = await _customerService.GetCustomerById(id);
                if (customer == null)
                {
                    return NotFound($"Cliente con ID {id} no encontrado");
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomer([FromBody] Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _customerService.AddCustomer(customer);
                return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(Guid id, [FromBody] Customer customer)
        {
            try
            {
                if (id != customer.Id)
                {
                    return BadRequest("El ID del parámetro no coincide con el ID del cliente");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingCustomer = await _customerService.GetCustomerById(id);
                if (existingCustomer == null)
                {
                    return NotFound($"Cliente con ID {id} no encontrado");
                }

                await _customerService.UpdateCustomer(customer);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(Guid id)
        {
            try
            {
                var existingCustomer = await _customerService.GetCustomerById(id);
                if (existingCustomer == null)
                {
                    return NotFound($"Cliente con ID {id} no encontrado");
                }

                await _customerService.DeleteCustomer(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
