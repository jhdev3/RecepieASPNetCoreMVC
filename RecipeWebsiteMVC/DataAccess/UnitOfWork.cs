using RecipeWebsiteMVC.Data;
using RecipeWebsiteMVC.DataAccess.Interfaces;

namespace RecipeWebsiteMVC.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _db;
        public ICategoryRepo Category { get; private set; }

        public IRecipeRepository Recipe { get; private set; }

        public UnitOfWork(AppDbContext db) 
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Recipe = new RecipeRepository(_db); 
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
