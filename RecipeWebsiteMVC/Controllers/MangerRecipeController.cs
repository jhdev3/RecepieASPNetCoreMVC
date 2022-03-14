using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeWebsiteMVC.Data;
using RecipeWebsiteMVC.DataAccess.Interfaces;
using RecipeWebsiteMVC.Models;

namespace RecipeWebsiteMVC.Controllers
{
    public class MangerRecipeController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;

        public MangerRecipeController(IUnitOfWork context)
        {
            _UnitOfWork = context;
        }
        //MVC 4: Men intressant hur man kan använda sig av Performing Multiple Operations in Parallel

        //https://docs.microsoft.com/en-us/aspnet/mvc/overview/performance/using-asynchronous-methods-in-aspnet-mvc-4

        //Visar en enkel lista på alla recept ;) 
        //Get Request
        public async Task<IActionResult> Index()
        {
            var recipies = await _UnitOfWork.Recipe.GetAllAsync();
            return View(recipies);
        }
        //Get - Create
        public IActionResult Create()
        {
            ViewBag.IngridientsCount = 1;
            ViewBag.DirectionsCount = 1;
            ViewBag.CreateEdit = "Create";

            return View();  
        }
        [HttpPost]  
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> Create(Recipe recipe, IFormFile file)
        {

            if (!ModelState.IsValid)
            {   
                // in View stuff ;)
                ViewBag.IngridientsCount = recipe.Ingredients.Count;
                ViewBag.DirectionsCount = recipe.Directions.Count;
                //Typ av View istället för att copy pasta och hålla på att trixa med 2 views som i princip är Lika
                ViewBag.CreateEdit = "Create"; 
                return View(recipe);  
            }
            ///Database things :)
            _UnitOfWork.Recipe.Add(recipe); 

            await _UnitOfWork.SaveAsync();//UnitOfWork :)
            return RedirectToAction("Index");   
        }
        public async Task<IActionResult> Details(string id)
        {
            if(id == null)
            {
                return NotFound(id);
            }
            var recipe = await _UnitOfWork.Recipe.GetDirectionsAndIngredients(id);
             
            if (recipe == null)
            {
                return NotFound(id);
            }
            return View(recipe);    
        }
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound(id);
            }
           
            var recipe = await _UnitOfWork.Recipe.GetDirectionsAndIngredients(id);

            if (recipe == null)
            {
                return NotFound(id);
            }
           
            ViewBag.IngridientsCount = recipe.Ingredients.Count;
            ViewBag.DirectionsCount = recipe.Directions.Count;
            ViewBag.CreateEdit = "Edit";//Typ av View

            return View("Create", recipe);//Slipper Göra 2 Typ lika  views
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Recipe recipe)
        {
            //Säkerhet för att inte manipulera Id och ändra på något annat
            //Bör vara samma då det används för att komma till Edit sidan.
            if(id != recipe.Id)
            {
                return NotFound(id);
            }
            if (!ModelState.IsValid)
            {
                ViewBag.IngridientsCount = recipe.Ingredients.Count;
                ViewBag.DirectionsCount = recipe.Directions.Count;
                ViewBag.CreateEdit = "Edit";
                return View("Create", recipe);
            }
           

            _UnitOfWork.Recipe.Update(recipe);
           
            await _UnitOfWork.SaveAsync();
            return RedirectToAction("Index");
        }

        //Get Confirmation Page for Delete
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound(id);
            }

            var recipe = await _UnitOfWork.Recipe.GetDirectionsAndIngredients(id);


            if (recipe == null)
            {
                return NotFound(id);
            }

            return View(recipe);
        }

        // POST: ManagerCategories/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] //Prevent Cross site attacks 
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
             var recipe = await _UnitOfWork.Recipe.GetDirectionsAndIngredients(id);

            if (recipe == null)
            {
                return NotFound(id);
            }

            _UnitOfWork.Recipe.DeleteCascade(recipe);
            await _UnitOfWork.SaveAsync();
            return RedirectToAction("Index");
        }
    }
}
