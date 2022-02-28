using Microsoft.AspNetCore.Mvc;
using RecipeWebsiteMVC.Cache;
using RecipeWebsiteMVC.Models;

namespace RecipeWebsiteMVC.Controllers
{
    public class RecipeManagerController : Controller
    {
        RepoCache recipeRepo;
        public RecipeManagerController()
        {
            recipeRepo = new RepoCache();
        }
        public IActionResult Index()
        {
            List<Recipe> recipes = recipeRepo.Collection().ToList();
            return View(recipes);
        }

        public IActionResult Create()
        {
            var recipe = new Recipe();

        
            return View(recipe);
        }

        [HttpPost]
        public IActionResult Create(Recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return View(recipe);
            }
            else
            {

                recipeRepo.Insert(recipe);

                return RedirectToAction("Index");
            }

        }
    }
}
