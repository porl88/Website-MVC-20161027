namespace MVC.Core.Data.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<T>
    {
        T Get(int id);

        Task<T> GetAsync(int id);

        T Get(Expression<Func<T, bool>> filter);

        Task<T> GetAsync(Expression<Func<T, bool>> filter);

        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        T Insert(T entity);

        T Update(T entity);

        void Delete(int id);

        void Delete(T entity);
    }
}
