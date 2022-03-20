using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RecipeWebsiteMVC.Data;
using RecipeWebsiteMVC.DataAccess.Interfaces;
using RecipeWebsiteMVC.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Authorization;
using RecipeWebsiteMVC.Models.UserRoles;
using Microsoft.AspNetCore.Mvc.Rendering;

//0176 grader tecknet
namespace RecipeWebsiteMVC.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{UR.Role_Admin}, {UR.Role_Contributor}")]
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
        public async Task<IActionResult> CreateAsync()
        {
            Recipe r = new Recipe();    
            ViewBag.CreateEdit = "Create";
            ViewBag.SelectCategory = await CategoryListAsync();
            return View(r);  
        }
        [HttpPost]  
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> Create(Recipe recipe, IFormFile? file = null) //Made it null to not mess with my tests 
        {
            //Sätter Ordining Utifrån den ordiningen de skapas i alternativ här är att sätta eget input Field och sätta själv
            if (!ModelState.IsValid)
            {   
  
                //Typ av View istället för att copy pasta och hålla på att trixa med 2 views som i princip är Lika
                ViewBag.CreateEdit = "Create";
                ViewBag.SelectCategory = await CategoryListAsync();

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

            setOrder(recipe);
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
            ViewBag.SelectCategory = await CategoryListAsync();


            return View("Create", recipe);//Slipper Göra 2 Typ lika  views
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Recipe recipe, IFormFile? file = null)
        {
            //Säkerhet för att inte manipulera Id och ändra på något annat
            //Bör vara samma då det används för att komma till Edit sidan.

            if (id != recipe.Id)
            {
                return NotFound(id);
            }


            if (!ModelState.IsValid)
            {
            
                ViewBag.CreateEdit = "Edit";
                ViewBag.SelectCategory = await CategoryListAsync();

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


            setOrder(recipe);
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


        [Authorize(Roles = UR.Role_Admin)]
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
        [Authorize(Roles = UR.Role_Admin)]
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
        [Authorize(Roles = UR.Role_Admin)]
        public async Task<IActionResult> DeleteIngrident(string id)
        {
            var ingrident = await _UnitOfWork.Ingredient.GetAsync(id);

            if (ingrident == null)//säkerhet
            {
                return NotFound(id);
            }

            _UnitOfWork.Ingredient.Remove(ingrident);
            await _UnitOfWork.SaveAsync();
            TempData["Success"] = $"Lyckades ta bort Ingrdiens";
            return RedirectToAction("Index");

        }
        [Authorize(Roles = UR.Role_Admin)]
        public async Task<IActionResult> DeleteDirection(string id)
        {
            var direction = await _UnitOfWork.Direction.GetAsync(id);

            if (direction == null)//säkerhet
            {
                return NotFound(id);
            }

                _UnitOfWork.Direction.Remove(direction);
                await _UnitOfWork.SaveAsync();
                TempData["Success"] = $"Lyckades ta bort Instruktionen";
            return RedirectToAction("Index"); //Sickar tillbaka till start sidan eftersom det är känsligt att ta bort samt flera som ändrar / tar bort i recept kan det uppstå många concurrency så att göra det ett i taget kan undvika lite fel

        }



        #region private functions
        //Skulle kunna göra en RecipeVM osv istället för viewBag för att få ListItems gör så här istället
        private async Task<IEnumerable<SelectListItem>> CategoryListAsync()
        {
            var Categories = await _UnitOfWork.Category.GetAllAsync();
            
            var selctListitems = Categories.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Name //Väljer Name och inte Id för borde inte finnas fler categorier med samma namn Samt sätt name som Key i databasen men då can jag få Concurrency errors även när jag lägger till Kategori det vill jag inte ha
            });
            return selctListitems;  
        } 

        private void setOrder(Recipe recipe)
        {
            for (int i = 0; i < recipe.Ingredients.Count; i++)
            {
                recipe.Ingredients[i].DisplayOrder = i;
            }
            for (int i = 0; i < recipe.Directions.Count; i++)
            {
                recipe.Directions[i].DisplayOrder = i;
            }
        }
        #endregion
    }
}
