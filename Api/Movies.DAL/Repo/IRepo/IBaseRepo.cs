using Movies.DAL.Entities;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;

namespace Movies.DAL.Repo.IRepo
{
    /// <summary>
    /// Defines the repository interface for handling basic CRUD operations on entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IBaseRepo<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Retrieves all entities.
        /// </summary>
        /// <returns>An <see cref="IQueryable{TEntity}"/> of all entities.</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Retrieves entities that match the specified filter.
        /// </summary>
        /// <param name="filter">The filter expression to apply.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/> of matching entities.</returns>
        IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the entity.</returns>
        Task<TEntity> Get(int id);

        /// <summary>
        /// Adds a new entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the added entity.</returns>
        Task<TEntity> Add(TEntity entity);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the updated entity.</returns>
        Task<TEntity> Update(TEntity entity);

        /// <summary>
        /// Deletes an existing entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the deleted entity.</returns>
        Task<TEntity> Delete(TEntity entity);
    }
}
