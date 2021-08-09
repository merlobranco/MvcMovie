using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MvcMovie.Core.IRepositories;
using MvcMovie.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MvcMovie.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected MvcMovieContext _context;
        internal DbSet<T> _dbSet;
        public readonly ILogger _logger;

        public GenericRepository(MvcMovieContext context, ILogger logger)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _logger = logger;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetAll function error", this.GetType());
                return Enumerable.Empty<T>();
            }
        }

        public async Task<bool> Exists(int id) {
            return await GetById(id) != null;
            
        }

        public async Task<T> GetById(int id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetById function error", this.GetType());
                return null;
            }
        }

        public async Task<bool> Insert(T entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }
        public async Task<bool> Update(T entity)
        {
            try
            {
                if (entity == null)
                    return false;

                // There is no UpdateAsync
                var updated = await Task.Run(() => _dbSet.Update(entity));
                return updated != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", this.GetType());
                return false;
            }
        }

        public async Task<bool> DeleteById(int id)
        {
            try
            {
                var existing = await _dbSet.FindAsync(id);

                return await Delete(existing);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", this.GetType());
                return false;
            }
        }

        public async Task<bool> Delete(T entity)
        {
            try
            {
                if (entity == null)
                    return false;

                var deleted = await Task.Run(() => _dbSet.Remove(entity));

                return deleted != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", this.GetType());
                return false;
            }
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            try {
                return await _dbSet.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Find function error", this.GetType());
                return Enumerable.Empty<T>();
            }
        }
    }
}
