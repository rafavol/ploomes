using Ploomes.API.Models;

namespace Ploomes.API.Infra.Repositories
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        Task<T> Create(T obj);
        Task<T> Update(T obj);
        Task<T> Update(long id, T obj);
        Task<bool> Delete(long id);
        Task<T> Get(long? id);
        Task<IEnumerable<T>> Get();
    }
}
