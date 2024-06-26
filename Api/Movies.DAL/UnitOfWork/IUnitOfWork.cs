using Movies.DAL.Entities;
using Movies.DAL.Repo.IRepo;
using System;

namespace Movies.DAL.UnitOfWork
{
    /// <summary>
    /// Defines the unit of work pattern for the MoviesDbContext.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the repository for the specified entity type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>An instance of <see cref="IBaseRepo{TEntity}"/>.</returns>
        IBaseRepo<TEntity> Repo<TEntity>() where TEntity : BaseEntity;
    }
}