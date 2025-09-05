using Customers.Domain.Erros;
using System.Text.RegularExpressions;

namespace Customers.Domain.ValueObjects
{
    public class PhoneNumber
    {
        private int minLength = 5;
        private int maxLength = 10;

        public string Value { get; private set; }

        private PhoneNumber() { }

        public PhoneNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("El número de teléfono es requerido");

            if (!Regex.IsMatch(value, @"^[0-9]+$"))
                throw new DomainException("El número de teléfono debe contener solo dígitos");

            if (value.Length < minLength)
                throw new DomainException($"El número de teléfono debe tener al menos {minLength} dígitos");

            if (value.Length > maxLength)
                throw new DomainException($"El número de teléfono no debe exceder {maxLength} dígitos");

            Value = value;
        }

        public override string ToString() => Value;

        public override int GetHashCode() => Value.GetHashCode();
    }
}
