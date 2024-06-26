using Microsoft.EntityFrameworkCore;
using Movies.DAL.Entities;
using Movies.DAL.Repo.IRepo;
using System.Linq.Expressions;

namespace Movies.DAL.Repo
{
    /// <summary>
    /// Provides a base repository implementation for handling basic CRUD operations on entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class BaseRepo<TEntity> : IBaseRepo<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext _context;

        public BaseRepo(DbContext context)
        {
            _context = context;
        }

        protected DbSet<TEntity> Set => _context.Set<TEntity>();

        public IQueryable<TEntity> GetAll() => Set;

        public IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> filter) => Set.Where(filter);

        public async Task<TEntity> Get(int id) => await Set.FirstOrDefaultAsync(entity => entity.Id == id);

        public async Task<TEntity> Add(TEntity entity)
        {
            entity.Created = DateTime.Now;
            entity.Modified = DateTime.Now;

            var created = Set.Add(entity);
            await SaveChanges();

            return created.Entity;
        }

        public async Task<TEntity> Delete(TEntity entity)
        {
            var removed = Set.Remove(entity);
            await SaveChanges();

            return removed.Entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            entity.Modified = DateTime.Now;

            var updated = Set.Update(entity);
            await SaveChanges();

            return updated.Entity;
        }

        private async Task SaveChanges()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
