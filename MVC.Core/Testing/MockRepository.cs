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

        public T GetById(object id)
        {
            return this.entities.FirstOrDefault(x => x.Id == (int)id);
        }

        public List<TResult> Get<TResult>(Func<IQueryable<T>, IQueryable<TResult>> query, params string[] includes)
        {
            var q = this.entities.AsQueryable();
            return query(q).ToList();
        }

        public TResult GetSingle<TResult>(Func<IQueryable<T>, IQueryable<TResult>> query, params string[] includes)
        {
            var q = this.entities.AsQueryable();
            return query(q).SingleOrDefault();
        }

        public TResult GetFirst<TResult>(Func<IQueryable<T>, IQueryable<TResult>> query, params string[] includes)
        {
            var q = this.entities.AsQueryable();
            return query(q).FirstOrDefault();
        }

        public T Insert(T entityToInsert)
        {
            if (entityToInsert.Id > 0)
            {
                var entityInList = this.entities.FirstOrDefault(x => x.Id == entityToInsert.Id);
                if (entityInList != null)
                {
                    entityInList = entityToInsert;
                }
                else
                {
                    this.entities.Add(entityToInsert);
                }
            }
            else
            {
                if (this.entities.Count > 0)
                {
                    entityToInsert.Id = this.entities.Max(x => x.Id) + 1;
                }
                else
                {
                    entityToInsert.Id = 1;
                }

                this.entities.Add(entityToInsert);
            }

            return entityToInsert;
        }

        public T Update(T entityToUpdate)
        {
            if (entityToUpdate.Id > 0)
            {
                var entityInList = this.entities.FirstOrDefault(x => x.Id == entityToUpdate.Id);
                if (entityInList != null)
                {
                    entityInList = entityToUpdate;
                }
                else
                {
                    this.entities.Add(entityToUpdate);
                }
            }
            else
            {
                if (this.entities.Count > 0)
                {
                    entityToUpdate.Id = this.entities.Max(x => x.Id) + 1;
                }
                else
                {
                    entityToUpdate.Id = 1;
                }

                this.entities.Add(entityToUpdate);
            }

            return entityToUpdate;
        }

        public void Delete(T entityToDelete)
        {
            this.entities.Remove(entityToDelete);
        }

        public void Delete(object id)
        {
            var entityToRemove = this.GetById(id);
            if (entityToRemove != null)
            {
                this.Delete(entityToRemove);
            }
        }

        public int GetCount(Expression<Func<T, bool>> where = null)
        {
            var q = this.entities.AsQueryable();
            return q.Where(where).Count();
        }

        public bool Exists(Expression<Func<T, bool>> where = null)
        {
            var q = this.entities.AsQueryable();
            return q.Where(where).Any();
        }

        public Task<bool> ExistsAsync(Expression<Func<T, bool>> where = null)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return this.GetById(id);
        }

        public async Task<List<TResult>> GetAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> query, params string[] includes)
        {
            return this.Get(query);
        }

        public async Task<TResult> GetSingleAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> query, params string[] includes)
        {
            return this.GetSingle(query);
        }

        public async Task<TResult> GetFirstAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> query, params string[] includes)
        {
            return this.GetFirst(query);
        }

        public async Task<int> GetCountAsync(Expression<Func<T, bool>> where = null)
        {
            return this.GetCount(where);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
