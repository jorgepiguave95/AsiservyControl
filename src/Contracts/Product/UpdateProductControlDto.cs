using System.ComponentModel.DataAnnotations;

namespace Contracts.Product
{
    public class UpdateProductControlDto : CreateProductControlDto
    {
        public Guid Id { get; set; }
    }
}
