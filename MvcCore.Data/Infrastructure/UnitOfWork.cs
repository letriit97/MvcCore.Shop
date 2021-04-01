using Microsoft.EntityFrameworkCore;
using MvcCore.Data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvcCore.Data.Infrastructure
{
    public class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork<TContext>
           where TContext : DbContext, IDisposable
    {
        /// <summary>
        /// The repositories
        /// </summary>
        private Dictionary<Type, object> _repositories;

        /// <summary>
        /// The disposed
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        public TContext Context { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork{TContext}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="ArgumentNullException">context</exception>
        public UnitOfWork(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new Repository<TEntity>(Context);
            return (IRepository<TEntity>)_repositories[type];
        }

        /// <summary>
        /// Commits the specified automatic history.
        /// </summary>
        /// <param name="autoHistory">if set to <c>true</c> [automatic history].</param>
        /// <returns></returns>
        public int Commit(bool autoHistory = false)
        {
            //if (autoHistory) Context.EnsureAutoHistory();
            return Context.SaveChanges();
        }

        /// <summary>
        /// Commits the asynchronous.
        /// </summary>
        /// <param name="autoHistory">if set to <c>true</c> [automatic history].</param>
        /// <returns></returns>
        public async Task<int> CommitAsync(bool autoHistory = false)
        {
            //if (autoHistory) Context.EnsureAutoHistory();

            return await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            disposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
