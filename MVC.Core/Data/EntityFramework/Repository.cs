// http://www.asp.net/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
// http://blogs.msdn.com/b/mrtechnocal/archive/2014/03/16/asynchronous-repositories.aspx

namespace MVC.Core.Data.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
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

        public virtual async Task<T> GetAsync(int id)
        {
            return await this.databaseSet.FindAsync(id);
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

        public async Task<T> GetSingleAsync(Func<IQueryable<T>, T> query)
        {
            var dataset = this.databaseSet;

            foreach (var include in this.includes)
            {
                dataset.Include(include);
            }

            var factory = Task<T>.Factory;
            return await factory.StartNew(() => query(dataset));
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

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter = null)
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

            return await dataset.ToListAsync();
        }

        public IEnumerable<T> Get(Func<IQueryable<T>, IQueryable<T>> query = null)
        {
            var dataset = this.databaseSet;

            foreach (var include in this.includes)
            {
                dataset.Include(include);
            }

            if (query == null)
            {
                return dataset;
            }
            else
            {
                return query(dataset);
            }
        }

        public async Task<IEnumerable<T>> GetAsync(Func<IQueryable<T>, IQueryable<T>> query = null)
        {
            var dataset = this.databaseSet;

            foreach (var include in this.includes)
            {
                dataset.Include(include);
            }

            if (query == null)
            {
                return await dataset.ToListAsync();
            }
            else
            {
                return await query(dataset).ToListAsync();
            }
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
    }
}
