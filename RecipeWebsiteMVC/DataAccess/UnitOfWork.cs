using RecipeWebsiteMVC.Data;
using RecipeWebsiteMVC.DataAccess.Interfaces;

namespace RecipeWebsiteMVC.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _db;

        public IAppUserFavoritRecipeRepo UserFavoritRecipe { get; private set; }
        public ICategoryRepo Category { get; private set; } //Trevlig säkerhet och "Make sense när _db är private i funktionerna"

        //En liten gissning här är att tråddarna har tillgång till appens resurer så att det inte ska kunna ändra _db contexten är viktigt.
        //Det här är inget som bör ändras under livslängden för en databas operation.
        public IRecipeRepository Recipe { get; private set; }
        public IAppUserRepo User { get; private set; }

        public IdirectionRepo Direction { get; private set; }
        public IingredientRepo Ingredient { get; private set; }

        public UnitOfWork(AppDbContext db) 
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Recipe = new RecipeRepository(_db); 
            User = new AppUserRepository(_db);
            UserFavoritRecipe = new AppUserFavoritRecipeRepo(_db);
            Direction = new DirectionRepo(_db); 
            Ingredient = new IngredientRepo(_db);    
        }

        public void Save()
        {
             _db.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();   
        }
    }
}
