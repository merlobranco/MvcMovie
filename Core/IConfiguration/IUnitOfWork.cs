using MvcMovie.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IMovieRepository MovieRepository { get; }

        Task<bool> Complete();
        bool HasChanges();
    }
}
