using Products.Domain.Errors;

namespace Products.Domain.ValueObjects
{
    public sealed class TipoControl
    {
        public string Value { get; }

        private static readonly string[] _validos =
        {
            "PESO NETO",
            "PESO FILL",
        };

        private TipoControl() { }

        public TipoControl(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("El tipo de control es requerido.");

            if (!_validos.Contains(value))
                throw new DomainException($"El tipo de control '{value}' no es válido. Valores permitidos: {string.Join(", ", _validos)}");

            Value = value;
        }

        public override string ToString() => Value;
    }
}
