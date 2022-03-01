using Microsoft.AspNetCore.Mvc;
using RecipeWebsiteMVC.Cache;
using RecipeWebsiteMVC.Models;

namespace RecipeWebsiteMVC.Controllers
{
    public class RecipeManagerController : Controller
    {
        RepoCache<Recipe> recipeRepo;
        public RecipeManagerController()
        {
            recipeRepo = new RepoCache<Recipe>();
        }
        public IActionResult Index()
        {
            List<Recipe> recipes = recipeRepo.Collection().ToList();
            return View(recipes);
        }

        //Fördelen med att använda ett IActionReuslt eller ActionReuslt istället för tex ViewReuslt
        // Är att det går att retunera olika typer av Actions
        // Mer Info om result types https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-6.0
         
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
