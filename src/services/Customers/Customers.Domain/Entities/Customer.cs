using Customers.Domain.Erros;
using Customers.Domain.ValueObjects;

namespace Customers.Domain.Entities
{
    public class Customer : Person
    {
        public Email Email { get; private set; }
        public PhoneNumber Phone { get; private set; }
        public bool EstaActivo { get; private set; }

        private Customer() : base()
        {
            Email = null!;
            Phone = null!;
        }

        public Customer(string firstName, string lastName, Email email, PhoneNumber phone, bool estaActivo = true)
            : base(firstName, lastName)
        {
            Email = email ?? throw new DomainException("El email es requerido");
            Phone = phone ?? throw new DomainException("El teléfono es requerido");
            EstaActivo = estaActivo;
        }

        public void UpdateEmail(Email newEmail)
        {
            Email = newEmail ?? throw new DomainException("El email es requerido");
        }

        public void UpdatePhone(PhoneNumber newPhone)
        {
            Phone = newPhone ?? throw new DomainException("El teléfono es requerido");
        }

        public void UpdateFirstName(string newFirstName)
        {
            if (string.IsNullOrWhiteSpace(newFirstName))
                throw new DomainException("El nombre es requerido");

            FirstName = newFirstName;
        }

        public void UpdateLastName(string newLastName)
        {
            if (string.IsNullOrWhiteSpace(newLastName))
                throw new DomainException("El apellido es requerido");

            LastName = newLastName;
        }

        public void Activate()
        {
            EstaActivo = true;
        }

        public void Deactivate()
        {
            EstaActivo = false;
        }
    }
}
