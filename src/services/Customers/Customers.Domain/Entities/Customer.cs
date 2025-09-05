using Customers.Domain.Erros;
using Customers.Domain.ValueObjects;

namespace Customers.Domain.Entities
{
    public class Customer : Person
    {
        public Email Email { get; private set; }
        public PhoneNumber Phone { get; private set; }

        private Customer() : base() { } // EF Core

        public Customer(string firstName, string lastName, Email email, PhoneNumber phone)
            : base(firstName, lastName)
        {
            Email = email ?? throw new DomainException("El email es requerido");
            Phone = phone ?? throw new DomainException("El teléfono es requerido");
        }

        public void UpdateEmail(Email newEmail)
        {
            Email = newEmail ?? throw new DomainException("El email es requerido");
        }

        public void UpdatePhone(PhoneNumber newPhone)
        {
            Phone = newPhone ?? throw new DomainException("El teléfono es requerido");
        }
    }
}
