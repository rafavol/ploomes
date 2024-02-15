using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Ploomes.API.DBContext;
using Ploomes.API.Exceptions;
using Ploomes.API.Models;

namespace Ploomes.API.Infra.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        protected readonly ApplicationContext _context;

        public BaseRepository()
        {

        }

        public BaseRepository(ApplicationContext context)
        {
            _context = context;
        }

        public virtual async Task<T> Create(T obj)
        {
            try
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException.Message.Contains("Duplicate entry"))
                {
                    throw new DuplicatedEntryException($"Já existe um registro com o mesmo valor.");
                }
                throw new Exception($"{e.Message}\nStackTrace:{e.StackTrace}");
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}\nStackTrace:{e.StackTrace}");
            }

        }

        public virtual async Task<T> Update(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return obj;
        }

        public virtual async Task<T> Update(long id, T obj)
        {

            try
            {
                obj.Id = id;
                _context.Attach(obj);
                _context.Entry(obj).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (InvalidOperationException e)
            {
                if (e.Message.Contains(@"cannot be tracked because another instance with the same key value for {'Id'} is already being tracked"))
                {
                    _context.Entry(obj).CurrentValues.SetValues(obj);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntryExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            catch (DbUpdateException e)
            {
                if (e.Message.Contains("unique"))
                {
                    throw new DuplicatedEntryException($"Já existe um registro com o mesmo valor.");
                }
                throw new Exception($"{e.Message}\nStackTrace:{e.StackTrace}");
            }

            return obj;
        }


        public virtual async Task<bool> Delete(long id)
        {
            var obj = await Get(id);

            try
            {
                if (obj != null)
                {
                    _context.Remove(obj);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (DbUpdateException)
            {
                throw new UnableToDeleteException($"Não é possível excluir o registro.");
            }
            catch (SqlException)
            {
                throw new UnableToDeleteException($"Não é possível excluir o registro.");
            }
        }

        public virtual async Task<T> Get(long? id)
        {
            return await _context.Set<T>().FindAsync(id);

        }

        public virtual async Task<IEnumerable<T>> Get()
        {
            return _context.Set<T>()
                                 .AsNoTracking();
        }

        private bool EntryExists(long id)
        {
            return _context.Set<T>().Any(e => e.Id == id);
        }
    }
}
