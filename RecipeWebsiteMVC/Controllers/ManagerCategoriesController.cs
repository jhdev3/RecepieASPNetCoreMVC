#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeWebsiteMVC.Data;
using RecipeWebsiteMVC.DataAccess.Interfaces;
using RecipeWebsiteMVC.Models;

namespace RecipeWebsiteMVC.Controllers
{
    public class ManagerCategoriesController : Controller
    {
        //Viktigt med private och readonly Readonly då DbContext inte är thread-safe, IE trådar har tillgång till appens "Data"  
        private readonly IUnitOfWork _UnitOfWork;


        public ManagerCategoriesController(IUnitOfWork context)
        {
            _UnitOfWork = context;
        }


        // GET: A list of all Categories and a way to mange them
        public async Task<IActionResult> Index()//krävs en Async metod för att kunna använda await vilket är viktigt/ ett måste om man använder sig av Aync metoder  
        {
            //Eftersom jag här använder Async för Iaction result och jag väntar på min databas att hämta hela DBSet / Tabellen
            //Används Async Metoderna är det super viktigt att programmet väntar in med Await :)
            var Categories = await _UnitOfWork.Category.GetAllAsync();
            return View(Categories);
        }

        // GET: ManagerCategories/Create View med formulär för create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ManagerCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.Category.Add(category);
                await _UnitOfWork.SaveAsync();

                TempData["Success"] = $"Lyckades skapa : {category.Name} :)";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: ManagerCategories/Edit/
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound(id);
            }

            var category = await _UnitOfWork.Category.GetAsync(id);
           
            if (category == null)
            {
                return NotFound(id);
            }

            return View(category);
        }

        // POST: ManagerCategories/Edit/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Category category)
        {
            //En extra säkerhet att inte bli manipulerad eller att Id har ändrats 
            if (id != category.Id)
            {
                return NotFound(id); //StatusKod 404 response tillbaka
            }

            if (ModelState.IsValid)
            {
                category.EditedAt = DateTime.Now;
                _UnitOfWork.Category.Update(category);
                //Concurrency exeptions -Kan uppkomma om du ändra något som någon annan tagit bort eller på samma objekt/etc
                try
                {
                    await _UnitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    RedirectToAction("Index");
                    TempData["Error"] = $"Misslyckades att updatera : {category.Name}"; //ex.Message om man vill se hela trevliga meddelandet eller spara till någon logg fil om man har det.
                }
                TempData["Success"] = $"Lyckades updatera : {category.Name} :)";

                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: ManagerCategories/Delete/Confirmation page 
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound(id);
            }

            var category = await _UnitOfWork.Category.GetFirstOrDefaultAsync(c => c.Id == id); 

            if (category == null)
            {
                return NotFound(id);
            }

            return View(category);
        }

        // POST: ManagerCategories/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] //Prevent Cross site attacks 
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var category = await _UnitOfWork.Category.GetAsync(id);
            if (category == null)
            {
                return NotFound(id);
            }
            _UnitOfWork.Category.Remove(category);
            await _UnitOfWork.SaveAsync();
            TempData["Success"] = $"Lyckades Ta bort : {category.Name} :)";

            return RedirectToAction(nameof(Index));
        }


    }
}
