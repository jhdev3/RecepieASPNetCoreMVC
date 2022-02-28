using RecipeWebsiteMVC.Models;

namespace RecipeWebsiteMVC.Models.Interfaces
{
    public interface IReposatory
    {
        IQueryable<Recipe> Collection();
        void Delete(string Id);
        Recipe Find(string Id);
        void Insert(Recipe t);
        void Update(Recipe t);
    }
}