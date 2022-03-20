using Microsoft.AspNetCore.Mvc;
using RecipeWebsiteMVC.DataAccess.Interfaces;
using RecipeWebsiteMVC.Models.ViewModels;
using System.Security.Claims;

namespace RecipeWebsiteMVC.ViewComponents
{
    /// <summary>
    /// View component för att se om man lagt till i recept eller inte fördel med view component till skillnad från partial view är att det går att ha en model som gör något när det renderas.
    /// Här tycker jag det blir smidigt istället för att lägga en check i controller eller liknande och då behöva använda viewbag eller skapa en ny ViewModel
    /// </summary>
    public class AddToMyListViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _UnitOfWork;
        public AddToMyListViewComponent(IUnitOfWork context) 
        {
            _UnitOfWork = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string RecipeID)
        {
            var getIdentity = (ClaimsIdentity)User.Identity;
            var claim = getIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var ViewModel = new AlreadyInListVM { IsInList = false, recipeID= RecipeID };
            //Borde inte kunna vara noll medtanke på Authorize men men då sickar vi bara tillbaka dig
            if (claim == null)
            {
                return View(ViewModel);//Just assuming here it could be null but doesnt matter if the click on button or not anyway
            }
            var listItem = await _UnitOfWork.UserFavoritRecipe.GetFirstOrDefaultAsync(x => x.ApplicationUserID == claim.Value && x.RecipeID == RecipeID);
            if (listItem == null)
            {
                return View(ViewModel);

            }
            ViewModel.IsInList = true;
            return View(ViewModel);

        }
    }
}
