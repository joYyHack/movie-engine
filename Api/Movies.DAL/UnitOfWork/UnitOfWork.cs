using Movies.DAL.Entities;
using Movies.DAL.Repo;
using Movies.DAL.Repo.IRepo;

namespace Movies.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MoviesDbContext _context;
        private readonly Dictionary<string, object?> _repos = new();

        public IBaseRepo<TEntity> Repo<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;

            if (!_repos.ContainsKey(type))
            {
                var repositoryType = typeof(BaseRepo<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

                _repos.Add(type, repositoryInstance);
            }

            return (IBaseRepo<TEntity>)_repos[type];
        }

        private bool disposed = false;

        public UnitOfWork(MoviesDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
