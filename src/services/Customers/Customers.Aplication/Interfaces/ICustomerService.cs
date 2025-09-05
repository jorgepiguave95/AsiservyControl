using Customers.Domain.Entities;
using Contracts.Customers;

namespace Customers.Aplication.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerResponseDto>> GetAllCustomers();
        Task<CustomerResponseDto?> GetCustomerById(Guid id);
        Task<CustomerResponseDto> AddCustomer(CreateCustomerDto createCustomerDto);
        Task<CustomerResponseDto?> UpdateCustomer(UpdateCustomerDto updateCustomerDto);
        Task<bool> DeleteCustomer(Guid id);
    }
}
