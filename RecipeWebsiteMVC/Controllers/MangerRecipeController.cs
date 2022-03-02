using Microsoft.AspNetCore.Mvc;

namespace RecipeWebsiteMVC.Controllers
{
    public class MangerRecipeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
