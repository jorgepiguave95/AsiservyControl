using Contracts.Product;

namespace Products.Aplication.Interfaces
{
    public interface IProductService
    {
        // ProductControl 
        Task<IEnumerable<ProductControlResponseDto>> GetAllProductControls();
        Task<ProductControlResponseDto?> GetProductControlById(Guid id);
        Task<ProductControlResponseDto> AddProductControl(CreateProductControlDto createProductControlDto);
        Task<ProductControlResponseDto?> UpdateProductControl(UpdateProductControlDto updateProductControlDto);
        Task<ProductControlResponseDto?> ActivateProductControl(Guid id);
        Task<ProductControlResponseDto?> DeactivateProductControl(Guid id);

        // ProductControlDetail 
        Task<IEnumerable<ProductControlDetailResponseDto>> GetProductControlDetails(Guid productControlId);
        Task<ProductControlDetailResponseDto?> GetProductControlDetailById(Guid id);
        Task<ProductControlDetailResponseDto> AddProductControlDetail(CreateProductControlDetailDto createDetailDto);
        Task<ProductControlDetailResponseDto?> UpdateProductControlDetail(UpdateProductControlDetailDto updateDetailDto);
    }
}
