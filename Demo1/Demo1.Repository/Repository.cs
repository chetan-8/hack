using Demo1.Core.Repository;
using Demo1.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly Demo1Context _dbContext;

        public Repository(Demo1Context dbContext)
        {
            _dbContext = dbContext;
            // _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        /// <summary>
        /// Adds new item in this set
        /// </summary>
        /// <param name="item">The item.</param>
        public virtual void Add(TEntity item)
        {
            if (item != null)
            {
                GetSet().Add(item);
            }
        }

        /// <summary>
        /// Adds new range of items in this set (more efficient than using Add in a loop)
        /// </summary>
        /// <param name="items"></param>
        public virtual void AddRange(IEnumerable<TEntity> items)
        {
            if (items != null)
            {
                GetSet().AddRange(items);
            }
        }

        public virtual TEntity Attach(TEntity item)
        {
            return item == null ? null : GetSet().Attach(item)?.Entity;
        }

        public void Deattach(TEntity item)
        {
            if (null != item)
            {
                _dbContext.Entry(item).State = EntityState.Detached;
            }
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Remove(TEntity item)
        {
            GetSet().Remove(item);
        }

        /// <summary>
        /// Deletes the list of specified item
        /// </summary>
        /// <param name="items">The item</param>
        public async Task<int> DeleteRangeAsync(IEnumerable<TEntity> items)
        {
            GetSet().RemoveRange(items);
            return await CommitAsync();
        }

        /// <summary>
        /// Modifies the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public virtual void Modify(TEntity item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
        }

        /// <summary>
        /// Modifies the specified property
        /// </summary>
        /// <param name="item">The item.</param>
        private void PropertyState(TEntity item, bool IsModified, List<string> excludeProperties)
        {
            foreach (var name in excludeProperties)
            {
                var property = _dbContext.Entry(item).Property(name);
                if (property != null)
                {
                    _dbContext.Entry(item).Property(name).IsModified = IsModified;
                }
            }
        }

        //public virtual void EditEntity(TEntity entity, 
        //    TypeOfEditEntityProperty typeOfEditEntityProperty, params string[] properties)
        //{
        //    var find = _dbContext.Set<TEntity>().Find(entity.GetType().GetProperty("Id").GetValue(entity, null));
        //    if (find == null)
        //        throw new Exception("id not found in database");
        //    if (typeOfEditEntityProperty == TypeOfEditEntityProperty.Ignore)
        //    {
        //        foreach (var item in entity.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.GetProperty))
        //        {
        //            if (!item.CanRead || !item.CanWrite)
        //                continue;
        //            if (properties.Contains(item.Name))
        //                continue;
        //            item.SetValue(find, item.GetValue(entity, null), null);
        //        }
        //    }
        //    else if (typeOfEditEntityProperty == TypeOfEditEntityProperty.Take)
        //    {
        //        foreach (var item in entity.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.GetProperty))
        //        {
        //            if (!item.CanRead || !item.CanWrite)
        //                continue;
        //            if (!properties.Contains(item.Name))
        //                continue;
        //            item.SetValue(find, item.GetValue(entity, null), null);
        //        }
        //    }
        //    else
        //    {
        //        foreach (var item in entity.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.GetProperty))
        //        {
        //            if (!item.CanRead || !item.CanWrite)
        //                continue;
        //            item.SetValue(find, item.GetValue(entity, null), null);
        //        }
        //    }
        //    _dbContext.SaveChanges();
        //}

        //public enum TypeOfEditEntityProperty
        //{
        //    Ignore,
        //    Take
        //}

        /// <summary>
        /// Update the specified Item
        /// </summary>
        /// <param name="item"></param>
        public virtual void Update(TEntity item)
        {
            _dbContext.Update(item);
        }
        /// <summary>
        /// Update multiple Items
        /// </summary>
        /// <param name="item"></param>
        public virtual void UpdateRange(IEnumerable<TEntity> items)
        {
            _dbContext.UpdateRange(items);
        }

        public virtual async Task<IEnumerable<TEntity>> FindAllNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync();
        }

        public virtual async Task<bool> RecordExists(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().AnyAsync(predicate);
        }

        /// <summary>
        /// Update multiple Items
        /// </summary>
        /// <param name="item"></param>
        public async Task<int> UpdateRangeAsync(IEnumerable<TEntity> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("entity");
            }
            UpdateRange(items);
            return await CommitAsync();
        }
        /// <summary>
        /// Merges the specified persisted.
        /// </summary>
        /// <param name="persisted">The persisted.</param>
        /// <param name="current">The current.</param>
        public virtual void Merge(TEntity persisted, TEntity current)
        {
            _dbContext.Entry(persisted).CurrentValues.SetValues(current);
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual TEntity Get(int id)
        {
            return GetSet().Find(id);
        }


        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return GetQuery();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await GetQuery().ToListAsync();
        }
        public virtual async Task<int> InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            AddRange(entities);
            return await CommitAsync();
        }


        public IQueryable<TEntity> GetQuery()
        {
            return GetSet();
        }
        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await GetSet().FindAsync(id);
        }
        //public virtual async Task<TEntity> GetAsync(int id, params Expression<Func<TEntity, object>>[] properties)
        //{
        //    if (properties == null)
        //        throw new ArgumentNullException(nameof(properties));
        //    var query = GetSet() as IQueryable<TEntity>;

        //    query = properties
        //               .Aggregate(query, (current, property) => current.Include(property));

        //    var res = query.AsNoTracking().ToList().FirstOrDefault(x => x.Id == id);
        //    return res;
        //}

        public async Task<IEnumerable<TEntity>> FindAllWithNavigation(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] properties)
        {
            if (properties == null)
                throw new ArgumentNullException(nameof(properties));
            var query = GetSet() as IQueryable<TEntity>;

            query = properties
                       .Aggregate(query, (current, property) => current.Include(property));

            var res = query.Where(condition);
            return res;
        }
        public virtual async Task<TEntity> GetAsync(int? id)
        {
            return (id != null) ? await GetAsync((int)id) : await System.Threading.Tasks.Task.FromResult((TEntity)null);
        }

        public virtual async Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        protected DbSet<TEntity> GetSet()
        {
            return _dbContext.Set<TEntity>();
        }
        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> InsertAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Add(entity);
            return await CommitAsync();
        }
        public int Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Add(entity);
            return Commit();
        }
        public async Task<int> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Update(entity);
            return await CommitAsync();
        }

        public async Task<int> UpdateAsync(TEntity entity, bool IsModified, List<string> excludeProperties)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Update(entity);
            PropertyState(entity, IsModified, excludeProperties);
            return await CommitAsync();
        }
        public async Task<int> DeleteAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Remove(entity);
            return await CommitAsync();
        }

       


        public void OnBeforeSaving(DbContext context)
        {
            var entries = context?.ChangeTracker?.Entries();

            if (entries == null)
            {
                return;
            }

            foreach (var entry in entries)
            {
                // get all the properties and are of type string
                var propertyValues = entry.CurrentValues.Properties.Where(p => p.ClrType == typeof(string));

                foreach (var prop in propertyValues)
                {
                    // access the correct column by it's name and trim the value if it's not null
                    if (entry.CurrentValues[prop.Name] != null) entry.CurrentValues[prop.Name] = entry.CurrentValues[prop.Name].ToString().Trim();
                }
            }
        }

        public virtual async Task<TEntity> FindSingleNoTracking(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = GetQuery();
            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }
            var result = await query.Where(predicate).AsNoTracking().ToListAsync();
            return result.FirstOrDefault();
        }

    }
}
