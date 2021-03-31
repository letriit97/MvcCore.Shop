using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MvcCore.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        /// <summary>
        /// Commits the specified automatic history.
        /// </summary>
        /// <param name="autoHistory">if set to <c>true</c> [automatic history].</param>
        /// <returns></returns>
        int Commit(bool autoHistory = false);

        /// <summary>
        /// Commits the asynchronous.
        /// </summary>
        /// <param name="autoHistory">if set to <c>true</c> [automatic history].</param>
        /// <returns></returns>
        Task<int> CommitAsync(bool autoHistory = false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    /// <seealso cref="IDisposable" />
    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }
}
