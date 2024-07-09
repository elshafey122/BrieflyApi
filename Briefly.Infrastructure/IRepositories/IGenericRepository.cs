namespace Briefly.Infrastructure.IRepositoties
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetTableNoTracking();
        IQueryable<T> GetTableTracking();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task Delete(T entity);
        Task DeleteRange(IEnumerable<T> entites);
        Task AddRangeAsync(IEnumerable<T> entity);
    }
}
