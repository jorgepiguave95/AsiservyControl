namespace Contracts.Product
{
    public class ProductControlDetailResponseDto
    {
        public Guid Id { get; set; }
        public Guid ProductControlId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Peso { get; set; }
        public string? TipoControl { get; set; }
        public string FechaFormateada => Fecha.ToString("dd/MM/yyyy HH:mm");
    }
}
