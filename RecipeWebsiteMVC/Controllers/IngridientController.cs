using Microsoft.AspNetCore.Mvc;
using RecipeWebsiteMVC.Data;
using RecipeWebsiteMVC.Models;
using RecipeWebsiteMVC.Models.dynamicField;

namespace RecipeWebsiteMVC.Controllers
{
    public class IngridientController : Controller
    {
        private readonly AppDbContext _dBcontext;
        public IngridientController(AppDbContext context)
        {
            _dBcontext = context;
        }
      
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(IngredientsViewModel i)
        {
            ModelState.Remove("RecipeId ");

            if (!ModelState.IsValid)
            {
                return View(i);
            }

            List<Ingredient> ingredients = i.returningredientsList();         
             _dBcontext.Ingredients.AddRange(ingredients); 
            _dBcontext.SaveChanges();   
            return View(); 
        }
    }
}
