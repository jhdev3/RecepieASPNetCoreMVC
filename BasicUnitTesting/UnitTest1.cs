using Xunit;
using RecipeWebsiteMVC.Models;

namespace BasicUnitTesting
{
    public class UnitTest1
    {
        [Fact]
        public void TestCreatingRecipeAndIngredient()
        {
            //Arrange
            var Recipe = new Recipe();
            Recipe.Title = "Test";
            Recipe.Description = "Testar recipe";
            Recipe.Category = "Vegan";


            //Act
            Recipe.AddInstruktion("Börja med att skriva ett test Case");
            var Ingredient = new Ingredient { Unit = 2, UnitType = "st", Name = "Tester i ett" };
            Recipe.AddIngredient(Ingredient);

            //Assert
            Assert.NotNull(Recipe.Id);
            Assert.NotNull(Recipe.DateOfCreation.ToString());
            Assert.Contains(Ingredient, Recipe.Ingredients);
            Assert.Equal("Börja med att skriva ett test Case", Recipe.Instruktions[0]);

        }
    }
}