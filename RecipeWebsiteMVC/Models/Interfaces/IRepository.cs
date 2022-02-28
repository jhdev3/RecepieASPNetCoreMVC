using RecipeWebsiteMVC.Models;

namespace RecipeWebsiteMVC.Models.Interfaces
{
    /// <summary>
    /// Interface for a simple repository can be used to create a cache or smimulate an database
    /// </summary>
    /// <typeparam name="T">Needs to have BaseEntity properties IE id etc </typeparam>
    public interface IRepository<T> where T : BaseEntity
    {
        /*
         * IEnumerable<T> is great for working with sequences that are iterated in-memory, but
           IQueryable<T> allows for out-of-memory things like a remote data source, such as a database or web service.
           Using IQueryable beacuse to simulate a database but also in the need of creating a Cache service e.g not having the cache on same maching or same container as the WebServer
         */
        IQueryable<T> Collection();
        void Delete(string Id);
        T Find(string Id);
        void Insert(T t);
        void Update(T t);
    }
}