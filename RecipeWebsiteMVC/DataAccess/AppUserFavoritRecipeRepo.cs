using Microsoft.EntityFrameworkCore;
using RecipeWebsiteMVC.Data;
using RecipeWebsiteMVC.DataAccess.Interfaces;
using RecipeWebsiteMVC.Models;

namespace RecipeWebsiteMVC.DataAccess
{
    public class AppUserFavoritRecipeRepo : Repository<UserFavoriteRecipe>, IAppUserFavoritRecipeRepo
    {
        private readonly AppDbContext _db;

        public AppUserFavoritRecipeRepo(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesForUser(string userId)
        {
            //Skapar Eager Loading med Include:)
            var userList = await _db.userFavoriteRecipes.Where(x => x.ApplicationUserID == userId).Include(r => r.recipe).ToListAsync();
            var recipeList = userList.Select(x => x.recipe).ToList(); 
            return recipeList;
        }
        //Update här behövs troligtvis inte då det anting blir lägga till eller ta bort :)
    }
}
