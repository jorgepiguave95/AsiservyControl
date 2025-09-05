using Products.Aplication.Interfaces;
using Products.Domain.Entities;
using Products.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Products.Infraestructure.Repositories
{
    public class ProductControlRepository : IRepository<ProductControl>
    {
        private readonly ProductsDbContext _context;

        public ProductControlRepository(ProductsDbContext context)
        {
            _context = context;
        }

        public async Task Add(ProductControl entity)
        => await _context.ProductControls.AddAsync(entity);


        public async Task<IEnumerable<ProductControl>> GetAll()
        => await _context.ProductControls.ToListAsync();

        public async Task<ProductControl> GetById(Guid id)
        => await _context.ProductControls.FindAsync(id);


        public async void Update(ProductControl entity)
        {
            _context.ProductControls.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(ProductControl entity)
        => _context.ProductControls.Remove(entity);

        public async Task Save()
        => await _context.SaveChangesAsync();
    }
}
