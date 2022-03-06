using RecipeWebsiteMVC.Data;
using RecipeWebsiteMVC.DataAccess.Interfaces;
using RecipeWebsiteMVC.Models;
using Microsoft.EntityFrameworkCore;

/* 
 *Regel bara i de här klasserna man får prata med db ;) 
 * 
*/
namespace RecipeWebsiteMVC.DataAccess
{
    public class RecipeRepository : Repository<Recipe>, IRecipeRepository
    {
        private AppDbContext _db;

        public RecipeRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void DeleteCascade(Recipe obj)
        {
            _db.Directions.RemoveRange(obj.Directions);
            _db.Ingredients.RemoveRange(obj.Ingredients);
            _db.Recipes.Remove(obj);
        }
        /// <summary>
        /// Get The full recipe including all the ingridients and directions
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Recipe Can include null values</returns>
        public async Task<Recipe> GetDirectionsAndIngredients(string id)
        {
            Recipe recipe = await _db.Recipes
              .Where(r => r.Id == id)
              .Include(i => i.Ingredients)
              .Include(d => d.Directions)
              .FirstOrDefaultAsync();

            return recipe;

        }

        /// <summary>
        /// Updating the full recipe 
        /// Only updates image when a new one is uploaded 
        /// </summary>
        /// <param name="obj">Recipe</param>
        public void Update(Recipe obj)
        {
            //Restrict Updating RecipeFromDb entity frameWork will keep track of this
            //Vill i princip updatera allt utom Image som bara görs när man laddar upp en ny.
            var RecipeFromDb = _db.Recipes.FirstOrDefault(r => r.Id == obj.Id);
            if (RecipeFromDb != null)
            {
                RecipeFromDb.Title =  obj.Title;
                RecipeFromDb.Description = obj.Description;
                RecipeFromDb.Category = obj.Category;   
                RecipeFromDb.Portions = obj.Portions;
                RecipeFromDb.EditedAt = DateTime.Now;
                RecipeFromDb.Directions = obj.Directions;
                RecipeFromDb.Ingredients = obj.Ingredients;

                if(obj.Image != null)
                {
                    RecipeFromDb.Image = obj.Image; 
                }
                
            }
        }

    }
}
