using RecipeWebsiteMVC.Models;
using RecipeWebsiteMVC.Models.Interfaces;
using System.Runtime.Caching;
using System.Linq;


namespace RecipeWebsiteMVC.Cache
{
    /// <summary>
    /// Creates temp list repos for accesing cache instead of DB 
    /// </summary>

    //Skapar en Cache istället för Static lists för Models som ska visas och sparas. 
    //Denna struktur skulle kunna implementeras i ett annat projekt för tex en moln Cache DB eller något sådant
    // Om Kummunikation med databasen senare skulle bli ett problem. 
    public class RepoCache<T> : IRepository<T> where T : BaseEntity
    {

        ObjectCache cache = MemoryCache.Default;
        List<T> itemsList;
        string className;

        public RepoCache()
        {
            className = typeof(T).Name;
            itemsList = cache[className] as List<T>;
            if (itemsList == null)
            {
                itemsList = new List<T>();
            }
        }

        private void Commit()
        {
            cache[className] = itemsList;
        }

        public void Insert(T t)
        {
            itemsList.Add(t);
            Commit();
        }

        public void Update(T t)
        {
            T tToUpdate = itemsList.Find(i => i.Id == t.Id);

            if (tToUpdate != null)
            {
                tToUpdate = t;
                Commit();

            }
            else
            {
                throw new Exception(className + " Not found");
            }
        }

     

        public T Find(string Id)
        {
            T t = itemsList.Find(i => i.Id == Id);
            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception(className + " Not found");
            }
        }

        public IQueryable<T> Collection()
        {
            return itemsList.AsQueryable();
        }

        public void Delete(string Id)
        {
            T tToDelete = itemsList.Find(i => i.Id == Id);

            if (tToDelete != null)
            {
                itemsList.Remove(tToDelete);
                Commit();

            }
            else
            {
                throw new Exception(className + " Not found");
            }
        }

    }

}
