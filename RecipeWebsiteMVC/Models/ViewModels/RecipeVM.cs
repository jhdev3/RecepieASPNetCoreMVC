using RecipeWebsiteMVC.Models.UnitTypeConvert;
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
        /// <summary>
        /// Multiplier = portions
        /// </summary>
        /// <param name="r"></param>
        public RecipeVM(Recipe r) 
        {
            Multiplier = r.Portions;
            recipe = r;
        }
        /// <summary>
        /// Setting multiplier
        /// </summary>
        /// <param name="r"></param>
        /// <param name="m"></param>
        public RecipeVM(Recipe r, int m)
        {
            Multiplier = m;
            recipe = r;
        }
        /// <summary>
        /// Updaterar Ingridienserna ;)
        /// </summary>
        public void UpdateIngridiens()
        {
            var unitTypeConvert = new UnitTypesCollection();

            if (recipe.Ingredients.Count > 0)
            {
                foreach(var ingridient in recipe.Ingredients)
                {
                    if(ingridient.Unit != null && recipe.Portions != 0) //Borde vara större än 0 men en försiktighetåtgärd
                    {
                        ingridient.Unit *= (double)(Multiplier / (double)recipe.Portions); 

                        /*Det är här aktuellt att ändra enhet-Skulle kunna göra utanför if satsen men det blir bara aktuellt om någon fyllt i recept på ett konstigt sätt */

                        unitTypeConvert.Convert(ingridient);    

                    }
                }  
            }
        }
    }
}
