using Customers.Domain.Entities;
using Contracts.Customer;

namespace Customers.Aplication.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerResponseDto>> GetAllCustomers();
        Task<CustomerResponseDto?> GetCustomerById(Guid id);
        Task<CustomerResponseDto> AddCustomer(CreateCustomerDto createCustomerDto);
        Task<CustomerResponseDto?> UpdateCustomer(UpdateCustomerDto updateCustomerDto);
        Task<CustomerResponseDto?> ActivateCustomer(Guid id);
        Task<CustomerResponseDto?> DeactivateCustomer(Guid id);
    }
}
