namespace Customers.Aplication.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> GetById(Guid id);

        Task<IEnumerable<TEntity>> GetAll();

        Task Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task Save();
    }
}
