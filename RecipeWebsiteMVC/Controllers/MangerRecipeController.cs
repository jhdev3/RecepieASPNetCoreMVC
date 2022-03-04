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
            return View();  
        }
        [HttpPost]  
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> Create(Recipe recipe)
        {
            //Lägg till databas relation med recept och ingridiens;) 
            //foreach(var i in recipe.Ingredients)
            //{
            //    i.RecipeId = recipe.Id; 
            //}

            if (!ModelState.IsValid)
            {
                return View(recipe);  
            }
            ///Database things :)
            _dBcontext.Add(recipe); 
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
            Recipe recipe = await _dBcontext.Recipes.FirstOrDefaultAsync(r => r.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Recipe recipe)
        {
            //Säkerhet för att inte manipulera Id och ändra på något annat
            // Bör vara samma då det används för att komma till Edit sidan.
            if(id != recipe.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(recipe);
            }
            ///Database things :)-> Kan behöva lägga det här i try block för att säkerställa ändring. med update och save
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

        // POST: ManagerCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] //Prevent Cross site attacks 
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var recipe =  _dBcontext.Recipes
                   .Where(r => r.Id == id)
                   .Include(i => i.Ingredients)
                   .Include(d => d.Directions)
                   .FirstOrDefault();
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
