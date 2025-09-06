using Microsoft.AspNetCore.Mvc;
using Contracts.Customer;
using System.Text.Json;

namespace ClientGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public CustomersController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CustomersService");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/customer");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var customers = JsonSerializer.Deserialize<IEnumerable<CustomerResponseDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return Ok(customers);
                }

                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex + "Error al obtener clientes");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/customer/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var customer = JsonSerializer.Deserialize<CustomerResponseDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return Ok(customer);
                }

                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex + "Error al obtener cliente por ID");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto customerData)
        {
            try
            {
                var json = JsonSerializer.Serialize(customerData);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/customer", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var createdCustomer = JsonSerializer.Deserialize<CustomerResponseDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return StatusCode((int)response.StatusCode, createdCustomer);
                }

                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex + "Error al crear cliente");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] UpdateCustomerDto customerData)
        {
            try
            {
                if (customerData.Id != Guid.Empty && customerData.Id != id)
                {
                    return BadRequest("El ID del parámetro no coincide con el ID del cliente");
                }

                customerData.Id = id;

                var json = JsonSerializer.Serialize(customerData);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/customer/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var updatedCustomer = JsonSerializer.Deserialize<CustomerResponseDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return Ok(updatedCustomer);
                }

                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex + "Error al actualizar cliente");
            }
        }

        [HttpPatch("{id}/activate")]
        public async Task<IActionResult> ActivateCustomer(Guid id)
        {
            try
            {
                var response = await _httpClient.PatchAsync($"api/customer/{id}/activate", null);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return Ok(responseContent);
                }

                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex + "Error al activar cliente");
            }
        }

        [HttpPatch("{id}/deactivate")]
        public async Task<IActionResult> DeactivateCustomer(Guid id)
        {
            try
            {
                var response = await _httpClient.PatchAsync($"api/customer/{id}/deactivate", null);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return Ok(responseContent);
                }

                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex + "Error al desactivar cliente");
            }
        }
    }
}
