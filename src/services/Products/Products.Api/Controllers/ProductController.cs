using Microsoft.AspNetCore.Mvc;
using Products.Aplication.Interfaces;
using Contracts.Product;
using Products.Domain.Errors;

namespace Products.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        #region ProductControl
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductControlResponseDto>>> GetAllProductControls()
        {
            try
            {
                var productControls = await _productService.GetAllProductControls();
                return Ok(productControls);
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
        public async Task<ActionResult<ProductControlResponseDto>> GetProductControlById(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest("El ID no puede estar vacío");

                var productControl = await _productService.GetProductControlById(id);
                if (productControl == null)
                    return NotFound($"Control de producto con ID {id} no encontrado");

                return Ok(productControl);
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
        public async Task<ActionResult<ProductControlResponseDto>> CreateProductControl([FromBody] CreateProductControlDto createProductControlDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var productControl = await _productService.AddProductControl(createProductControlDto);
                return CreatedAtAction(nameof(GetProductControlById), new { id = productControl.Id }, productControl);
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
        public async Task<ActionResult<ProductControlResponseDto>> UpdateProductControl(Guid id, [FromBody] UpdateProductControlDto updateProductControlDto)
        {
            try
            {
                if (id != updateProductControlDto.Id)
                    return BadRequest("El ID del parámetro no coincide con el ID del control de producto");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var productControl = await _productService.UpdateProductControl(updateProductControlDto);
                if (productControl == null)
                    return NotFound($"Control de producto con ID {id} no encontrado");

                return Ok(productControl);
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

        #region ProductControlDetail
        [HttpGet("{productControlId}/details")]
        public async Task<ActionResult<IEnumerable<ProductControlDetailResponseDto>>> GetProductControlDetails(Guid productControlId)
        {
            try
            {
                if (productControlId == Guid.Empty)
                    return BadRequest("El ID del ProductControl no puede estar vacío");

                var existingProductControl = await _productService.GetProductControlById(productControlId);
                if (existingProductControl == null)
                    return NotFound($"El ProductControl con ID {productControlId} no existe");

                var details = await _productService.GetProductControlDetails(productControlId);
                return Ok(details);
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

        [HttpGet("details/{id}")]
        public async Task<ActionResult<ProductControlDetailResponseDto>> GetProductControlDetailById(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest("El ID no puede estar vacío");

                var detail = await _productService.GetProductControlDetailById(id);
                if (detail == null)
                    return NotFound($"Detalle de control con ID {id} no encontrado");

                return Ok(detail);
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

        [HttpPost("details")]
        public async Task<ActionResult<ProductControlDetailResponseDto>> CreateProductControlDetail([FromBody] CreateProductControlDetailDto createDetailDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existingProductControl = await _productService.GetProductControlById(createDetailDto.ProductControlId);
                if (existingProductControl == null)
                    return BadRequest($"El ProductControl con ID {createDetailDto.ProductControlId} no existe");

                var detail = await _productService.AddProductControlDetail(createDetailDto);
                return CreatedAtAction(nameof(GetProductControlDetailById), new { id = detail.Id }, detail);
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

        [HttpPut("details/{id}")]
        public async Task<ActionResult<ProductControlDetailResponseDto>> UpdateProductControlDetail(Guid id, [FromBody] UpdateProductControlDetailDto updateDetailDto)
        {
            try
            {
                if (id != updateDetailDto.Id)
                    return BadRequest("El ID del parámetro no coincide con el ID del detalle");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var detail = await _productService.UpdateProductControlDetail(updateDetailDto);
                return Ok(detail);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
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
