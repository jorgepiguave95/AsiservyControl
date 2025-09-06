using Microsoft.AspNetCore.Mvc;

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
                    return Ok(content);
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
                    return Ok(content);
                }

                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex + "Error al obtener control de producto");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductControl([FromBody] object productData)
        {
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(productData);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/product", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, responseContent);
                }

                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex + "Error al crear control de producto");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductControl(Guid id, [FromBody] object productData)
        {
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(productData);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/product/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return Ok(responseContent);
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
                    return Ok(content);
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
                    return Ok(content);
                }

                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex + "Error al obtener detalle de control " + id);
            }
        }

        [HttpPost("details")]
        public async Task<IActionResult> CreateProductControlDetail([FromBody] object detailData)
        {
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(detailData);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/product/details", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, responseContent);
                }

                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex + "Error al crear detalle de control de producto");
            }
        }

        [HttpPut("details/{id}")]
        public async Task<IActionResult> UpdateProductControlDetail(Guid id, [FromBody] object detailData)
        {
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(detailData);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/product/details/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return Ok(responseContent);
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
