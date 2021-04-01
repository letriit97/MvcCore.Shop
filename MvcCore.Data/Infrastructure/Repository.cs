using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using MvcCore.Data.Infrastructure.Interfaces;

namespace MvcCore.Data.Infrastructure
{
    /// <summary>
    /// The repository.
    /// </summary>
    /// <typeparam name="T">The model object</typeparam>
    /// <seealso cref="IRepository{T}" />
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Properties
        /// <summary>
        /// The database context
        /// </summary>
        protected readonly DbContext _dbContext;

        /// <summary>
        /// The database set
        /// </summary>
        protected readonly DbSet<T> _dbSet;
        private object context;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        /// <summary>
        /// Finds the specified match.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns>T.</returns>
        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return _dbSet.SingleOrDefault(match);
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns>IQueryable&lt;T&gt;.</returns>
        public virtual IQueryable<T> FindAll()
        {
            return _dbSet;
        }

        /// <summary>
        /// Finds all asynchronous.
        /// </summary>
        /// <returns>IQueryable&lt;T&gt;.</returns>
        public virtual IQueryable<T> FindAllAsync()
        {
            return _dbSet;
        }

        /// <summary>
        /// Finds the asynchronous.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _dbSet.SingleOrDefaultAsync(match);
        }


        /// <summary>
        /// Get all
        /// </summary>
        /// <returns>The <see cref="IQueryable" /></returns>
        public virtual IQueryable<T> GetAll()
        {
            return _dbSet;
        }




        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id">The identity</param>
        /// <returns>The model</returns>
        public virtual T GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Gets the asynchronous by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        public virtual async Task<T> GetAsyncById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Gets or sets Get model by id as detached
        /// </summary>
        /// <param name="id">The identity</param>
        /// <returns>The entity object</returns>
        public virtual T GetByIdAsDetached(Guid id)
        {
            var entity = _dbSet.Find(id);
            EntityEntry entityEntry = _dbContext.Entry(entity);
            entityEntry.State = EntityState.Detached;
            return entity;
        }

        /// <summary>
        /// Add new entity object
        /// </summary>
        /// <param name="entity">The entity object</param>
        public virtual void Add(T entity)
        {
            EntityEntry entityEntry = _dbContext.Entry(entity);
            if (entityEntry.State != EntityState.Detached)
            {
                entityEntry.State = EntityState.Added;
            }
            else
            {
                _dbSet.Add(entity);
            }
        }

        /// <summary>
        /// Update the entity object
        /// </summary>
        /// <param name="entity">The entity object</param>
        public virtual void Update(T entity)
        {
            EntityEntry entityEntry = _dbContext.Entry(entity);
            if (entityEntry.State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            entityEntry.State = EntityState.Modified;
        }

        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public virtual int Count()
        {
            return _dbSet.Count();
        }

        /// <summary>
        /// Counts the asynchronous.
        /// </summary>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        public virtual Task<int> CountAsync()
        {
            return _dbSet.CountAsync();
        }


        /// <summary>
        /// Delete the entity object
        /// </summary>
        /// <param name="entity">The entity object</param>
        public virtual void Delete(T entity)
        {
            EntityEntry entityEntry = _dbContext.Entry(entity);
            if (entityEntry.State != EntityState.Deleted)
            {
                entityEntry.State = EntityState.Deleted;
            }
            else
            {
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
            }
        }

        /// <summary>
        /// Delete the entity object by id
        /// </summary>
        /// <param name="id">The identity</param>
        public virtual void Delete(Guid id)
        {
            var entity = GetById(id);
            if (entity == null) return; // not found; assume already deleted.
            Delete(entity);
        }

        /// <summary>
        /// Check the entity existed or not
        /// </summary>
        /// <param name="id">The identity</param>
        /// <returns>Is existed or not</returns>
        public bool Exists(Guid id)
        {
            return _dbSet.Find(id) != null;
        }

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns>System.Int32.</returns>
        public virtual int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        public virtual async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// list as an asynchronous operation.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;T&gt;&gt;.</returns>
        public virtual async Task<IEnumerable<T>> ListAsync()
        {
            var query = _dbSet.AsNoTracking();
            return await query.ToListAsync();
        }

        /// <summary>
        /// list as an asynchronous operation.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>Task&lt;IEnumerable&lt;T&gt;&gt;.</returns>
        public virtual async Task<IEnumerable<T>> ListAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            var query = _dbSet.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        /// <summary>
        /// list as an asynchronous operation.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public virtual async Task<IEnumerable<T>> ListAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties = null)
        {
            var query = _dbSet.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                query = includeProperties(query);
            }
            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        public void AddRangge(T[] entity)
        {
            throw new NotImplementedException();
        }


    }
}
