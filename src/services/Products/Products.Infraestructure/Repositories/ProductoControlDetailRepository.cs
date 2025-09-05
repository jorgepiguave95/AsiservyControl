using Microsoft.EntityFrameworkCore;
using Products.Aplication.Interfaces;
using Products.Domain.Entities;
using Products.Infraestructure.Persistence;

namespace Products.Infraestructure.Repositories
{
    public class ProductoControlDetailRepository : IRepository<ProductControlDetail>
    {
        private readonly ProductsDbContext _context;

        public ProductoControlDetailRepository(ProductsDbContext context)
        {
            _context = context;
        }

        public async Task Add(ProductControlDetail entity)
        => await _context.ProductControlDetails.AddAsync(entity);


        public async Task<IEnumerable<ProductControlDetail>> GetAll()
        => await _context.ProductControlDetails.ToListAsync();

        public async Task<ProductControlDetail> GetById(Guid id)
        => await _context.ProductControlDetails.FindAsync(id);


        public async void Update(ProductControlDetail entity)
        {
            _context.ProductControlDetails.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(ProductControlDetail entity)
        => _context.ProductControlDetails.Remove(entity);

        public async Task Save()
        => await _context.SaveChangesAsync();
    }
}
