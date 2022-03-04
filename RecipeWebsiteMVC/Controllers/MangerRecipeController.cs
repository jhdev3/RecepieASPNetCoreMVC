using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeWebsiteMVC.Data;
using RecipeWebsiteMVC.Models;

namespace RecipeWebsiteMVC.Controllers
{
    public class MangerRecipeController : Controller
    {
        private readonly AppDbContext _dBcontext;

        public MangerRecipeController(AppDbContext context)
        {
            _dBcontext = context;
        }
        //MVC 4: Men intressant hur man kan använda sig av Performing Multiple Operations in Parallel

        //https://docs.microsoft.com/en-us/aspnet/mvc/overview/performance/using-asynchronous-methods-in-aspnet-mvc-4

        //Visar en enkel lista på alla recept ;) 
        //Get Request
        public async Task<IActionResult> Index()
        {
            var recipies = await _dBcontext.Recipes.ToListAsync();
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
        public async Task<IActionResult> Create(Recipe recipe)
        {

            if (!ModelState.IsValid)
            {   
                //If Statement in View ;)
                ViewBag.IngridientsCount = recipe.Ingredients.Count;
                ViewBag.DirectionsCount = recipe.Directions.Count;
                //Typ av View istället för att copy pasta och hålla på att trixa med 2 views som i princip är Lika
                ViewBag.CreateEdit = "Create"; 
                return View(recipe);  
            }
            ///Database things :)
            _dBcontext.Recipes.Add(recipe); 
            //_dBcontext.Ingredients.AddRange(recipe.Ingredients);

            await _dBcontext.SaveChangesAsync();//UnitOfWork :)
            return RedirectToAction("Index");   
        }
        public async Task<IActionResult> Details(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            Recipe recipe = await _dBcontext.Recipes.FirstOrDefaultAsync(r => r.Id == id);
            if(recipe == null)
            {
                return NotFound();
            }
            return View(recipe);    
        }
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var recipe = await _dBcontext.Recipes
                 .Where(r => r.Id == id)
                 .Include(i => i.Ingredients)
                 .Include(d => d.Directions)
                 .FirstOrDefaultAsync();

            if (recipe == null)
            {
                return NotFound();
            }
           
            if (recipe == null)
            {
                return NotFound();
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
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                ViewBag.IngridientsCount = recipe.Ingredients.Count;
                ViewBag.DirectionsCount = recipe.Directions.Count;
                ViewBag.CreateEdit = "Edit";
                return View("Create", recipe);
            }
            ///Database things :)-> Kan behöva lägga det här i try block för att säkerställa ändring. med update och save
            ///Här skulle 2 kunna ändra samtidigt och då få allt att Krasha. 
            recipe.EditedAt = DateTime.Now;

            _dBcontext.Update(recipe);
           
            await _dBcontext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //Get Confirmation Page for Delete
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _dBcontext.Recipes
                .FirstOrDefaultAsync(r => r.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: ManagerCategories/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] //Prevent Cross site attacks 
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var recipe =  await _dBcontext.Recipes
                   .Where(r => r.Id == id)
                   .Include(i => i.Ingredients)
                   .Include(d => d.Directions)
                   .FirstOrDefaultAsync();
            if (recipe == null)
            {
                return NotFound();
            }

            _dBcontext.Directions.RemoveRange(recipe.Directions);
            _dBcontext.Ingredients.RemoveRange(recipe.Ingredients);
            _dBcontext.Recipes.Remove(recipe);
            await _dBcontext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
