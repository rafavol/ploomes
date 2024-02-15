using Microsoft.EntityFrameworkCore;
using Ploomes.API.DBContext;
using Ploomes.API.DTO;
using Ploomes.API.Models;

namespace Ploomes.API.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }
        public UserRepository()
        {

        }

        public async Task<bool> VerificaUserNameDisponivel(string username)
        {
            var userDB = await _context.Users.AnyAsync(e => e.UserName.Equals(username));
            return userDB;
        }

        public async Task<User> GetByUserName(string username)
        {
            var userDB = await _context.Users.SingleOrDefaultAsync(e => e.UserName.Equals(username));
            return userDB;
        }

        public async Task<User> GetInfoByUserId(long id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(e => e.Id == id);
            if (user is not null)
            {
                return user;
            }
            return null;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var userDB = _context.Set<User>()
                                 .AsNoTracking();
            return userDB;
        }
    }
}
