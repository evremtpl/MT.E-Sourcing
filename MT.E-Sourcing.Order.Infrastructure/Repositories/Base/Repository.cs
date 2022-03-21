using Microsoft.EntityFrameworkCore;
using MT.E_Sourcing.Order.Domain.Entities.Base;
using MT.E_Sourcing.Order.Domain.Repositories.Base;
using MT.E_Sourcing.Order.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MT.E_Sourcing.Order.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T:Entity
    {
        protected readonly OrderContext _context;
        public Repository( OrderContext context)
        {
            _context = context;
        }
        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();

            if (disableTracking) query = query.AsNoTracking();
         
            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if(predicate!=null) query = query.Where(predicate);

            if (orderBy != null) return await orderBy(query).ToListAsync();

            return await query.ToListAsync();

        }

       

       
    }
}
