using GymNutri.Data.Interfaces;
using GymNutri.Infrastructure.Interfaces;
using GymNutri.Infrastructure.SharedKernel;
using GymNutri.Utilities.Constants;
using GymNutri.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace GymNutri.Data.EF
{
    public class EFRepository<T, K> : IRepository<T, K>, IDisposable where T : DomainEntity<K>
    {
        private readonly AppDbContext _context;

        public EFRepository(AppDbContext context)
        {
            _context = context;
        }

        public T FindById(K id, params Expression<Func<T, object>>[] includeProperties)
        {
            return FindAll(includeProperties).SingleOrDefault(x => x.Id.Equals(id));
        }

        public T FindSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return FindAll(includeProperties).SingleOrDefault(predicate);
        }

        public IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = _context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items;
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = _context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items.Where(predicate);
        }

        public void Add(T entity, out bool result, out string message)
        {
            try
            {
                _context.Add(entity);
                result = true;
                message = CommonConstants.Message.Success;
            }
            catch (Exception ex)
            {
                result = false;
                message = ex.Message;
            }
        }

        public void Update(T entity, out bool result, out string message)
        {
            try
            {
                _context.Set<T>().Update(entity);
                result = true;
                message = CommonConstants.Message.Success;
            }
            catch (Exception ex)
            {
                result = false;
                message = ex.Message;
            }
        }

        public virtual void UpdateChangedProperties(K id, T entity, out bool result, out string message, params Expression<Func<T, object>>[] updatedProperties)
        {
            try
            {
                var dbEntity = _context.Set<T>().AsNoTracking().Single(p => p.Id.Equals(id));
                var databaseEntry = _context.Entry(dbEntity);
                var inputEntry = _context.Entry(entity);
                if (updatedProperties.Any())
                {
                    //update explicitly mentioned properties
                    foreach (var property in updatedProperties)
                    {
                        databaseEntry.Property(property).IsModified = true;
                    }
                }
                else
                {
                    //no items mentioned, so find out the updated entries
                    IEnumerable<string> createdProperties = typeof(ICreatedTracking).GetPublicProperties().Select(x => x.Name);
                    IEnumerable<string> domainProperties = typeof(DomainEntity<K>).GetPublicProperties().Select(x => x.Name);

                    var allProperties = databaseEntry.Metadata.GetProperties()
                    .Where(x => !createdProperties.Contains(x.Name))
                    .Where(x => !domainProperties.Contains(x.Name));

                    foreach (var property in allProperties)
                    {
                        var proposedValue = inputEntry.Property(property.Name).CurrentValue;
                        var originalValue = databaseEntry.Property(property.Name).OriginalValue;

                        if ((originalValue != null && proposedValue != null && !originalValue.Equals(proposedValue))
                            || (originalValue == null && proposedValue != null)
                            || (originalValue != null && proposedValue == null)
                            || (property.PropertyInfo.PropertyType.IsGenericType && property.PropertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            databaseEntry.Property(property.Name).IsModified = true;
                            databaseEntry.Property(property.Name).CurrentValue = proposedValue;
                        }
                    }
                }
                _context.Set<T>().Update(dbEntity);

                result = true;
                message = CommonConstants.Message.Success;
            }
            catch (Exception ex)
            {
                result = false;
                message = ex.Message;
            }
        }

        public void SoftRemove(K id, out bool result, out string message)
        {
            try
            {
                var entity = FindById(id);
                entity.Active = false;
                _context.Set<T>().Update(entity);
                result = true;
                message = CommonConstants.Message.Success;
            }
            catch (Exception ex)
            {
                result = true;
                message = ex.Message;
            }
        }

        public void SoftRemove(T entity, out bool result, out string message)
        {
            try
            {
                entity.Active = false;
                _context.Set<T>().Update(entity);
                result = true;
                message = CommonConstants.Message.Success;
            }
            catch (Exception ex)
            {
                result = false;
                message = ex.Message;
            }
        }

        public void Remove(T entity, out bool result, out string message)
        {
            try
            {
                _context.Set<T>().Remove(entity);
                result = true;
                message = CommonConstants.Message.Success;
            }
            catch (Exception ex)
            {
                result = false;
                message = ex.Message;
            }
        }

        public void Remove(K id, out bool result, out string message)
        {
            try
            {
                var entity = FindById(id);
                _context.Set<T>().Remove(entity);
                result = true;
                message = CommonConstants.Message.Success;
            }
            catch (Exception ex)
            {
                result = true;
                message = ex.Message;
            }
        }

        public void RemoveMultiple(List<T> entities, out bool result, out string message)
        {
            try
            {
                _context.Set<T>().RemoveRange(entities);
                result = true;
                message = CommonConstants.Message.Success;
            }
            catch (Exception ex)
            {
                result = false;
                message = ex.Message;
            }
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public bool CheckExist(T entity, out K foundId, out bool result, out string message, Expression<Func<T, bool>> predicate)
        {
            try
            {
                IQueryable<T> data = _context.Set<T>().Where(predicate);
                result = true;
                message = CommonConstants.Message.Success;
                if (data.Any())
                {
                    foundId = data.FirstOrDefault().Id;
                    message = CommonConstants.Message.Existed;
                    return true;
                }
                else
                {
                    foundId = default(K);
                    return false;
                }

            }
            catch (Exception ex)
            {
                foundId = default(K);
                result = false;
                message = ex.Message;
                return false;
            }
        }
    }
}
