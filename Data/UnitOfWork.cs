using Microsoft.Extensions.Logging;
using MvcMovie.Core.IConfiguration;
using MvcMovie.Core.IRepositories;
using MvcMovie.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MvcMovieContext _context;
        private readonly ILogger _logger;


        public IMovieRepository MovieRepository => new MovieRepository(_context, _logger);
        public UnitOfWork(MvcMovieContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

        }

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
