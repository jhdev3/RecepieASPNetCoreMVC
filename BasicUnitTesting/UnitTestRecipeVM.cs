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
       
        [Theory]
        [InlineData(4, 1)]
        [InlineData(8, 2)]
        [InlineData(-2, 0.5)]
        [InlineData(3, 0.75)]
        public void TestUpdate_Ingridients_Amount(int multi, double res)
        {
            data = new List<Ingredient>{
                new Ingredient  { Unit = 2, UnitType = " ", Name = "Bananer"},
                new Ingredient  { Unit = 2, UnitType = " ", Name = "vatten"},
                new Ingredient  { Unit = null, UnitType = " ", Name = "Under rubrik"},
                new Ingredient  { Unit = 0.5, UnitType = " ", Name = "Köttfärs"},
                new Ingredient  { Unit = 2.5, UnitType = " ", Name = "Chili"},
            };
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

        [Fact]
        public void TestRecipeVM_UnitTypesCollection()
        {
            //Arrange
            data = new List<Ingredient>{
                new Ingredient  { Unit = 0.5, UnitType = "kg", Name = "Köttfärs"},
                new Ingredient  { Unit = 2000, UnitType = "g", Name = "Kyckling"},
            };
            var r = new Recipe { Portions = 4, Ingredients = data };
            RecipeVM rvm = new RecipeVM(r, 4);

            //Act
            rvm.UpdateIngridiens();
            //Assert
            Assert.Equal("g", r.Ingredients[0].UnitType);
            Assert.Equal(500, r.Ingredients[0].Unit);
            Assert.Equal("kg", r.Ingredients[1].UnitType);
            Assert.Equal(2, r.Ingredients[1].Unit);
        }
    }
}
