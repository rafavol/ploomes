using Ploomes.API.Infra.Repositories;

namespace Ploomes.API.Infra.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; } 

        void Commit();
    }
}