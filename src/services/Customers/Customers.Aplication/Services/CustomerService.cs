using Customers.Aplication.Interfaces;
using Customers.Domain.Entities;

namespace Customers.Aplication.Services
{
    public class CustomerService : ICustomerService
    {
        private IRepository<Customer> _customerRepository;

        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        async Task ICustomerService.AddCustomer(Customer customer)
        {
            return await _customerRepository.Add(customer);
        }

        async Task<IEnumerable<Customer>> ICustomerService.GetAllCustomers()
        {
            return await _customerRepository.GetAll();
        }

        async Task<Customer> ICustomerService.GetCustomerById(Guid id)
        {
            return await _customerRepository.GetById(id);
        }

        async Task ICustomerService.UpdateCustomer(Customer customer)
        {
            await _customerRepository.Update(customer);
        }

        async Task ICustomerService.DeleteCustomer(Customer customer)
        {
            await _customerRepository.Delete(customer);
        }
    }
}
