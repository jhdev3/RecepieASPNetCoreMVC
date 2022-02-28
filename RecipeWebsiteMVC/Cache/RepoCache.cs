using RecipeWebsiteMVC.Models;
using RecipeWebsiteMVC.Models.Interfaces;
using System.Runtime.Caching;

namespace RecipeWebsiteMVC.Cache
{
    /// <summary>
    /// Creates temp list repos for accesing cache instead of DB 
    /// </summary>

    //Skapar en Cache istället för Static lists för Models som ska visas och sparas. 
    public class RepoCache : IReposatory
    {

        ObjectCache cache = MemoryCache.Default;
        List<Recipe> items;
        string className;

        public RepoCache()
        {
            className = typeof(Recipe).Name;
            items = cache[className] as List<Recipe>;
            if (items == null)
            {
                items = new List<Recipe>();
            }
        }

        private void Commit()
        {
            cache[className] = items;
        }

        public void Insert(Recipe t)
        {
            items.Add(t);
            Commit();
        }

        public void Update(Recipe t)
        {
            Recipe tToUpdate = items.Find(i => i.Id == t.Id);

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

        public Recipe Find(string Id)
        {
            Recipe t = items.Find(i => i.Id == Id);
            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception(className + " Not found");
            }
        }

        public IQueryable<Recipe> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(string Id)
        {
            Recipe tToDelete = items.Find(i => i.Id == Id);

            if (tToDelete != null)
            {
                items.Remove(tToDelete);
                Commit();

            }
            else
            {
                throw new Exception(className + " Not found");
            }
        }

    }

}
