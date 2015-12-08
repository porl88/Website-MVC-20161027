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

        T GetSingle(Func<IQueryable<T>, T> query);

        Task<T> GetSingleAsync(Func<IQueryable<T>, T> query);

        IEnumerable<T> Get();

        Task<IEnumerable<T>> GetAsync();

        IEnumerable<T> Get(Func<IQueryable<T>, IQueryable<T>> query);

        Task<IEnumerable<T>> GetAsync(Func<IQueryable<T>, IQueryable<T>> query);

        IQueryable<T> Query();

        T Insert(T entity);

        T Update(T entity);

        void Delete(int id);

        void Delete(T entity);
    }
}
