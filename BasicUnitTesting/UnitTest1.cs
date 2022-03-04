using Xunit;
using RecipeWebsiteMVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace BasicUnitTesting
{
    /// <summary>
    /// Basic Unit Test to test som of the models and the behviours. 
    /// Was a bit waste of time will do the same by mocking but in the begining
    /// it was nice to have some tests to check the model behviours.
    /// </summary>
    public class UnitTest1 
    {
        /// <summary>
        /// Pupulating the Fakes db during the run of this ClassTest ;)
        /// </summary>
        public List<Recipe> fakeRecipeDb = new List<Recipe>();
        public List<Ingredient> fakeIngridientDb = new List<Ingredient>();
        public List<Direction> fakeDirectionDb = new List<Direction>();

        public UnitTest1()
        {
  
        }
      


        [Fact]
        public void TestCreatingRecipeAndSave()
        {
            //Arrange
            var recipe = new Recipe();
            var ingredient = new Ingredient { Unit = 2, UnitType = "st", Name = "Tester i ett"};
            var directive = new Direction { Text = "Test2"};


            //Act
            recipe.Title = "Test";
            recipe.Description = "Testar recipe";
            recipe.Category = "Vegan";
            recipe.Directions.Add(directive);
            recipe.Ingredients.Add(ingredient);

            fakeRecipeDb.Add(recipe);
            fakeDirectionDb.Add(directive);
            fakeIngridientDb.Add(ingredient);

            //Assert
            Assert.NotNull(recipe.Id);
            Assert.NotNull(recipe.DateOfCreation.ToString());
            Assert.Contains(ingredient, recipe.Ingredients);
            Assert.Equal("Test2", recipe.Directions[0].Text);
        }

    }
}
