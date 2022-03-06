using RecipeWebsiteMVC.Models;

namespace RecipeWebsiteMVC.DataAccess.Interfaces
{
    /// <summary>
    /// Inteface for readOnly
    /// </summary>
    public interface IReadOnlyRecipe
    {
        Recipe GetRecipe(string id);
        IEnumerable<Recipe> GetAllRecipes();
    }
}
