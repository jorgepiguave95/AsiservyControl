using Microsoft.AspNetCore.Mvc;
using Contracts.Product;
using System.Text.Json;

namespace ClientGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ProductsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ProductsService");
        }

        #region ProductControl
        [HttpGet]
        public async Task<IActionResult> GetAllProductControls()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/product");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var productControls = JsonSerializer.Deserialize<IEnumerable<ProductControlResponseDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return Ok(productControls);
                }

                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex + "Error al obtener controles de producto");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductControlById(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/product/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var productControl = JsonSerializer.Deserialize<ProductControlResponseDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return Ok(productControl);
                }

                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex + "Error al obtener control de producto");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductControl([FromBody] CreateProductControlDto productData)
        {
            try
            {
                var json = JsonSerializer.Serialize(productData);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/product", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var createdProductControl = JsonSerializer.Deserialize<ProductControlResponseDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return StatusCode((int)response.StatusCode, createdProductControl);
                }

                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex + "Error al crear control de producto");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductControl(Guid id, [FromBody] UpdateProductControlDto productData)
        {
            try
            {
                productData.Id = id;

                var json = JsonSerializer.Serialize(productData);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/product/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var updatedProductControl = JsonSerializer.Deserialize<ProductControlResponseDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return Ok(updatedProductControl);
                }

                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex + "Error al actualizar control de producto");
            }
        }
        #endregion

        #region ProductControlDetail Endpoints
        [HttpGet("{productControlId}/details")]
        public async Task<IActionResult> GetProductControlDetails(Guid productControlId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/product/{productControlId}/details");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var productControlDetails = JsonSerializer.Deserialize<IEnumerable<ProductControlDetailResponseDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return Ok(productControlDetails);
                }

                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex + "Error al obtener detalles del control de producto");
            }
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetProductControlDetailById(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/product/details/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var productControlDetail = JsonSerializer.Deserialize<ProductControlDetailResponseDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return Ok(productControlDetail);
                }

                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex + "Error al obtener detalle de control " + id);
            }
        }

        [HttpPost("details")]
        public async Task<IActionResult> CreateProductControlDetail([FromBody] CreateProductControlDetailDto detailData)
        {
            try
            {
                var json = JsonSerializer.Serialize(detailData);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/product/details", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var createdDetail = JsonSerializer.Deserialize<ProductControlDetailResponseDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return StatusCode((int)response.StatusCode, createdDetail);
                }

                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex + "Error al crear detalle de control de producto");
            }
        }

        [HttpPut("details/{id}")]
        public async Task<IActionResult> UpdateProductControlDetail(Guid id, [FromBody] UpdateProductControlDetailDto detailData)
        {
            try
            {
                detailData.Id = id;

                var json = JsonSerializer.Serialize(detailData);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/product/details/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var updatedDetail = JsonSerializer.Deserialize<ProductControlDetailResponseDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return Ok(updatedDetail);
                }

                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex + "Error al actualizar detalle de control " + id);
            }
        }

        #endregion
    }
}
