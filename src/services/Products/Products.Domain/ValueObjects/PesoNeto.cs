using Products.Domain.Errors;

namespace Products.Domain.ValueObjects
{
    public sealed class PesoNeto
    {
        public decimal Value { get; }

        private PesoNeto() { }

        private PesoNeto(decimal value)
        {
            if (value < 0) throw new DomainException("El peso neto no puede ser negativo.");
            Value = decimal.Round(value, 2);
        }

        public static PesoNeto From(decimal pesoDrenado, decimal pesoEnvase)
        {
            if (pesoDrenado < 0) throw new DomainException("El peso drenado no puede ser negativo.");
            if (pesoEnvase < 0) throw new DomainException("El peso del envase no puede ser negativo.");
            return new PesoNeto(pesoDrenado + pesoEnvase);
        }

        public override string ToString() => Value.ToString("0.##");
    }
}
