using Contracts.DataAccess.Repositories;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Implementation of repository access methods
    /// for Relational Database Engine
    /// </summary>
    /// <typeparam name="TEntity">Type of the data table to which 
    /// current reposity is attached</typeparam>
    public class RepositoryBase<TKey, TEntity> : IRepositoryBase<TKey, TEntity> where TEntity : class
    {
        /// <summary>
        /// Entity framework DB context holding connection information and properties
        /// and tracking entity states 
        /// </summary>
        protected DbContext Context { get; set; }

        /// <summary>
        /// Representation of table in database
        /// </summary>
        protected DbSet<TEntity> DbSet()
        {
            return Context.Set<TEntity>();
        }

        public RepositoryBase(ForumDbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Adds entity to the database
        /// </summary>
        /// <param name="entity">Entity to add</param>
        public async Task AddAsync(TEntity entity)
        {
            await DbSet().AddAsync(entity);
        }

        /// <summary>
        /// Ads collection of entities to the database
        /// </summary>
        /// <param name="entities">Enumerable list of entities</param>
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await DbSet().AddRangeAsync(entities);
        }

        /// <summary>
        /// All records in a table
        /// </summary>
        /// <returns>Queryable expression tree</returns>
        public IQueryable<TEntity> All()
        {
            return DbSet().AsQueryable();
        }

        /// <summary>
        /// Records which fulfil the <paramref name="search"/> condition
        /// </summary>
        /// <returns>Queryable expression tree</returns>
        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> search)
        {
            return DbSet().Where(search);
        }

        /// <summary>
        /// The result collection won't be tracked by the context
        /// </summary>
        /// <returns>Expression tree</returns>
        public IQueryable<TEntity> AllAsNoTracking()
        {
            return DbSet()
                .AsNoTracking();
        }

        /// <summary>
        /// Records which fulfil the <paramref name="search"/> condition returned as no tracking
        /// </summary>
        /// <returns>Expression tree</returns>
        public IQueryable<TEntity> FindByConditionAsNoTracking(Expression<Func<TEntity, bool>> search)
        {
            return DbSet()
                .Where(search)
                .AsNoTracking();
        }

        /// <summary>
        /// Deletes a record from database
        /// </summary>
        /// <param name="entity">Entity representing record to be deleted</param>
        public void Delete(TEntity entity)
        {
            EntityEntry entry = Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                DbSet().Attach(entity);
            }

            entry.State = EntityState.Deleted;
        }

        /// <summary>
        /// Detaches given entity from the context
        /// </summary>
        /// <param name="entity">Entity to be detached</param>
        public void Detach(TEntity entity)
        {
            EntityEntry entry = Context.Entry(entity);

            entry.State = EntityState.Detached;
        }

        /// <summary>
        /// Gets specific record from database by primary key
        /// </summary>
        /// <param name="id">record identificator</param>
        /// <returns>Single record</returns>
        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await DbSet().FindAsync(id);
        }

        public async Task<TEntity> GetByIdsAsync(object[] id)
        {
            return await DbSet().FindAsync(id);
        }

        /// <summary>
        /// Updates set of records in the database
        /// </summary>
        /// <param name="entities">Enumerable collection of entities to be updated</param>
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            DbSet().UpdateRange(entities);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            DbSet().RemoveRange(entities);
        }

        public void DeleteRange(Expression<Func<TEntity, bool>> deleteWhereClause)
        {
            var entities = FindByCondition(deleteWhereClause);
            DeleteRange(entities);
        }

        /// <summary>
        /// Disposing the context when it is not neede
        /// Don't have to call this method explicitely
        /// Leave it to the IoC container
        /// </summary>
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
