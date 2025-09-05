using Customers.Domain.Erros;

namespace Customers.Domain.Entities
{
    public abstract class Person
    {
        public Guid Id { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }

        protected Person()
        {
            Id = Guid.NewGuid();
        }

        protected Person(string firstName, string lastName) : this()
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("El nombre es requerido");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("El apellido es requerido");

            FirstName = firstName;
            LastName = lastName;
        }
    }
}
