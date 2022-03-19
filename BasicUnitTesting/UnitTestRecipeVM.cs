using Xunit;
using RecipeWebsiteMVC.Models;
using System.Collections.Generic;
using System.Linq;
using RecipeWebsiteMVC.Models.ViewModels;

namespace BasicUnitTesting
{
  
    public class UnitTestRecipeVM 
    {
        public List<Ingredient> data;
        public UnitTestRecipeVM()
        {
            data = new List<Ingredient>{
                new Ingredient  { Unit = 2, UnitType = "st", Name = "Bananer"},
                new Ingredient  { Unit = 2, UnitType = "dl", Name = "vatten"},
                new Ingredient  { Unit = null, UnitType = "st", Name = "Under rubrik"},
                new Ingredient  { Unit = 0.5, UnitType = "kg", Name = "Köttfärs"},
                new Ingredient  { Unit = 2.5, UnitType = "st", Name = "Chili"},
            };
        }
        [Theory]
        [InlineData(4, 1)]
        [InlineData(8, 2)]
        [InlineData(-2, 0.5)]
        [InlineData(3, 0.75)]
        public void TestUpdate_Ingridients(int multi, double res)
        {
            //Arrange
            var r = new Recipe { Portions = 4, Ingredients = data};
           RecipeVM rvm = new RecipeVM(r, multi);

            //Act
            rvm.UpdateIngridiens();

            foreach(var u in data)
            {
                if (u.Unit != null) {
                    u.Unit *= res;
                 }       
            }

            //Assert
            for(int i= 0; i < rvm.recipe.Ingredients.Count; i++) { 
                if(i == 2)
                {
                    Assert.Null(rvm.recipe.Ingredients[i].Unit);
                }
                else { 
                Assert.Equal(data[i].Unit, rvm.recipe.Ingredients[i].Unit);
                }
            }
            }

    }
}
