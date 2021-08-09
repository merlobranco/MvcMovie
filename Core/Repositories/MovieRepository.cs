using LinqKit;
using Microsoft.Extensions.Logging;
using MvcMovie.Core.IRepositories;
using MvcMovie.Data;
using MvcMovie.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MvcMovie.Core.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MvcMovieContext context, ILogger logger) : base(context, logger) { }

        public async Task<IEnumerable<Movie>> Find(string genre, string title)
        {
            Expression<Func<Movie, bool>> predicate = PredicateBuilder.New<Movie>(true);
            if (!string.IsNullOrWhiteSpace(genre)) 
            {
                predicate = predicate.And(m => m.Genre == genre);
            }

            if (!string.IsNullOrWhiteSpace(title)) 
            {
                predicate = predicate.And(m => m.Title.Contains(title));
            }

            return await Find(predicate);
         }

        public async Task<IEnumerable<string>> FindGenres()
        {
            try {
                return await _dbSet.OrderBy(m => m.Genre).Select(m => m.Genre).Distinct().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} FindGenres function error", this.GetType());
                return Enumerable.Empty<string>();
            }
        }
    }
}
