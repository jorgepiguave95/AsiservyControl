using Customers.Aplication.Interfaces;
using Customers.Domain.Entities;
using Customers.Domain.ValueObjects;
using Contracts.Customers;

namespace Customers.Aplication.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;

        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<CustomerResponseDto>> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAll();
            return customers.Select(MapToResponseDto);
        }

        public async Task<CustomerResponseDto?> GetCustomerById(Guid id)
        {
            var customer = await _customerRepository.GetById(id);
            return customer != null ? MapToResponseDto(customer) : null;
        }

        public async Task<CustomerResponseDto> AddCustomer(CreateCustomerDto createCustomerDto)
        {
            var customer = MapToEntity(createCustomerDto);
            await _customerRepository.Add(customer);
            await _customerRepository.Save();
            return MapToResponseDto(customer);
        }

        public async Task<CustomerResponseDto?> UpdateCustomer(UpdateCustomerDto updateCustomerDto)
        {
            var customer = await _customerRepository.GetById(updateCustomerDto.Id);
            if (customer == null)
                return null;

            customer.UpdateEmail(new Email(updateCustomerDto.Email ?? string.Empty));
            customer.UpdatePhone(new PhoneNumber(updateCustomerDto.Phone ?? string.Empty));

            _customerRepository.Update(customer);
            await _customerRepository.Save();
            return MapToResponseDto(customer);
        }

        public async Task<bool> DeleteCustomer(Guid id)
        {
            var customer = await _customerRepository.GetById(id);
            if (customer == null)
                return false;

            _customerRepository.Delete(customer);
            await _customerRepository.Save();
            return true;
        }

        private CustomerResponseDto MapToResponseDto(Customer customer)
        {
            return new CustomerResponseDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email.Value,
                Phone = customer.Phone.Value
            };
        }

        private Customer MapToEntity(CreateCustomerDto dto)
        {
            var email = new Email(dto.Email ?? string.Empty);
            var phone = new PhoneNumber(dto.Phone ?? string.Empty);
            return new Customer(dto.FirstName ?? string.Empty, dto.LastName ?? string.Empty, email, phone);
        }
    }
}
