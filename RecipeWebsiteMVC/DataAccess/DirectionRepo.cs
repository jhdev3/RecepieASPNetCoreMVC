using RecipeWebsiteMVC.Data;
using RecipeWebsiteMVC.DataAccess.Interfaces;
using RecipeWebsiteMVC.Models;

namespace RecipeWebsiteMVC.DataAccess
{
    public class DirectionRepo : Repository<Direction>, IdirectionRepo
    {
        private readonly AppDbContext _db;

        public DirectionRepo(AppDbContext db) : base(db)
        {
            _db = db;
        }
    }
}

