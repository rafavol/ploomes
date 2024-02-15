using Ploomes.API.Models;

namespace Ploomes.API.Infra.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<bool> VerificaUserNameDisponivel(string username);
        public Task<User> GetByUserName(string username);
        public Task<User> GetInfoByUserId(long Id);
        public Task<IEnumerable<User>> GetUsers();

    }
}
