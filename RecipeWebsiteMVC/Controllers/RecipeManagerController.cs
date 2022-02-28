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

        //Fördelen med att använda ett Interface som ActionReuslt eller ActionReuslt
        // Är att det går att retunera olika typer av Actions eftersom det är ett Interface
        /* Tex :
        public ViewResult Delete(string id)
        {
            if (id != null)
            {
                return PartialView("Index", new { id });
            }
            return View();
            
        } 
        Mer Info om result types https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-6.0
         */
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
