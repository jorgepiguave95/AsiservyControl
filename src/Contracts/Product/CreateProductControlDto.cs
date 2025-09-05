using System.ComponentModel.DataAnnotations;

namespace Contracts.Product
{
    public class CreateProductControlDto
    {
        public string? Producto { get; set; }
        public string? NombreCliente { get; set; }
        public string? Marca { get; set; }
        public decimal PorcentajeMiga { get; set; }
        public decimal PesoDrenado { get; set; }
        public decimal PesoEnvase { get; set; }
    }
}
