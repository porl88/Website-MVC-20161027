namespace MVC.Core.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Data.EntityFramework;
    using Entities;

    public class MockRepository<T> : IRepository<T> where T : BaseEntity
    {
        private List<T> entities = new List<T>();

        protected List<T> Entities
        {
            get
            {
                return this.entities;
            }
        }

        public virtual T Get(int id)
        {
            return this.entities.FirstOrDefault(x => x.Id == id);
        }

        public T GetSingle(Func<IQueryable<T>, T> query)
        {
            return query(this.entities.AsQueryable());
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
            {
                return this.entities.AsQueryable().Where(filter);
            }
            else
            {
                return this.entities;
            }
        }

        public IEnumerable<T> Get(Func<IQueryable<T>, IQueryable<T>> query = null)
        {
            if (query == null)
            {
                return this.entities.AsQueryable();
            }
            else
            {
                return query(this.entities.AsQueryable());
            }
        }

        public IQueryable<T> Query()
        {
            return this.entities.AsQueryable();
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return this.Get(id);
        }

        public async Task<U> GetFirstAsync<U>(Func<IQueryable<T>, IQueryable<U>> query)
        {
            return query(this.entities.AsQueryable()).FirstOrDefault();
        }

        public async Task<U> GetSingleAsync<U>(Expression<Func<T, bool>> where, Expression<Func<T, U>> select)
        {
            return this.entities.AsQueryable().Where(where).Select(select).SingleOrDefault();
        }

        public async Task<List<U>> GetAsync<U>(Func<IQueryable<T>, IQueryable<U>> query)
        {
            return query(this.entities.AsQueryable()).ToList();
        }

        public async Task<IEnumerable<T>> GetAsync(Func<IQueryable<T>, IQueryable<T>> query = null)
        {
            return this.Get(query);
        }

        public virtual T Insert(T entity)
        {
            if (entity.Id > 0)
            {
                var entityInList = this.entities.FirstOrDefault(x => x.Id == entity.Id);
                if (entityInList != null)
                {
                    entityInList = entity;
                }
                else
                {
                    this.entities.Add(entity);
                }
            }
            else
            {
                if (this.entities.Count > 0)
                {
                    entity.Id = this.entities.Max(x => x.Id) + 1;
                }
                else
                {
                    entity.Id = 1;
                }

                this.entities.Add(entity);
            }

            return entity;
        }

        public virtual T Update(T entity)
        {
            if (entity.Id > 0)
            {
                var entityInList = this.entities.FirstOrDefault(x => x.Id == entity.Id);
                if (entityInList != null)
                {
                    entityInList = entity;
                }
                else
                {
                    this.entities.Add(entity);
                }
            }
            else
            {
                if (this.entities.Count > 0)
                {
                    entity.Id = this.entities.Max(x => x.Id) + 1;
                }
                else
                {
                    entity.Id = 1;
                }

                this.entities.Add(entity);
            }

            return entity;
        }

        public virtual void Delete(T entity)
        {
            this.entities.Remove(entity);
        }

        public virtual void Delete(int id)
        {
            var entityToRemove = this.Get().FirstOrDefault(x => x.Id == id);
            if (entityToRemove != null)
            {
                this.Delete(entityToRemove);
            }
        }
    }
}
