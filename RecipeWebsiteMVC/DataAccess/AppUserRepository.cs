using RecipeWebsiteMVC.Data;
using RecipeWebsiteMVC.DataAccess.Interfaces;
using RecipeWebsiteMVC.Models;

namespace RecipeWebsiteMVC.DataAccess
{
    public class AppUserRepository : Repository<ApplicationUser>, IAppUserRepo
    {
        private readonly AppDbContext _db;

        public AppUserRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
    }
}

