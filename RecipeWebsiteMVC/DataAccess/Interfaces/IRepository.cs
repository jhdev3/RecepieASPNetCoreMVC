using RecipeWebsiteMVC.Models;
using System.Linq.Expressions;

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
         
         IEnumerable executes the query first and then it filters the data based on conditions.
         Thats why i use IEnumerable just beacuse this is not supposed to build queries, we dont want to repeat them :)
         
         */
        T Get(string id);
        IEnumerable<T> GetAll();
        /// <summary>
        /// Find returns Null if not found
        /// </summary>
        /// <param name="predicate">Funtion that returns bool</param>
        /// <returns></returns>
        T GetFirstOrDefault(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Simple Add
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);
        /// <summary>
        /// Add a list of entities
        /// </summary>
        /// <param name="entities"></param>
        void AddRange(IEnumerable<T> entities);
        /// <summary>
        /// Remove one
        /// </summary>
        /// <param name="entity"></param>
        void Remove(T entity);
        /// <summary>
        /// Remove a list of entities
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRange(IEnumerable<T> entities);
    }
}