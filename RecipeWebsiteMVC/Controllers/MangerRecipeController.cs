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
        public IActionResult Create(string id)
        {
            return View();  
        }
        [HttpPost]  
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> Create(Recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return View(recipe);  
            }
            ///Database things :)
            _dBcontext.Add(recipe);
            await _dBcontext.SaveChangesAsync();
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
    }
}
