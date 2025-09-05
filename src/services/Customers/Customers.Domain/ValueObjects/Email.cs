using Customers.Domain.Erros;

namespace Customers.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; private set; }

        private Email() { }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("El email es requerido");

            if (!value.Contains("@"))
                throw new DomainException("El email es inválido");

            Value = value;
        }

        public override string ToString() => Value;
    }
}
