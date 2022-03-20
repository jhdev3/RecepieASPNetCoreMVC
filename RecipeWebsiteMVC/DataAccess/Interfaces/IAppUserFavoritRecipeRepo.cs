using RecipeWebsiteMVC.Models;

namespace RecipeWebsiteMVC.DataAccess.Interfaces
{
    public interface IAppUserFavoritRecipeRepo : IRepositoryAsync<UserFavoriteRecipe>
    {
        public Task<IEnumerable<Recipe>> GetAllRecipesForUser(string userId);

    }
}
