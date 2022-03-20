using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeWebsiteMVC.DataAccess.Interfaces;
using RecipeWebsiteMVC.Models;
using System.Security.Claims;

namespace RecipeWebsiteMVC.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class UserRecipeListController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public UserRecipeListController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var getIdentity = (ClaimsIdentity)User.Identity;
            var claim = getIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //Borde inte kunna vara noll medtanke på Authorize men men då sickar vi bara tillbaka dig
            if (claim == null)
            {
                return NotFound();
            }
            var recipes = await _UnitOfWork.UserFavoritRecipe.GetAllRecipesForUser(claim.Value);
            return View(recipes);
        }
    }
}
