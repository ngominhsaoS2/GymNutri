using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace GymNutri.Infrastructure.Interfaces
{
    public interface IRepository<T, K> where T : class
    {
        T FindById(K id, params Expression<Func<T, object>>[] includeProperties);

        T FindSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        void Add(T entity, out bool result, out string message);

        //K Add2(T entity, out bool result, out string message);

        void Update(T entity, out bool result, out string message);

        void UpdateChangedProperties(K id, T entity, out bool result, out string message, params Expression<Func<T, object>>[] updatedProperties);

        void SoftRemove(T entity, out bool result, out string message);

        void SoftRemove(K id, out bool result, out string message);

        void Remove(T entity, out bool result, out string message);

        void Remove(K id, out bool result, out string message);

        void RemoveMultiple(List<T> entities, out bool result, out string message);

        bool CheckExist(T entity, out K foundId, out bool result, out string message, Expression<Func<T, bool>> predicate);

    }
}
