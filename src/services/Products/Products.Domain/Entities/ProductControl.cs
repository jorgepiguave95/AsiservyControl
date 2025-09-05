using Products.Domain.Errors;
using Products.Domain.ValueObjects;

namespace Products.Domain.Entities
{

    public class ProductControl
    {
        public Guid Id { get; private set; }
        public DateTime Fecha { get; private set; }
        public string Producto { get; private set; } = default!;
        public string NombreCliente { get; private set; } = default!;
        public string Marca { get; private set; } = default!;
        public decimal PorcentajeMiga { get; private set; }
        public decimal PesoDrenado { get; private set; }
        public decimal PesoEnvase { get; private set; }
        public bool EstaActivo { get; private set; }
        public PesoNeto PesoNeto { get; private set; } = default!;

        // Relación 1..N con detalles
        private readonly List<ProductControlDetail> _detalles = new();
        public IReadOnlyCollection<ProductControlDetail> Detalles => _detalles.AsReadOnly();

        private ProductControl() { }

        public ProductControl(
            DateTime fecha,
            string producto,
            string nombreCliente,
            string marca,
            decimal porcentajeMiga,
            decimal pesoDrenado,
            decimal pesoEnvase,
            bool estaActivo = true)
        {
            Id = Guid.NewGuid();
            SetFecha(fecha);
            SetProducto(producto);
            SetNombreCliente(nombreCliente);
            SetMarca(marca);
            SetPorcentajeMiga(porcentajeMiga);
            SetPesos(pesoDrenado, pesoEnvase);
            EstaActivo = estaActivo;
        }

        public void Activar() => EstaActivo = true;
        public void Desactivar() => EstaActivo = false;

        public void SetFecha(DateTime fecha)
        {
            if (fecha == default) throw new DomainException("La fecha es requerida.");
            Fecha = fecha;
        }

        public void SetProducto(string producto)
        {
            if (string.IsNullOrWhiteSpace(producto)) throw new DomainException("El producto es requerido.");
            Producto = producto.Trim();
        }

        public void SetNombreCliente(string nombreCliente)
        {
            if (string.IsNullOrWhiteSpace(nombreCliente)) throw new DomainException("El nombre del cliente es requerido.");
            NombreCliente = nombreCliente.Trim();
        }

        public void SetMarca(string marca)
        {
            if (string.IsNullOrWhiteSpace(marca)) throw new DomainException("La marca es requerida.");
            Marca = marca.Trim();
        }

        public void SetPorcentajeMiga(decimal porcentaje)
        {
            if (porcentaje < 0 || porcentaje > 100) throw new DomainException("El porcentaje de miga debe estar entre 0 y 100.");
            PorcentajeMiga = decimal.Round(porcentaje, 2);
        }

        public void SetPesos(decimal pesoDrenado, decimal pesoEnvase)
        {
            if (pesoDrenado < 0) throw new DomainException("El peso drenado no puede ser negativo.");
            if (pesoEnvase < 0) throw new DomainException("El peso del envase no puede ser negativo.");

            PesoDrenado = decimal.Round(pesoDrenado, 3);
            PesoEnvase = decimal.Round(pesoEnvase, 3);

            RecalcularPesoNeto();
        }

        public void RecalcularPesoNeto()
        {
            PesoNeto = PesoNeto.From(PesoDrenado, PesoEnvase);
        }

        public ProductControlDetail AgregarDetalle(DateTime fecha, decimal peso, TipoControl tipoControl)
        {
            var detalle = new ProductControlDetail(Id, fecha, peso, tipoControl);
            _detalles.Add(detalle);
            return detalle;
        }
    }

}
