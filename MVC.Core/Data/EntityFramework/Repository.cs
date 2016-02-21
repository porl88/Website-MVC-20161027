// http://www.asp.net/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
// http://blogs.msdn.com/b/mrtechnocal/archive/2014/03/16/asynchronous-repositories.aspx

namespace MVC.Core.Data.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext context;
        private readonly DbSet<T> databaseSet;
        private readonly string[] includes = new string[0];

        public Repository(DbContext context)
        {
            this.context = context;
            this.context.Database.Log = s => Debug.WriteLine(s);
            this.context.Database.Log = s => Console.Write(s);
            this.databaseSet = context.Set<T>();
        }

        public Repository(DbContext context, string[] includes)
            : this(context)
        {
            this.includes = includes;
        }

        public virtual T Get(int id)
        {
            return this.databaseSet.Find(id);
        }

        public T GetSingle(Func<IQueryable<T>, T> query)
        {
            var dataset = this.databaseSet;

            foreach (var include in this.includes)
            {
                dataset.Include(include);
            }

            return query(dataset);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> dataset = this.databaseSet;

            if (filter != null)
            {
                dataset = dataset.Where(filter);
            }

            foreach (var include in this.includes)
            {
                dataset = dataset.Include(include);
            }

            return dataset.ToList();
        }

        public List<T> Get()
        {
            return this.databaseSet.ToList();
        }

        public List<U> Get<U>(Func<IQueryable<T>, IQueryable<U>> query)
        {
            var dataset = this.GetDbQuery();
            return query(dataset).ToList();
        }

        public virtual IQueryable<T> Query()
        {
            if (this.includes.Length == 0)
            {
                return this.databaseSet;
            }
            else
            {
                var query = this.databaseSet.Include(this.includes[0]);
                if (this.includes.Length > 1)
                {
                    for (var i = 1; i < this.includes.Length; i++)
                    {
                        query = query.Include(this.includes[i]);
                    }
                }

                return query;
            }
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await this.databaseSet.FindAsync(id);
        }

        public async Task<U> GetFirstAsync<U>(Func<IQueryable<T>, IQueryable<U>> query)
        {
            var dataset = this.GetDbQuery();
            return await query(dataset).FirstOrDefaultAsync();
        }

        public async Task<U> GetSingleAsync<U>(Expression<Func<T, bool>> where, Expression<Func<T, U>> select)
        {
            var local = this.databaseSet.Local.AsQueryable().Where(where).Select(select).SingleOrDefault();

            if (local != null)
            {
                return local;
            }
            else
            {
                var dataset = this.GetDbQuery();
                return await dataset.Where(where).Select(select).SingleOrDefaultAsync();
            }
        }

        public async Task<List<U>> GetAsync<U>(Func<IQueryable<T>, IQueryable<U>> query)
        {
            var dataset = this.GetDbQuery();
            return await query(dataset).ToListAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> where)
        {
            return await this.databaseSet.Where(where).CountAsync();
        }



        //public virtual T Get(int id)
        //{
        //    return this.databaseSet.Find(id);
        //}

        //public virtual async Task<T> GetAsync(int id)
        //{
        //    return await this.databaseSet.FindAsync(id);
        //}

        //public T GetSingle(Func<IQueryable<T>, T> query)
        //{
        //    var dataset = this.databaseSet;

        //    foreach (var include in this.includes)
        //    {
        //        dataset.Include(include);
        //    }

        //    return query(dataset);
        //}

        //public async Task<T> GetSingleAsync(Func<IQueryable<T>, T> query)
        //{
        //    var dataset = this.databaseSet;

        //    foreach (var include in this.includes)
        //    {
        //        dataset.Include(include);
        //    }

        //    var factory = Task<T>.Factory;
        //    return await factory.StartNew(() => query(dataset));
        //}

        //public IEnumerable<T> Find(Expression<Func<T, bool>> filter = null)
        //{
        //    IQueryable<T> dataset = this.databaseSet;

        //    if (filter != null)
        //    {
        //        dataset = dataset.Where(filter);
        //    }

        //    foreach (var include in this.includes)
        //    {
        //        dataset = dataset.Include(include);
        //    }

        //    return dataset.ToList();
        //}

        //public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter = null)
        //{
        //    IQueryable<T> dataset = this.databaseSet;

        //    if (filter != null)
        //    {
        //        dataset = dataset.Where(filter);
        //    }

        //    foreach (var include in this.includes)
        //    {
        //        dataset = dataset.Include(include);
        //    }

        //    return await dataset.ToListAsync();
        //}

        //public IEnumerable<T> Get(Func<IQueryable<T>, IQueryable<T>> query = null)
        //{
        //    var dataset = this.databaseSet;

        //    foreach (var include in this.includes)
        //    {
        //        dataset.Include(include);
        //    }

        //    if (query == null)
        //    {
        //        return dataset;
        //    }
        //    else
        //    {
        //        return query(dataset);
        //    }
        //}

        //public async Task<IEnumerable<T>> GetAsync(Func<IQueryable<T>, IQueryable<T>> query = null)
        //{
        //    var dataset = this.databaseSet;

        //    foreach (var include in this.includes)
        //    {
        //        dataset.Include(include);
        //    }

        //    if (query == null)
        //    {
        //        return await dataset.ToListAsync();
        //    }
        //    else
        //    {
        //        return await query(dataset).ToListAsync();
        //    }
        //}

        //public virtual IQueryable<T> Query()
        //{
        //    if (this.includes.Length == 0)
        //    {
        //        return this.databaseSet;
        //    }
        //    else
        //    {
        //        var query = this.databaseSet.Include(this.includes[0]);
        //        if (this.includes.Length > 1)
        //        {
        //            for (var i = 1; i < this.includes.Length; i++)
        //            {
        //                query = query.Include(this.includes[i]);
        //            }
        //        }

        //        return query;
        //    }
        //}

        public virtual T Insert(T entityToInsert)
        {
            this.databaseSet.Add(entityToInsert);
            this.context.Entry(entityToInsert).State = EntityState.Added;
            return entityToInsert;
        }

        public virtual T Update(T entityToUpdate)
        {
            this.databaseSet.Attach(entityToUpdate);
            this.context.Entry(entityToUpdate).State = EntityState.Modified;
            return entityToUpdate;
        }

        public virtual void Delete(int id)
        {
            T entityToDelete = this.databaseSet.Find(id);
            this.Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (this.context.Entry(entityToDelete).State == EntityState.Detached)
            {
                this.databaseSet.Attach(entityToDelete);
            }

            this.databaseSet.Remove(entityToDelete);
        }

        private DbQuery<T> GetDbQuery()
        {
            DbQuery<T> dataset = this.databaseSet;

            foreach (var include in this.includes)
            {
                dataset = dataset.Include(include);
            }

            return dataset;
        }
    }
}
