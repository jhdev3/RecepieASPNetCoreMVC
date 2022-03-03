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
            var recipe = new Recipe();
            recipe.Id = "testId";
            var ingredient = new Ingredient { Unit = 2, UnitType = "st", Name = "Tester i ett", RecipeId = recipe.Id };
            var directive = new Direction { Text = "Börja med att skriva ett test Case", RecipeId = recipe.Id };
            var ingredient2 = new Ingredient { Unit = 3, UnitType = "st", Name = "Tester", RecipeId = recipe.Id };
            var directive2 = new Direction { Text = "Testa dit Test Case LOL", RecipeId = recipe.Id };


            //Act
            recipe.Title = "Test";
            recipe.Description = "Testar recipe";
            recipe.Category = "Vegan";
            recipe.Directions.Add(directive);
            recipe.Ingredients.Add(ingredient);
            recipe.Directions.Add(directive2);
            recipe.Ingredients.Add(ingredient2);

            fakeRecipeDb.Add(recipe);

            fakeDirectionDb.AddRange(recipe.Directions);

            fakeIngridientDb.AddRange(recipe.Ingredients);
        }
      


        [Fact]
        public void TestCreatingRecipeAndSave()
        {
            //Arrange
            var recipe = new Recipe();
            var ingredient = new Ingredient { Unit = 2, UnitType = "st", Name = "Tester i ett", RecipeId = recipe.Id };
            var directive = new Direction { Text = "Test2", RecipeId = recipe.Id };


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

        [Fact]
        public void TestRetriveFromDB()
        {
            //Arrange
            var recipe = fakeRecipeDb.Find(i => i.Id == "testId");
            recipe.Directions = fakeDirectionDb.FindAll(fkey => fkey.RecipeId == "testId").ToList();

            recipe.Ingredients = fakeIngridientDb.FindAll(fkey => fkey.RecipeId == "testId").ToList();
            //Act

            int icount = recipe.Ingredients.Count();
            recipe.Title = "Test Retrive From Db";
            int dcount = recipe.Directions.Count();
            recipe.EditedAt = System.DateTime.Now;

            //Assert

            Assert.NotEqual(recipe.Title, "Test");
            Assert.NotNull(recipe.EditedAt);
            Assert.Equal(2, icount);    
            Assert.Equal(2, dcount);    
        }

    }
}
