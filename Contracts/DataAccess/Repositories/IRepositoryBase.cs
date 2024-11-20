using System.Linq.Expressions;

namespace Contracts.DataAccess.Repositories
{
    /// <summary>
    /// Abstraction of repository access methods
    /// </summary>
    public interface IRepositoryBase<TKey, TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// All records in a table
        /// </summary>
        /// <returns>Queryable expression tree</returns>
        IQueryable<TEntity> All();

        /// <summary>
        /// All records in a table
        /// </summary>
        /// <returns>Queryable expression tree</returns>
        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> search);

        /// <summary>
        /// The result collection won't be tracked by the context
        /// </summary>
        /// <returns>Expression tree</returns>
        IQueryable<TEntity> AllAsNoTracking();

        /// <summary>
        /// The result collection won't be tracked by the context
        /// </summary>
        /// <returns>Expression tree</returns>
        IQueryable<TEntity> FindByConditionAsNoTracking(Expression<Func<TEntity, bool>> search);

        /// <summary>
        /// Gets specific record from database by primary key
        /// </summary>
        /// <param name="id">record identificator</param>
        /// <returns>Single record</returns>
        Task<TEntity> GetByIdAsync(TKey id);

        Task<TEntity> GetByIdsAsync(object[] id);

        /// <summary>
        /// Adds entity to the database
        /// </summary>
        /// <param name="entity">Entity to add</param>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Ads collection of entities to the database
        /// </summary>
        /// <param name="entities">Enumerable list of entities</param>
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates set of records in the database
        /// </summary>
        /// <param name="entities">Enumerable collection of entities to be updated</param>
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes a record from database
        /// </summary>
        /// <param name="entity">Entity representing record to be deleted</param>
        void Delete(TEntity entity);

        void DeleteRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Detaches given entity from the context
        /// </summary>
        /// <param name="entity">Entity to be detached</param>
        void Detach(TEntity entity);
    }
}
