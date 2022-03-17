using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RecipeWebsiteMVC.Data;
using RecipeWebsiteMVC.DataAccess.Interfaces;
using RecipeWebsiteMVC.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

//0176 grader tecknet
namespace RecipeWebsiteMVC.Controllers
{
    public class MangerRecipeController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;//För ladda upp image
        public MangerRecipeController(IUnitOfWork context, IWebHostEnvironment hostEnvironment = null) //Setting to null only so i dont need to moq this in tests 
        {
            _UnitOfWork = context;
            _hostEnvironment = hostEnvironment; 
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
            Recipe r = new Recipe();    
            ViewBag.CreateEdit = "Create";
            
            
            return View(r);  
        }
        [HttpPost]  
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> Create(Recipe recipe, IFormFile? file = null) //Made it null to not mess with my tests 
        {

            if (!ModelState.IsValid)
            {   
  
                //Typ av View istället för att copy pasta och hålla på att trixa med 2 views som i princip är Lika
                ViewBag.CreateEdit = "Create"; 
                return View(recipe);  
            }
            if(file != null) { //Should always be null in the tests
            ///Database things :)
                string fileName = Guid.NewGuid().ToString();    //Om man laddar upp filer med samma namn kan det skapa problem.
                string upload = Path.Combine(_hostEnvironment.WebRootPath, @"Images\FakeBlobStorage"); //Vart filen ska sparas
                string extension = Path.GetExtension(file.FileName); //Får typ av fil
                using(var fileStream = new FileStream(Path.Combine(upload, fileName+ extension), FileMode.Create))
                {
                    file.CopyTo(fileStream);    
                }
                recipe.Image = @"Images\FakeBlobStorage\" + fileName + extension; //Eftersom jag spara filen i root skulle jag spara I ett riktigt blobstorage bör det vara upload+filename +extension
            }
            _UnitOfWork.Recipe.Add(recipe); 
            await _UnitOfWork.SaveAsync();//UnitOfWork :)
            TempData["Success"] = $"Lyckades skapa : {recipe.Title} :)";

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
           
         
            ViewBag.CreateEdit = "Edit";//Typ av View

            return View("Create", recipe);//Slipper Göra 2 Typ lika  views
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Recipe recipe, IFormFile? file = null)
        {
            //Säkerhet för att inte manipulera Id och ändra på något annat
            //Bör vara samma då det används för att komma till Edit sidan.
            if(id != recipe.Id)
            {
                return NotFound(id);
            }
            
            if (!ModelState.IsValid)
            {
            
                ViewBag.CreateEdit = "Edit";
                return View("Create", recipe);
            }
            //File
            if (file != null)
            { //Should always be null in the tests
                ///Database things :)
                string fileName = Guid.NewGuid().ToString();    //Om man laddar upp filer med samma namn kan det skapa problem.
                string upload = Path.Combine(_hostEnvironment.WebRootPath, @"Images\FakeBlobStorage"); //Vart filen ska sparas
                string extension = Path.GetExtension(file.FileName); //Får typ av fil
                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                recipe.Image = @"Images\FakeBlobStorage\" + fileName + extension; //Eftersom jag spara filen i root skulle jag spara I ett riktigt blobstorage bör det vara upload+filename +extension
            }

            _UnitOfWork.Recipe.Update(recipe);
            //Mindre sanolikt här att det uppkommer för att update fungerar lite anorlunda än Kategorin men det skulle kunna hända så bättre att vara safe :)
            try
            {
                await _UnitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                RedirectToAction("Index");
                TempData["Error"] = $"Misslyckades att updatera : {recipe.Title}";
            }
            TempData["Success"] = $"Lyckades updatera : {recipe.Title} :)";

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
            TempData["Success"] = $"Lyckades ta bort : {recipe.Title} :)";

            return RedirectToAction("Index");
        }
    }
}
