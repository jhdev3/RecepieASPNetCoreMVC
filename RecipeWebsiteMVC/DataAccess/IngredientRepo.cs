using RecipeWebsiteMVC.Data;
using RecipeWebsiteMVC.DataAccess.Interfaces;
using RecipeWebsiteMVC.Models;

namespace RecipeWebsiteMVC.DataAccess
{
    public class IngredientRepo : Repository<Ingredient>, IingredientRepo
    {
        private readonly AppDbContext _db;

        public IngredientRepo(AppDbContext db) : base(db)
        {
            _db = db;
        }
    }
}

