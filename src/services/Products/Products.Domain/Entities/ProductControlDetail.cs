using Products.Domain.Errors;
using Products.Domain.ValueObjects;

namespace Products.Domain.Entities
{
    public class ProductControlDetail
    {
        public Guid Id { get; private set; }
        public Guid ProductControlId { get; private set; }
        public DateTime Fecha { get; private set; }
        public decimal Peso { get; private set; }
        public TipoControl TipoControl { get; private set; } = default!; 

        private ProductControlDetail() { }

        public ProductControlDetail(Guid productControlId, DateTime fecha, decimal peso, TipoControl tipoControl)
        {
            Id = Guid.NewGuid();

            if (productControlId == Guid.Empty)
                throw new DomainException("El id de ProductControl es requerido.");

            ProductControlId = productControlId;

            SetFecha(fecha);
            SetPeso(peso);
            TipoControl = tipoControl ?? throw new DomainException("El tipo de control es requerido.");
        }

        public void SetFecha(DateTime fecha)
        {
            if (fecha == default) throw new DomainException("La fecha es requerida.");
            Fecha = fecha;
        }

        public void SetPeso(decimal peso)
        {
            if (peso < 0) throw new DomainException("El peso no puede ser negativo.");
            Peso = decimal.Round(peso, 3);
        }

        public void SetTipoControl(TipoControl tipoControl)
        {
            TipoControl = tipoControl ?? throw new DomainException("El tipo de control es requerido.");
        }
    }
}
