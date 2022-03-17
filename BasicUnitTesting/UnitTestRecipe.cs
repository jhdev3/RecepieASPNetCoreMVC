using Xunit;
using RecipeWebsiteMVC.Models;
using System.Collections.Generic;
using System.Linq;
using RecipeWebsiteMVC.Controllers;
using Moq;
using RecipeWebsiteMVC.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BasicUnitTesting
{

    public class UnitTestRecipe
    {
        private Mock<IRecipeRepository> RecipeRepo;
        private List<Recipe> _Recipes;
        private Mock<IUnitOfWork> UnitiOfWork;

        public UnitTestRecipe()
        {

            RecipeRepo = new Mock<IRecipeRepository>();
            _Recipes = new List<Recipe>();
            UnitiOfWork = new Mock<IUnitOfWork>();

        }
        #region index
        /*Index */
        [Fact]
        public async void TestRecipe_Index_EmptyList()
        {
            //Arrange
            RecipeRepo.Setup(c => c.GetAllAsync()).ReturnsAsync(_Recipes);
            UnitiOfWork.Setup(u => u.Recipe).Returns(RecipeRepo.Object);
            MangerRecipeController Mrc = new MangerRecipeController(UnitiOfWork.Object);
            //Act
            var actionResult = await Mrc.Index();
            ViewResult viewResult = actionResult as ViewResult;
            var resultat = viewResult.ViewData.Model as IEnumerable<Recipe>;
            //Assert
            Assert.Equal(0, resultat.Count());
        }
        [Fact]
        public async void TestRecipe_Index_NotEmptyList()
        {
            //Arrange
            _Recipes.Add(new Recipe());
            _Recipes.Add(new Recipe { });
            _Recipes.AsQueryable();
            RecipeRepo.Setup(c => c.GetAllAsync()).ReturnsAsync(_Recipes);
            UnitiOfWork.Setup(u => u.Recipe).Returns(RecipeRepo.Object);
            MangerRecipeController Mrc = new MangerRecipeController(UnitiOfWork.Object);
            //Act
            var actionResult = await Mrc.Index();
            ViewResult viewResult = actionResult as ViewResult;
            var resultat = viewResult.ViewData.Model as IEnumerable<Recipe>;


            //Assert
            Assert.Equal(2, resultat.Count());
        }
        #endregion

        #region Create
        /*Create Post */

        //Todo Get Create om det finns tid och lust
        [Fact]
        public async void TestCreateRecipe_Fail()
        {
            //Arrange
            MangerRecipeController Mrc = new MangerRecipeController(UnitiOfWork.Object);
            Mrc.ModelState.AddModelError("Title", "Required");
            Mrc.ModelState.AddModelError("Portions", "Required");

            //Act
            var model = new Recipe
            {
                Title = null,
                Portions = 0,
            };
            //Indebug mode lets check the count ;)


            var actionResult = await Mrc.Create(model);
            ViewResult viewResult = actionResult as ViewResult;
            var resultat = viewResult.ViewData.ModelState;

          
            // Assert
            Assert.False(resultat.IsValid);
        }
        [Fact]
        public async void TestCreateRecipe_Working()
        {
            //Arrange
            RecipeRepo.Setup(c => c.Add(It.IsAny<Recipe>()))
            .Verifiable();
            UnitiOfWork.Setup(c => c.Recipe).Returns(RecipeRepo.Object);
            MangerRecipeController Mrc = new MangerRecipeController(UnitiOfWork.Object);
            var mockTempData = new Mock<ITempDataDictionary>();
            Mrc.TempData = mockTempData.Object;

            //Act
            var model = new Recipe
            {
                Title = "Testar",
                Description = "Att Skapa ett recept",
                Portions = 4,            
            };
            model.Directions.Add(new Direction { Text = "Steg1: Lär dig Mock" });
            model.Ingredients.Add(new Ingredient { Unit = 2, UnitType = "tsk", Name = "Salt" });
            var actionResult = await Mrc.Create(model);


            // Assert - Kollar om jag blev Redirected och till samma controller samt att det är index.
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(actionResult);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            UnitiOfWork.Verify();//Kollar så att Add Gjordes :)
        }
        #endregion

        #region Details
        /*Todo Details  */
        [Theory]
        [InlineData((string)null)]//If id is null
        [InlineData("ABC123")]//If db cant find the Category object
        public async void TestGetDetails_Recipe_Fail(string? id)
        {

            //Arrange
            RecipeRepo.Setup(c => c.GetDirectionsAndIngredients(id)).ReturnsAsync((Recipe)null);//Test if category is null
            UnitiOfWork.Setup(u => u.Recipe).Returns(RecipeRepo.Object);
            MangerRecipeController Mrc = new MangerRecipeController(UnitiOfWork.Object);
            //Act
            var result = Mrc.Details(id).Result; //<-- CAST HERE

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(id, notFoundObjectResult.Value);

        }

        public async void TestGetDetails_RecipeSucces()
        {
            string id = "123Find";
            Recipe recipe = new Recipe { Id = id, Title = "TestGet", Category= "Wok", Description="Testar Details :)", 
                                         Image="../aBasd.png", Portions=4};
            recipe.Directions.Add(new Direction { Text = "Många Tests :), Gjorde dem innan jag börjar ändra allt för mycket i model etc för att upptäcka fel" });
            recipe.Ingredients.Add(new Ingredient { Unit = 2.5, UnitType = "dl", Name = "Socker" });

            RecipeRepo.Setup(c => c.GetDirectionsAndIngredients(id)).ReturnsAsync(recipe);//Test if category is null
            UnitiOfWork.Setup(u => u.Recipe).Returns(RecipeRepo.Object);
            MangerRecipeController Mrc = new MangerRecipeController(UnitiOfWork.Object);
            //Act
            var result = Mrc.Edit(id).Result; //Check during debugging test and wanted just result

            // Assert
            var FoundObjectResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(recipe, FoundObjectResult.Model);
        }
        #endregion

        #region Edit
        /*EditGet */

        [Theory]
        [InlineData((string)null)]//If id is null
        [InlineData("ABC123")]//If db cant find the Category object
        public async void TestGetEdit_Recipe_Fail(string? id)
        {

            //Arrange
            RecipeRepo.Setup(c => c.GetDirectionsAndIngredients(id)).ReturnsAsync((Recipe)null);//Test if category is null
            UnitiOfWork.Setup(u => u.Recipe).Returns(RecipeRepo.Object);
            MangerRecipeController Mrc = new MangerRecipeController(UnitiOfWork.Object);
            //Act
            var result = Mrc.Edit(id).Result; //<-- CAST HERE

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(id, notFoundObjectResult.Value);

        }
        [Fact]
        public async void TestGet_EditRecipe_SuccesReturnView()
        {
            //Arrange
            string id = "123Find";
            Recipe recipe = new Recipe { Id = id, Title = "TestGet" };
            RecipeRepo.Setup(c => c.GetDirectionsAndIngredients(id)).ReturnsAsync(recipe);//Test if category is null
            UnitiOfWork.Setup(u => u.Recipe).Returns(RecipeRepo.Object);
            MangerRecipeController Mrc = new MangerRecipeController(UnitiOfWork.Object);
            //Act
            var result = Mrc.Edit(id).Result; //Check during debugging test and wanted just result

            // Assert
            var FoundObjectResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(recipe, FoundObjectResult.Model);

        }


        /*EditPost */
        [Fact]
        public async void TestPost_EditRecipe_FailIdsNotMatching()
        {
            //Arrange
            string id = "123";
            Recipe recipe = new Recipe { Id = "456", Title = "TestGet" };

            MangerRecipeController Mrc = new MangerRecipeController(UnitiOfWork.Object);
            //Act
            var result = Mrc.Edit(id, recipe).Result;

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(id, notFoundObjectResult.Value);

        }
        [Fact]
        public async void TestPost_EditRecipe_FailModelState()
        {
            //Arrange
            string id = "123";
            Recipe recipe = new Recipe { Id = "123", Title = null };

            MangerRecipeController Mrc = new MangerRecipeController(UnitiOfWork.Object);
            Mrc.ModelState.AddModelError("Title", "Required"); //Injecting a modelstate error :)
            //Act
            var result = Mrc.Edit(id, recipe).Result;

            // Assert
            var ReturnCreateView = Assert.IsType<ViewResult>(result);
            Assert.Equal(recipe, ReturnCreateView.Model);
            Assert.Equal("Create", ReturnCreateView.ViewName);
        }
        [Fact]
        public async void TestPost_EditRecipe_Success()
        {
            //Arrange
            string id = "123";
            Recipe recipe = new Recipe { Id = "123", Title = "TestGet" };
            RecipeRepo.Setup(c => c.Update(It.IsAny<Recipe>()))
           .Verifiable();
            UnitiOfWork.Setup(c => c.Recipe).Returns(RecipeRepo.Object);
            MangerRecipeController Mrc = new MangerRecipeController(UnitiOfWork.Object);
            var mockTempData = new Mock<ITempDataDictionary>();
            Mrc.TempData = mockTempData.Object;
            //Act
            var actionResult = await Mrc.Edit(id, recipe);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(actionResult);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            UnitiOfWork.Verify();//Kollar så att Update Gjordes :)
        }
        #endregion

        #region Delete
        /*Delete */
        /*Get */

        [Theory]
        [InlineData((string)null)]//If id is null
        [InlineData("ABC123")]//If db cant find the Category object
        public void TestGet_DeleteRecipe_Fail(string? id)
        {
            //Arrange
            RecipeRepo.Setup(c => c.GetDirectionsAndIngredients(id)).ReturnsAsync((Recipe)null);
            UnitiOfWork.Setup(u => u.Recipe).Returns(RecipeRepo.Object);
            MangerRecipeController Mrc = new MangerRecipeController(UnitiOfWork.Object);
            //Act
            var result = Mrc.Delete(id).Result;

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(id, notFoundObjectResult.Value);

        }
        [Fact]
        public void TestGet_DeleteRecipe_returnView()
        {
            //Arrange
            string id = "123Find";
            Recipe recipe = new Recipe { Id = id, Title = "TestGetDelete" };
            RecipeRepo.Setup(c => c.GetDirectionsAndIngredients(id)).ReturnsAsync(recipe);

            UnitiOfWork.Setup(u => u.Recipe).Returns(RecipeRepo.Object);
            MangerRecipeController Mrc = new MangerRecipeController(UnitiOfWork.Object);
            //Act
            var result = Mrc.Delete(id).Result; //Check during debugging test and wanted just result

            // Assert
            var FoundObjectResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(recipe, FoundObjectResult.Model);

        }
        ///*Delete Confirmed */
        //When deleting a recipe using cascade so good check is to manuly check db aswell.
        [Fact]
        public void TestDeleteRecipeConfirmed_Fail()
        {
            string id = "123";
            //Arrange
            RecipeRepo.Setup(c => c.GetDirectionsAndIngredients(id)).ReturnsAsync((Recipe)null);//Test if category is null
            UnitiOfWork.Setup(u => u.Recipe).Returns(RecipeRepo.Object);
            MangerRecipeController Mrc = new MangerRecipeController(UnitiOfWork.Object);
            //Act
            var result = Mrc.DeleteConfirmed(id).Result;

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(id, notFoundObjectResult.Value);

        }
        [Fact]
        public async void TesDeleteConfirmRecipe_SuccesRedirectToIndex()
        {
            //Arrange
            string id = "123Find";
            Recipe recipe = new Recipe { Id = id, Title = "TestGet" };
            RecipeRepo.Setup(c => c.GetDirectionsAndIngredients(id)).ReturnsAsync(recipe);//Test if category is null
            RecipeRepo.Setup(c => c.Remove(It.IsAny<Recipe>())).Verifiable();
            UnitiOfWork.Setup(u => u.Recipe).Returns(RecipeRepo.Object);
            MangerRecipeController Mrc = new MangerRecipeController(UnitiOfWork.Object);
            var mockTempData = new Mock<ITempDataDictionary>();
            Mrc.TempData = mockTempData.Object;
            //Act
            var actionResult = await Mrc.DeleteConfirmed(id); //Check during debugging test and wanted just result

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(actionResult);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            UnitiOfWork.Verify();//Kollar så att Remove Gjordes :)

        }
        #endregion


    }
}
