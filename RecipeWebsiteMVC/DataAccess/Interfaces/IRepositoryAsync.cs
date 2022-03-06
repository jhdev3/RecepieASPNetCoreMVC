using RecipeWebsiteMVC.Models;
using System.Linq.Expressions;

namespace RecipeWebsiteMVC.DataAccess.Interfaces
{
    /// <summary>
    /// Generic Interface for a simple repository can be used to create a cache or smimulate an database
    /// To be used in CRUD-Operations 
    /// And it's follows the Async pattern
    /// </summary>
    /// <typeparam name="T">Needs to have BaseEntity properties IE id etc </typeparam>
    public interface IRepositoryAsync<T> where T : class
    {
        /*
         * IEnumerable<T> is great for working with sequences that are iterated in-memory, but
           IQueryable<T> allows for out-of-memory things like a remote data source, such as a database or web service.
         
         IEnumerable executes the query first and then it filters the data based on conditions.
         Thats why i use IEnumerable just beacuse this is not supposed to build queries, we dont want to repeat them :)
         
         */
        /// <summary>
        /// Async Method to Get Class by Id
        /// </summary>
        /// <param name="id">Needs to be an Id in the db</param>
        /// <returns></returns>
        Task<T> GetAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        /// <summary>
        /// Find returns Null if not found, Good instead of Find can be used for other tabels not just ID
        /// </summary>
        /// <param name="predicate">Funtion that returns bool</param>
        /// <returns></returns>
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Simple Add Not Async sense its Save in unit of work that needs to be async, This just put changes to the tabels
        /// </summary>
        /// <param name="entity">What to be added</param>
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



/* 
    Viktigt Repository har inget ansvar för att ha koll på ändringar, Updateringar,
    Spara Till databasen. De utför bara logiken sedan är det upp till 
    Unit of Work
    att Updatera Tabellerna i databasen på ett sätt som gör att alla andringar som ska göras görs.
    Failar en failar alla -> Lyckas en, Lyckas alla. 
 

 */