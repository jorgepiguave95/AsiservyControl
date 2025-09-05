using System.ComponentModel.DataAnnotations;

namespace Contracts.Customers
{
    public class UpdateCustomerDto : CreateCustomerDto
    {
        public Guid Id { get; set; }
    }
}
