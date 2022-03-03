using System.Collections.Generic;

namespace RecipeWebsiteMVC.Models.dynamicField
{
    public class IngredientsViewModel
    {
      public List<Ingredient> ingredients { get; set; }

      public IngredientsViewModel()
        {
            ingredients = new List<Ingredient>();   
        }

        public IEnumerator<Ingredient> GetEnumerator()
        {
           
            return ingredients.GetEnumerator();
        }
       
        //Behöver för att databasen ska känna igen object typen. dvs Ingredient
        public List<Ingredient> returningredientsList()
        {
            return ingredients; 
        }
    }
}
