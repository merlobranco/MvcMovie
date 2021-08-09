using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MvcMovie.Core.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<bool> Exists(int id);
        Task<T> GetById(int id);
        Task<bool> Insert(T entity);
        Task<bool> Update(T entity);
        Task<bool> DeleteById(int id);
        Task<bool> Delete(T entity);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    }
}
