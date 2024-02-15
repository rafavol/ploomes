using Ploomes.API.DBContext;
using Ploomes.API.Infra.Repositories;

namespace Ploomes.API.Infra.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly ApplicationContext applicationContext;
        private bool disposed;

        private UserRepository userRepository;

        public virtual IUserRepository UserRepository =>
            userRepository ??= new UserRepository(applicationContext);

        public UnitOfWork(ApplicationContext _applicationContext)
        {
            applicationContext = _applicationContext;
        }

        public virtual async void Commit()
        {
            await applicationContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                applicationContext.Dispose();
            }
            disposed = true;
        }
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
