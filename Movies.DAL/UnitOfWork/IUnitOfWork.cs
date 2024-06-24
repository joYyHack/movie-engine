using Movies.DAL.Entities;
using Movies.DAL.Repo.IRepo;

namespace Movies.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IBaseRepo<TEntity> Repo<TEntity>() where TEntity : BaseEntity;
    }
}
