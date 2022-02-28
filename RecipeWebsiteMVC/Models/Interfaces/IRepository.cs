using RecipeWebsiteMVC.Models;

namespace RecipeWebsiteMVC.Models.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        void Delete(string Id);
        T Find(string Id);
        void Insert(string t);
        void Update(string t);
    }
}