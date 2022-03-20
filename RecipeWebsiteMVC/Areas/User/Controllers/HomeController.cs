using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeWebsiteMVC.DataAccess.Interfaces;
using RecipeWebsiteMVC.Models;
using RecipeWebsiteMVC.Models.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

namespace RecipeWebsiteMVC.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _UnitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _UnitOfWork = unitOfWork;
        }

        public  async Task<IActionResult> Index()
        {
            IEnumerable<Recipe> recipes =  await _UnitOfWork.Recipe.GetAllAsync();
            return View(recipes);
        }

        public async Task<IActionResult> Details(string id)
        {
            Recipe r = await _UnitOfWork.Recipe.GetDirectionsAndIngredients(id);

            if(r == null)
            {
                return NotFound(id);
            }
            RecipeVM rVM = new RecipeVM(r);

            bool isAjax = HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
            {
                return PartialView("Details", rVM);
            }

            return View(rVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> Details(string id, int Multiplier)
        {
            Recipe r = await _UnitOfWork.Recipe.GetDirectionsAndIngredients(id);
            if (r == null)
            {
                return NotFound(id);
            }
            RecipeVM rVM = new RecipeVM(r, Multiplier); 
            rVM.UpdateIngridiens(); 
            return View(rVM);
        }

        [Authorize]
        public async Task<IActionResult> AddToListAsync(string RecipeId)
        {
            var getIdentity = (ClaimsIdentity)User.Identity;
            var claim = getIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //Borde inte kunna vara noll medtanke på Authorize men men då sickar vi bara tillbaka dig
            if (claim == null)
            {
                return RedirectToAction("Details", new { id = RecipeId });
            }

            var test = await _UnitOfWork.UserFavoritRecipe.GetFirstOrDefaultAsync(u => u.ApplicationUserID == claim.Value && u.RecipeID == RecipeId);
            if(test == null) { 
                UserFavoriteRecipe UserFR = new UserFavoriteRecipe
                {
                    RecipeID = RecipeId,
                    ApplicationUserID = claim.Value
                };
                _UnitOfWork.UserFavoritRecipe.Add(UserFR);
                _UnitOfWork.Save();
            }

            return RedirectToAction("Details", new { id = RecipeId });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}