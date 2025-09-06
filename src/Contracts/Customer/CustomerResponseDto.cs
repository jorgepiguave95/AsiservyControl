namespace Contracts.Customer
{
    public class CustomerResponseDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool EstaActivo { get; set; }
    }
}
