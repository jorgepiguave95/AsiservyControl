using System.ComponentModel.DataAnnotations;

namespace Contracts.Product
{
    public class CreateProductControlDetailDto
    {
        public Guid ProductControlId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Peso { get; set; }
        public string? TipoControl { get; set; }
    }
}
