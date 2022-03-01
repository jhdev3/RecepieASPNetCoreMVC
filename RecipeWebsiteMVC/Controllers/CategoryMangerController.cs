using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeWebsiteMVC.Cache;
using RecipeWebsiteMVC.Models;

namespace RecipeWebsiteMVC.Controllers
{
    public class CategoryMangerController : Controller
    {
        RepoCache<Category> CategoryRepo;

        public CategoryMangerController()
        {
            CategoryRepo = new RepoCache<Category>();       
        }
        // GET: CategoryMangerController and listing details
        public IActionResult Index()
        {
            List<Category> categories = CategoryRepo.Collection().ToList(); 
            return View(categories);
        }

        // GET: CategoryMangerController/Create
        public IActionResult Create()
        {
            Category category = new Category(); 
            return View(category);
        }

        // POST: CategoryMangerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category c)
        {
            if (!ModelState.IsValid)
            {
                return View(c);
            }
            else
            {
                CategoryRepo.Insert(c);

                return RedirectToAction("Index");
            }
        }

        // GET: CategoryMangerController/Edit/5
        public IActionResult Edit(string id)
        {
            return View();
        }

        // POST: CategoryMangerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryMangerController/Delete/5
        public IActionResult Delete(string id)
        {
            return View();
        }

        // POST: CategoryMangerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
