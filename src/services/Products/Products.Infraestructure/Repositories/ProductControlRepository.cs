using Products.Aplication.Interfaces;
using Products.Domain.Entities;
using Products.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Products.Infraestructure.Repositories
{
    public class ProductControlRepository : IProductControlRepository
    {
        private readonly ProductsDbContext _context;

        public ProductControlRepository(ProductsDbContext context)
        {
            _context = context;
        }

        // Métodos de IRepository<ProductControl>
        public async Task Add(ProductControl entity)
        => await _context.ProductControls.AddAsync(entity);

        public async Task<IEnumerable<ProductControl>> GetAll()
        => await _context.ProductControls.ToListAsync();

        public async Task<ProductControl?> GetById(Guid id)
        => await _context.ProductControls
            .Include(pc => pc.Detalles)
            .FirstOrDefaultAsync(pc => pc.Id == id);

        public void Update(ProductControl entity)
        {
            var existingEntry = _context.Entry(entity);
            if (existingEntry.State == EntityState.Detached)
            {
                _context.ProductControls.Attach(entity);
            }
            existingEntry.State = EntityState.Modified;
        }

        public void Delete(ProductControl entity)
        => _context.ProductControls.Remove(entity);

        public async Task Save()
        => await _context.SaveChangesAsync();

        // Métodos de IProductControlRepository
        public async Task<IEnumerable<ProductControlDetail>> GetDetailsByProductControlId(Guid productControlId)
        {
            return await _context.ProductControlDetails
                .Where(d => d.ProductControlId == productControlId)
                .OrderBy(d => d.Fecha)
                .ToListAsync();
        }

        public async Task<ProductControlDetail?> GetDetailById(Guid detailId)
        => await _context.ProductControlDetails.FirstOrDefaultAsync(d => d.Id == detailId);

        public async Task AddDetail(ProductControlDetail detail)
        => await _context.ProductControlDetails.AddAsync(detail);
    }
}
