#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeWebsiteMVC.Data;
using RecipeWebsiteMVC.Models;

namespace RecipeWebsiteMVC.Controllers
{
    public class ManagerCategoriesController : Controller
    {
        //Viktigt då DbContexte inte är thread-safe
        private readonly AppDbContext _dBcontext;

        public ManagerCategoriesController(AppDbContext context)
        {
            _dBcontext = context;
        }

        // GET: A list of all Categories and a way to mange them
        public async Task<IActionResult> Index()//krävs en Async metod för att kunna använda await vilket är viktigt/ ett måste om man använder sig av Aync metoder  
        {
            //Eftersom jag här använder Async för Iaction result och jag väntar på min databas att hämta hela DBSet / Tabellen
            //Används Async Metoderna är det super viktigt att programmet väntar in med Await :)
            return View(await _dBcontext.Categories.ToListAsync());
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
                _dBcontext.Add(category);
                await _dBcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: ManagerCategories/Edit/
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _dBcontext.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
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
                return NotFound(); //StatusKod 404 response tillbaka
            }

            if (ModelState.IsValid)
            {
                try
                {
                    category.EditedAt = DateTime.Now;
                    _dBcontext.Update(category);
                    await _dBcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: ManagerCategories/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _dBcontext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: ManagerCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] //Prevent Cross site attacks 
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var category = await _dBcontext.Categories.FindAsync(id);
            if(category == null)
            {
                return NotFound();
            }   
            _dBcontext.Categories.Remove(category);
            await _dBcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(string id)
        {
            return _dBcontext.Categories.Any(e => e.Id == id);
        }
    }
}
