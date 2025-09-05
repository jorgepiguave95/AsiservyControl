using System.ComponentModel.DataAnnotations;

namespace Contracts.Product
{
    public class UpdateProductControlDetailDto : CreateProductControlDetailDto
    {
        public Guid Id { get; set; }
    }
}
