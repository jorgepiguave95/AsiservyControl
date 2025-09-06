using Products.Aplication.Interfaces;
using Products.Domain.Entities;
using Products.Domain.ValueObjects;
using Contracts.Product;

namespace Products.Aplication.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductControlRepository _productControlRepository;

        public ProductService(IProductControlRepository productControlRepository)
        {
            _productControlRepository = productControlRepository;
        }

        #region ProductControl
        public async Task<IEnumerable<ProductControlResponseDto>> GetAllProductControls()
        {
            var productControls = await _productControlRepository.GetAll();
            return productControls.Select(MapProductControlToResponseDto);
        }

        public async Task<ProductControlResponseDto?> GetProductControlById(Guid id)
        {
            var productControl = await _productControlRepository.GetById(id);
            return productControl != null ? MapProductControlToResponseDto(productControl) : null;
        }

        public async Task<ProductControlResponseDto> AddProductControl(CreateProductControlDto createProductControlDto)
        {
            var productControl = MapToProductControlEntity(createProductControlDto);
            await _productControlRepository.Add(productControl);
            await _productControlRepository.Save();
            return MapProductControlToResponseDto(productControl);
        }

        public async Task<ProductControlResponseDto?> UpdateProductControl(UpdateProductControlDto updateProductControlDto)
        {
            var productControl = await _productControlRepository.GetById(updateProductControlDto.Id);
            if (productControl == null)
                return null;

            productControl.SetProducto(updateProductControlDto.Producto ?? string.Empty);
            productControl.SetNombreCliente(updateProductControlDto.NombreCliente ?? string.Empty);
            productControl.SetMarca(updateProductControlDto.Marca ?? string.Empty);
            productControl.SetPorcentajeMiga(updateProductControlDto.PorcentajeMiga);
            productControl.SetPesos(updateProductControlDto.PesoDrenado, updateProductControlDto.PesoEnvase);

            _productControlRepository.Update(productControl);
            await _productControlRepository.Save();
            return MapProductControlToResponseDto(productControl);
        }
        #endregion

        #region ProductControlDetail
        public async Task<IEnumerable<ProductControlDetailResponseDto>> GetProductControlDetails(Guid productControlId)
        {
            var details = await _productControlRepository.GetDetailsByProductControlId(productControlId);
            return details.Select(MapProductControlDetailToResponseDto);
        }

        public async Task<ProductControlDetailResponseDto?> GetProductControlDetailById(Guid id)
        {
            var detail = await _productControlRepository.GetDetailById(id);
            return detail != null ? MapProductControlDetailToResponseDto(detail) : null;
        }

        public async Task<ProductControlDetailResponseDto> AddProductControlDetail(CreateProductControlDetailDto createDetailDto)
        {
            var productControl = await _productControlRepository.GetById(createDetailDto.ProductControlId);
            if (productControl == null)
                throw new InvalidOperationException($"ProductControl con el ID {createDetailDto.ProductControlId} no encontrado");

            var tipoControl = new TipoControl(createDetailDto.TipoControl ?? string.Empty);
            var detail = productControl.AgregarDetalle(createDetailDto.Fecha, createDetailDto.Peso, tipoControl);

            await _productControlRepository.AddDetail(detail);
            await _productControlRepository.Save();
            return MapProductControlDetailToResponseDto(detail);
        }

        public async Task<ProductControlDetailResponseDto?> UpdateProductControlDetail(UpdateProductControlDetailDto updateDetailDto)
        {
            var detail = await _productControlRepository.GetDetailById(updateDetailDto.Id);
            if (detail == null)
                throw new InvalidOperationException($"ProductControlDetail con el ID {updateDetailDto.Id} no encontrado");

            var productControl = await _productControlRepository.GetById(detail.ProductControlId);
            if (productControl == null)
                throw new InvalidOperationException($"ProductControl con el ID {detail.ProductControlId} no encontrado");

            detail.SetFecha(updateDetailDto.Fecha);
            detail.SetPeso(updateDetailDto.Peso);
            detail.SetTipoControl(new TipoControl(updateDetailDto.TipoControl ?? string.Empty));

            _productControlRepository.Update(productControl);
            await _productControlRepository.Save();
            return MapProductControlDetailToResponseDto(detail);
        }

        #endregion

        #region Mappers
        private ProductControlResponseDto MapProductControlToResponseDto(ProductControl productControl)
        {
            return new ProductControlResponseDto
            {
                Id = productControl.Id,
                Fecha = productControl.Fecha,
                Producto = productControl.Producto,
                NombreCliente = productControl.NombreCliente,
                Marca = productControl.Marca,
                PorcentajeMiga = productControl.PorcentajeMiga,
                PesoDrenado = productControl.PesoDrenado,
                PesoEnvase = productControl.PesoEnvase,
                PesoNeto = productControl.PesoNeto.Value,
                EstaActivo = productControl.EstaActivo
            };
        }

        private ProductControl MapToProductControlEntity(CreateProductControlDto dto)
        {
            return new ProductControl(
                fecha: DateTime.Now,
                producto: dto.Producto ?? string.Empty,
                nombreCliente: dto.NombreCliente ?? string.Empty,
                marca: dto.Marca ?? string.Empty,
                porcentajeMiga: dto.PorcentajeMiga,
                pesoDrenado: dto.PesoDrenado,
                pesoEnvase: dto.PesoEnvase,
                estaActivo: true
            );
        }

        private ProductControlDetailResponseDto MapProductControlDetailToResponseDto(ProductControlDetail detail)
        {
            return new ProductControlDetailResponseDto
            {
                Id = detail.Id,
                ProductControlId = detail.ProductControlId,
                Fecha = detail.Fecha,
                Peso = detail.Peso,
                TipoControl = detail.TipoControl.Value
            };
        }
        #endregion
    }
}
