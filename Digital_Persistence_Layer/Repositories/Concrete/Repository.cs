using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Digital_Domain_Layer.Extensions;
using Digital_Persistence_Layer.AppDbContext;
using Digital_Persistence_Layer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Digital_Persistence_Layer.Repositories.Concrete
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly ApplicationDbContext context;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
        }

        private DbSet<T> Table { get => context.Set<T>(); }


        public async Task<T> CreateAsync(T entity)
        {
            await Table.AddAsync(entity);
            if (await context.SaveChangesAsync() > 0)
            {
                return entity;
            }
            return null;
        }

        public async Task DeleteAsync(Guid Id)
        {
            var entity = await Table.FindAsync(Id);
            if (entity is not null)
            {
                Table.Remove(entity);
                await context.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IEnumerable<T> entities = await Table.ToListAsync();
            if (entities is not null)
            {
                return entities;
            }
            return null;

        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await Table.FindAsync(id);
            if (entity is not null)
            {
                return entity;
            }
            return null;
        }

        public async Task<T> UpdateAsync(Guid Id, T entity)
        {
            var entityToUpdate = await Table.FindAsync(Id);
            if (entityToUpdate is not null)
            {
                context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                if (await context.SaveChangesAsync() > 0)
                {
                    return entityToUpdate;
                }
            }
            return null;
        }

        public async Task<bool> IsExist(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = Table.AsQueryable();
            if (filter is not null)
            {
                query = query.Where(filter);
            }
            return await query.AnyAsync();
        }

        public async Task<PagedResult<T>> GetPagedResult(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int pageNumber = 1, int pageSize = 10, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table.AsQueryable();
            if (filter is not null)
            {
                query = query.Where(filter);
            }
            int totalCount = await query.CountAsync();
            if (orderBy is not null)
            {
                query = orderBy(query);
            }
            if (includeProperties.Length > 0)
            {
                foreach (var item in includeProperties)
                {
                    query = query.Include(item);
                }
            }
            List<T> items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResult<T>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

        }

        public async Task<IEnumerable<T>> GetWithIncludeProperties(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Table.AsQueryable();
            if (includeProperties.Length > 0)
            {
                foreach (var item in includeProperties)
                {
                    query = query.Include(item);

                }
            }
            return query.AsEnumerable();
        }
    }
}
