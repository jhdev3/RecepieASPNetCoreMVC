using Microsoft.AspNetCore.Mvc;
using RecipeWebsiteMVC.DataAccess.Interfaces;
using RecipeWebsiteMVC.Models;
using RecipeWebsiteMVC.Models.ViewModels;
using System.Diagnostics;

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
            RecipeVM rVM = new RecipeVM{ Multiplier = r.Portions, recipe = r};

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
            RecipeVM rVM = new RecipeVM { Multiplier = Multiplier, recipe = r };
            rVM.UpdateIngridiens(); 
            return View(rVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}