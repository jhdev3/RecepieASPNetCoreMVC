using RecipeWebsiteMVC.Models;

namespace RecipeWebsiteMVC.DataAccess.Interfaces
{
    public interface IAppUserRepo : IRepositoryAsync<ApplicationUser> //Not using anything other than standard
    {
    }
}
