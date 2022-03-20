using RecipeWebsiteMVC.Models;

namespace RecipeWebsiteMVC.DataAccess.Interfaces
{
    //Behöver ingen update det görs på Cascade med Recipe
    public interface IingredientRepo : IRepositoryAsync<Ingredient>
    {
    }
}
