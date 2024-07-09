
namespace Briefly.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbset;  
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }

        public virtual async Task AddAsync(T entity)
        {
            var result = await _dbset.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entites)
        {
            var result = _dbset.AddRangeAsync(entites);
            await _context.SaveChangesAsync();
        }


        public async Task Delete(T entity)
        {
            _dbset.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRange(IEnumerable<T> entites)
        {
             _dbset.RemoveRange(entites);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var res =  await _dbset.FindAsync(id);
            return res;
        }

        public IQueryable<T> GetTableNoTracking()
        {
           var res =  _dbset.AsNoTracking();
           return res;
        }

        public IQueryable<T> GetTableTracking()
        {
            var res =  _dbset.AsTracking();
            return res;
        }

        public async Task UpdateAsync(T entity)
        {
             _dbset.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
