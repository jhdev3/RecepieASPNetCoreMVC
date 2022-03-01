using RecipeWebsiteMVC.Models;
namespace RecipeWebsiteMVC.DataAccess.Interfaces
{
    /// <summary>
    /// Generic Interface for a simple repository can be used to create a cache or smimulate an database
    /// To be used in CRUD-Operations 
    /// </summary>
    /// <typeparam name="T">Needs to have BaseEntity properties IE id etc </typeparam>
    public interface IRepository<T> where T : BaseEntity
    {
        /*
         * IEnumerable<T> is great for working with sequences that are iterated in-memory, but
           IQueryable<T> allows for out-of-memory things like a remote data source, such as a database or web service.
           Using IQueryable beacuse to simulate a database but also in the need of creating a Cache service e.g not having the cache on same maching or same container as the WebServer
         */
        void Insert(T t);
        T Find(string Id);
        void Update(T t);
        void Delete(string Id);

        /// <summary>
        /// Steps away a bit from CRUD, but simple way of getting the full list of objects.
        /// </summary>
        /// <returns> Querie of Repository  objects </returns>
        IQueryable<T> Collection();

    }
}