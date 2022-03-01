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
            Recipe.AddDirection(new Direction { Text = "B�rja med att skriva ett test Case", RecipeId=Recipe.Id });
            var Ingredient = new Ingredient { Unit = 2, UnitType = "st", Name = "Tester i ett", RecipeId=Recipe.Id };
            Recipe.AddIngredient(Ingredient);

            //Assert
            Assert.NotNull(Recipe.Id);
            Assert.NotNull(Recipe.DateOfCreation.ToString());
            Assert.Contains(Ingredient, Recipe.Ingredients);
            Assert.Equal("B�rja med att skriva ett test Case", Recipe.Directions[0].Text);

        }
    }
}