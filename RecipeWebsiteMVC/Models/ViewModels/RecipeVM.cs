using System.ComponentModel.DataAnnotations;

namespace RecipeWebsiteMVC.Models.ViewModels
{
    /// <summary>
    /// Model For listing Recipes
    /// </summary>
    public class RecipeVM
    {
        [Required]
        [Range(1, 100)]
        [Display(Name = "Portioner")]
        public int Multiplier { get; set; }    
        public Recipe recipe { get; set; }  

        public void UpdateIngridiens()
        {
            if(recipe.Ingredients.Count > 0)
            {
                foreach(var ingridient in recipe.Ingredients)
                {
                    if(ingridient.Unit != null && recipe.Portions != 0) //Borde vara större än 0 men en försiktighetåtgärd
                    {
                        ingridient.Unit *= (double)(Multiplier / (double)recipe.Portions); 

                        /*Todo Featuer Unit type converter också det blir många etc */
                    }
                }  
            }
        }
    }
}
