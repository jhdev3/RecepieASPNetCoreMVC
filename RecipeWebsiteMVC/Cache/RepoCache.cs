using RecipeWebsiteMVC.Models;
using RecipeWebsiteMVC.DataAccess.Interfaces;
using System.Runtime.Caching;
using System.Linq;


namespace RecipeWebsiteMVC.Cache
{
    /// <summary>
    /// Creates temp Dictionary repos for accesing cache instead of DB  
    /// </summary>
    
    //Skapar en Cache istället för Static lists för Models som ska visas och sparas. 
    //Denna struktur skulle kunna implementeras i ett annat projekt för tex en moln Cache DB eller något sådant
    // Om Kummunikation med databasen senare skulle bli ett problem. 
    //Samt en enkel klass att använda i Unit tester för att testa Modeller.
    //Tanken var även att använda Irepo i SQLAcces även där och använda interfacet i alla Controllers som använder som hanterar data.
    //I tidigare versioner av .Net core fanns App start och det gick att mapa sina repos där så att man kunde använda Interfacet överallt.
    //Nu vet jag inte riktigt hur jag ska åstakomma det på samma sätt :)
  
    
    /* To Be Continued This setup Failed when implementing db and
     * Repo Pattern Need to change a bit not much but a bit 
     * 
    public class RepoCache<T> : IRepository<T> where T : BaseEntity
    {

        ObjectCache cache = MemoryCache.Default;
        Dictionary<string, T> itemsList; //Using Dictionary,  all objects have an Id => faster Find:)
        string className;

        public RepoCache()
        {
            className = typeof(T).Name;
            itemsList = cache[className] as Dictionary<string, T>;
            if (itemsList == null)
            {
                itemsList = new Dictionary<string, T>();
            }
        }
        /// <summary>
        /// Add to Cache
        /// </summary>
        private void Commit()//In database its important to use UnitiOfWork so evertything or nothing saves incase of error. 
        {
            cache[className] = itemsList;
        }
        /// <summary>
        /// Add to list
        /// </summary>
        /// <param name="t">Base Entity objekt</param>
        public void Insert(T t)
        {
            itemsList.Add(t.Id, t);
            Commit();
        }
        /// <summary>
        /// Update Cache and List
        /// </summary>
        /// <param name="t"></param>
        /// <exception cref="KeyNotFoundException"></exception>
        public void Update(T t)
        {

            if (itemsList.ContainsKey(t.Id))
            {
                itemsList[t.Id] = t;
                Commit();

            }
            else
            {
                throw new KeyNotFoundException(className + " Not found");
            }
        }

     
        /// <summary>
        /// Find Object Based on ID
        /// </summary>
        /// <param name="Id">Id/indexparameter</param>
        /// <returns>Object if exists</returns>
        /// <exception cref="Exception">NotFoundE</exception>
        public T Find(string Id)
        {

            if (itemsList.ContainsKey(Id))
            {
                return itemsList[Id];
            }
            else
            {
                throw new KeyNotFoundException(Id + " Not found");
            }
        }
        /// <summary>
        /// Get a Querable list of all items in the Cache
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> Collection()
        {
            return itemsList.Values.AsQueryable();
        }
        /// <summary>
        /// Delete object 
        /// </summary>
        /// <param name="Id">Index</param>
        /// <exception cref="Exception"></exception>
        public void Delete(string Id)
        {
            if (itemsList.ContainsKey(Id))
            {
                itemsList.Remove(Id);

                Commit();
            }
            else
            {
                throw new KeyNotFoundException(Id + " Not found");
            }
        }

    }
  */
}
