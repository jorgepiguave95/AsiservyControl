using Customers.Aplication.Interfaces;
using Contracts.Customer;
using Microsoft.AspNetCore.Mvc;
using Customers.Domain.Erros;

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

        #region Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerResponseDto>>> GetAllCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllCustomers();
                return Ok(customers);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponseDto>> GetCustomerById(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest("El ID no puede estar vacío");

                var customer = await _customerService.GetCustomerById(id);
                if (customer == null)
                    return NotFound($"Cliente con ID {id} no encontrado");

                return Ok(customer);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CustomerResponseDto>> CreateCustomer([FromBody] CreateCustomerDto createCustomerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var customer = await _customerService.AddCustomer(createCustomerDto);
                return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerResponseDto>> UpdateCustomer(Guid id, [FromBody] UpdateCustomerDto updateCustomerDto)
        {
            try
            {
                if (id != updateCustomerDto.Id)
                    return BadRequest("El ID del parámetro no coincide con el ID del cliente");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var customer = await _customerService.UpdateCustomer(updateCustomerDto);
                if (customer == null)
                    return NotFound($"Cliente con ID {id} no encontrado");

                return Ok(customer);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("{id}/activate")]
        public async Task<ActionResult<CustomerResponseDto>> ActivateCustomer(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest("El ID no puede estar vacío");

                var customer = await _customerService.ActivateCustomer(id);
                if (customer == null)
                    return NotFound($"Cliente con ID {id} no encontrado");

                return Ok(customer);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("{id}/deactivate")]
        public async Task<ActionResult<CustomerResponseDto>> DeactivateCustomer(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest("El ID no puede estar vacío");

                var customer = await _customerService.DeactivateCustomer(id);
                if (customer == null)
                    return NotFound($"Cliente con ID {id} no encontrado");

                return Ok(customer);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion
    }
}
