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

        T GetSingle(Func<IQueryable<T>, T> query);

        IEnumerable<T> Get(Func<IQueryable<T>, IQueryable<T>> query = null);

        IEnumerable<T> Find(Expression<Func<T, bool>> filter = null);

        IQueryable<T> Query();

        Task<T> GetAsync(int id);

        Task<U> GetFirstAsync<U>(Func<IQueryable<T>, IQueryable<U>> query);

        Task<U> GetSingleAsync<U>(Expression<Func<T, bool>> where, Expression<Func<T, U>> select);

        Task<List<U>> GetAsync<U>(Func<IQueryable<T>, IQueryable<U>> query);

        //T Get(int id);

        //Task<T> GetAsync(int id);

        //T GetSingle(Func<IQueryable<T>, T> query);

        //Task<T> GetSingleAsync(Func<IQueryable<T>, T> query);

        //IEnumerable<T> Find(Expression<Func<T, bool>> filter = null);

        //Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter = null);

        //IEnumerable<T> Get(Func<IQueryable<T>, IQueryable<T>> query = null);

        //Task<IEnumerable<T>> GetAsync(Func<IQueryable<T>, IQueryable<T>> query = null);

        //IQueryable<T> Query();

        T Insert(T entity);

        T Update(T entity);

        void Delete(int id);

        void Delete(T entity);
    }
}
