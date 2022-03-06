using RecipeWebsiteMVC.Models;

namespace RecipeWebsiteMVC.DataAccess.Interfaces
{
    public interface ICategoryRepo : IRepositoryAsync<Category>
    {
        void Update(Category category); 
    }
}
