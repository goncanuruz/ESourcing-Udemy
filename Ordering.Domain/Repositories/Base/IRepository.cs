using Ordering.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Repositories.Base
{
    public interface IRepository<T>
        where T:Entity
    {
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T,bool>> predicate);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate=null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy =null, string includeString=null, bool disableTracking=true);



    }
}
