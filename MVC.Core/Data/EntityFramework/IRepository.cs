namespace MVC.Core.Data.EntityFramework
{
    using System.Threading.Tasks;

    public interface IRepository<T> : IReadOnlyRepository<T> where T : class
    {
        T Insert(T entityToInsert);

        T Update(T entityToUpdate);

        void Delete(object id);

        void Delete(T entityToDelete);

        void Save();

        Task SaveAsync();
    }
}
