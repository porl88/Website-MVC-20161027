// https://www.asp.net/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
// http://cpratt.co/truly-generic-repository/
// http://blogs.msdn.com/b/mrtechnocal/archive/2014/03/16/asynchronous-repositories.aspx

namespace MVC.Core.Data.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class EntityFrameworkReadOnlyRepository<T> : IReadOnlyRepository<T> where T : class
    {
        protected readonly DbContext context;
        protected readonly DbSet<T> dbSet;

        /*
         * ALTERNATIVE:
         * internal DbContext context;
         * internal DbSet<T> dbSet;
        */

        public EntityFrameworkReadOnlyRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
#if DEBUG
            this.context.Database.Log = s => Debug.WriteLine(s);
            this.context.Database.Log = s => Console.Write(s);
#endif
        }

        public virtual T GetById(object id)
        {
            return this.dbSet.Find(id);
        }

        public virtual List<TResult> Get<TResult>(Func<IQueryable<T>, IQueryable<TResult>> query, params string[] includes)
        {
            IQueryable<T> dbSet = this.dbSet;

            dbSet = this.GetQueryable(includes: includes);

            return query(dbSet).ToList();
        }

        public virtual TResult GetSingle<TResult>(Func<IQueryable<T>, IQueryable<TResult>> query, params string[] includes)
        {
            IQueryable<T> dbSet = this.dbSet;

            dbSet = this.GetQueryable(includes: includes);

            return query(dbSet).SingleOrDefault();
        }

        public virtual TResult GetFirst<TResult>(Func<IQueryable<T>, IQueryable<TResult>> query, params string[] includes)
        {
            IQueryable<T> dbSet = this.dbSet;

            dbSet = this.GetQueryable(includes: includes);

            return query(dbSet).FirstOrDefault();
        }

        public virtual int GetCount(Expression<Func<T, bool>> where = null)
        {
            IQueryable<T> dbSet = this.dbSet;

            return this.GetQueryable(where).Count();
        }

        public virtual bool Exists(Expression<Func<T, bool>> where = null)
        {
            return this.GetQueryable(where).Any();
        }

        public virtual async Task<T> GetByIdAsync(object id)
        {
            return await this.dbSet.FindAsync(id);
        }

        public virtual async Task<List<TResult>> GetAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> query, params string[] includes)
        {
            IQueryable<T> dbSet = this.dbSet;

            dbSet = this.GetQueryable(includes: includes);

            return await query(dbSet).ToListAsync();
        }

        public virtual async Task<TResult> GetSingleAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> query, params string[] includes)
        {
            IQueryable<T> dbSet = this.dbSet;

            dbSet = this.GetQueryable(includes: includes);

            return await query(dbSet).SingleOrDefaultAsync();
        }

        public virtual async Task<TResult> GetFirstAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> query, params string[] includes)
        {
            IQueryable<T> dbSet = this.dbSet;

            dbSet = this.GetQueryable(includes: includes);

            return await query(dbSet).FirstOrDefaultAsync();
        }

        public virtual async Task<int> GetCountAsync(Expression<Func<T, bool>> where = null)
        {
            IQueryable<T> dbSet = this.dbSet;

            return await this.GetQueryable(where).CountAsync();
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> where = null)
        {
            IQueryable<T> dbSet = this.dbSet;

            return await this.GetQueryable(where).AnyAsync();
        }

        protected virtual IQueryable<T> GetQueryable
        (
            Expression<Func<T, bool>> where = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int skip = 0,
            int take = 0,
            params string[] includes
        )
        {
            IQueryable<T> query = this.context.Set<T>();

            if (where != null)
            {
                query = query.Where(where);
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip > 0)
            {
                query = query.Skip(skip);
            }

            if (take > 0)
            {
                query = query.Take(take);
            }

            return query;
        }
    }
}
