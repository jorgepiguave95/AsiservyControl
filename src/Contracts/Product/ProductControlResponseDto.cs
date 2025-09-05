namespace Contracts.Product
{
    public class ProductControlResponseDto
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public string? Producto { get; set; }
        public string? NombreCliente { get; set; }
        public string? Marca { get; set; }
        public decimal PorcentajeMiga { get; set; }
        public decimal PesoDrenado { get; set; }
        public decimal PesoEnvase { get; set; }
        public decimal PesoNeto { get; set; }
        public bool EstaActivo { get; set; }
    }
}
