using Products.Domain.Entities;

namespace Products.Aplication.Interfaces
{
    public interface IProductControlRepository : IRepository<ProductControl>
    {
        Task<IEnumerable<ProductControlDetail>> GetDetailsByProductControlId(Guid productControlId);
        Task<ProductControlDetail?> GetDetailById(Guid detailId);
    }
}
