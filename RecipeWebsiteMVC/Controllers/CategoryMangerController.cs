using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RecipeWebsiteMVC.Controllers
{
    public class CategoryMangerController : Controller
    {
        // GET: CategoryMangerController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CategoryMangerController/Details/5
        public ActionResult Details(string id)
        {
            return View();
        }

        // GET: CategoryMangerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryMangerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: CategoryMangerController/Edit/5
        public ActionResult Edit(string id)
        {
            return View();
        }

        // POST: CategoryMangerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
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
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: CategoryMangerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
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
