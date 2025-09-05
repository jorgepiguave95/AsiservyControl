using System.ComponentModel.DataAnnotations;

namespace Contracts.Customer
{
    public class UpdateCustomerDto : CreateCustomerDto
    {
        public Guid Id { get; set; }
    }
}
