// https://www.asp.net/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
// http://cpratt.co/truly-generic-repository/
// http://blogs.msdn.com/b/mrtechnocal/archive/2014/03/16/asynchronous-repositories.aspx

namespace MVC.Core.Data.EntityFramework
{
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Threading.Tasks;

    public class EntityFrameworkRepository<T> : EntityFrameworkReadOnlyRepository<T>, IRepository<T> where T : class
    {
        public EntityFrameworkRepository(DbContext context) : base (context)
        {
        }

        public virtual T Insert(T entityToInsert)
        {
            this.dbSet.Add(entityToInsert);
            this.context.Entry(entityToInsert).State = EntityState.Added;
            return entityToInsert;
        }

        public virtual T Update(T entityToUpdate)
        {
            this.dbSet.Attach(entityToUpdate);
            this.context.Entry(entityToUpdate).State = EntityState.Modified;
            return entityToUpdate;
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = this.dbSet.Find(id);
            this.Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (this.context.Entry(entityToDelete).State == EntityState.Detached)
            {
                this.dbSet.Attach(entityToDelete);
            }

            this.dbSet.Remove(entityToDelete);
        }


        public virtual void Save()
        {
            try
            {
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                this.ThrowEnhancedValidationException(e);
            }
        }

        public virtual Task SaveAsync()
        {
            try
            {
                return this.context.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                this.ThrowEnhancedValidationException(e);
            }

            return Task.FromResult(0);
        }

        private void ThrowEnhancedValidationException(DbEntityValidationException e)
        {
            var errorMessages = e.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
            var fullErrorMessage = string.Join("; ", errorMessages);
            var exceptionMessage = string.Concat(e.Message, " The validation errors are: ", fullErrorMessage);
            throw new DbEntityValidationException(exceptionMessage, e.EntityValidationErrors);
        }
    }
}
