namespace MVC.Core.Data.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    // http://www.asp.net/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
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

        public virtual T Get(Expression<Func<T, bool>> filter)
        {
            return this.databaseSet.SingleOrDefault(filter);
        }

        public async virtual Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await this.databaseSet.SingleOrDefaultAsync(filter);
        }

        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = this.databaseSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var include in this.includes)
            {
                query = query.Include(include);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public async virtual Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = this.databaseSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var include in this.includes)
            {
                query = query.Include(include);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public virtual T Insert(T entity)
		{
			this.databaseSet.Add(entity);
			this.context.Entry(entity).State = EntityState.Added;
			return entity;
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
