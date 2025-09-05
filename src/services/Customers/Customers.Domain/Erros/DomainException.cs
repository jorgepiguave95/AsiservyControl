namespace Customers.Domain.Erros
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }
}
