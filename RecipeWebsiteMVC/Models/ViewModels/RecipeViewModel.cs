using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecipeWebsiteMVC.Models.ViewModels
{
    public class RecipeViewModel
    {
       public Recipe recipe { get; set; }   
       public IEnumerable<SelectListItem> CategoryList { get; set; }    

       public int RecipeCount { get; set; }
       public int DirectionsCount { get; set; }
       public string CreateEdit { get; set; }   


    }
}
