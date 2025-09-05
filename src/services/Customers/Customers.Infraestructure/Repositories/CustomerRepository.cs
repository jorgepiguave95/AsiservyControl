using Customers.Aplication.Interfaces;
using Customers.Domain.Entities;
using Customers.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Customers.Infraestructure.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private CustomersDbContext _context;

        public CustomerRepository(CustomersDbContext context)
        {
            _context = context;
        }

        public async Task Add(Customer entity)
        => await _context.Customers.AddAsync(entity);


        public async Task<IEnumerable<Customer>> GetAll()
        => await _context.Customers.ToListAsync();

        public async Task<Customer> GetById(Guid id)
        => await _context.Customers.FindAsync(id);


        public async void Update(Customer entity)
        {
            _context.Customers.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Customer entity)
        => _context.Customers.Remove(entity);

        public async Task Save()
        => await _context.SaveChangesAsync();

    }
}
