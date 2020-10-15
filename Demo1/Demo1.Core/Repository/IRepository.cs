using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.Core.Repository
{
    /// <summary>
    /// Base interface for implement a "Repository Pattern"
    /// </summary>
    /// <typeparam name="TEntity">Type of entity for this repository </typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Add item into repository
        /// </summary>
        /// <param name="item">Item to add to repository</param>
        void Add(TEntity item);

        /// <summary>
        /// Add items into repository (more efficient than using Add in a loop)
        /// </summary>
        /// <param name="item"></param>
        void AddRange(IEnumerable<TEntity> item);

        /// <summary>
        /// Inserts multiple items to the DB
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<int> InsertRangeAsync(IEnumerable<TEntity> item);

        /// <summary>
        /// Delete list of items 
        /// </summary>
        /// <param name="item">Items to delete</param>
        Task<int> DeleteRangeAsync(IEnumerable<TEntity> items);
        TEntity Attach(TEntity item);
        void Deattach(TEntity item);

        /// <summary>
        /// Set item as modified
        /// </summary>
        /// <param name="item">Item to modify</param>
        void Modify(TEntity item);


        /// <summary>
        /// Update the specified Item 
        /// </summary>
        /// <param name="item"></param>
        void Update(TEntity item);

        Task<int> UpdateAsync(TEntity entity, bool IsModified, List<string> excludeProperties);

        /// <summary>
        /// Update multiple Items
        /// </summary>
        /// <param name="items"></param>
        void UpdateRange(IEnumerable<TEntity> items);

        /// <summary>
        /// Update multiple Items and commits to DB
        /// </summary>
        /// <param name="items"></param>
        Task<int> UpdateRangeAsync(IEnumerable<TEntity> items);

        Task<IEnumerable<TEntity>> FindAllNoTracking(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Eager loading with child properties
        /// </summary>
        /// <param name="id"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        //Task<TEntity> GetAsync(int id, params Expression<Func<TEntity, object>>[] properties);

        /// <summary>
        /// Execute SQL Command
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        //void ExecuteSqlCommand(string sql, object[] parameters = null);

        /// <summary>
        /// Execute SQL Command Async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        //Task ExecuteSqlCommandAsync(string sql, object[] parameters = null);

        /// <summary>
        /// Sets modified entity into the repository. 
        /// When calling Commit() method in UnitOfWork 
        /// these changes will be saved into the storage
        /// </summary>
        /// <param name="persisted">The persisted item</param>
        /// <param name="current">The current item</param>
        void Merge(TEntity persisted, TEntity current);

        /// <summary>
        /// Get element by entity key
        /// </summary>
        /// <param name="id">Entity key value</param>
        /// <returns></returns>
        TEntity Get(int id);
        /// <summary>
        /// Get all elements of type TEntity in repository
        /// </summary>
        /// <returns>List of selected elements</returns>
        IEnumerable<TEntity> GetAll();
        #region Async

        Task<TEntity> GetAsync(int id);
        Task<TEntity> GetAsync(int? id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<int> CommitAsync();
        int Commit();
        Task<int> InsertAsync(TEntity entity);
        int Insert(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        /// <summary>
        /// Delete specied item 
        /// </summary>
        /// <param name="item">Item to delete</param>
        Task<int> DeleteAsync(TEntity entity);
        /// <summary>
        /// Get DB set in IQueryable format
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetQuery();
        #endregion
        /// <summary>
        /// Find all the items from table based on the condition(s)
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> condition);


        Task<IEnumerable<TEntity>> FindAllWithNavigation(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] properties);

       
        Task<bool> RecordExists(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindSingleNoTracking(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

    }
}
