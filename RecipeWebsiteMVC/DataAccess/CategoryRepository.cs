using RecipeWebsiteMVC.Data;
using RecipeWebsiteMVC.DataAccess.Interfaces;
using RecipeWebsiteMVC.Models;

namespace RecipeWebsiteMVC.DataAccess
{
    public class CategoryRepository : Repository<Category>, ICategoryRepo
    {
        private readonly AppDbContext _db;

        public CategoryRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category obj)
        {
            //Enkelt error här Concurrency behöver fixas ;)
            _db.Update(obj);
        }
    }
}
