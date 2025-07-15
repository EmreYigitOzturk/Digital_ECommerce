using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Digital_Domain_Layer.Extensions;

namespace Digital_Persistence_Layer.Repositories.Interface
{
    public interface IRepository<T> where T : new()
    {

        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(Guid Id,T entity);
        Task DeleteAsync(Guid Id);
        Task<T> GetByIdAsync(Guid id);
        public Task<bool> IsExist(Expression<Func<T, bool>> filter = null);
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetWithIncludeProperties(params Expression<Func<T, object>>[] includeProperties);

        public Task<PagedResult<T>> GetPagedResult(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int pageNumber = 1, int pageSize = 10, params Expression<Func<T, object>>[] includeProperties);

    }
}
