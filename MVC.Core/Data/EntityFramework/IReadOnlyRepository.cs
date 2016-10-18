namespace MVC.Core.Data.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IReadOnlyRepository<T> where T : class
    {
        T GetById(object id);

        List<TResult> Get<TResult>(Func<IQueryable<T>, IQueryable<TResult>> query, params string[] includes);

        TResult GetSingle<TResult>(Func<IQueryable<T>, IQueryable<TResult>> query, params string[] includes);

        TResult GetFirst<TResult>(Func<IQueryable<T>, IQueryable<TResult>> query, params string[] includes);

        int GetCount(Expression<Func<T, bool>> where = null);

        bool Exists(Expression<Func<T, bool>> where = null);

        Task<T> GetByIdAsync(object id);

        Task<List<TResult>> GetAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> query, params string[] includes);

        Task<TResult> GetSingleAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> query, params string[] includes);

        Task<TResult> GetFirstAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> query, params string[] includes);

        Task<int> GetCountAsync(Expression<Func<T, bool>> where = null);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> where = null);
    }
}
