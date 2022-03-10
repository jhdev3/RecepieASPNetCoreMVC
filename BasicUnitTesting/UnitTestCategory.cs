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

namespace BasicUnitTesting
{

    public class UnitTestCategory
    {
        private Mock<ICategoryRepo> CategoryRepo;
        private List<Category> _categories;
        private Mock<IUnitOfWork> UnitiOfWork;

        public UnitTestCategory()
        {

            CategoryRepo = new Mock<ICategoryRepo>();
            _categories = new List<Category>();
            UnitiOfWork = new Mock<IUnitOfWork>();

        }
        #region index
        /*Index */
        [Fact]
        public async void TestCategory_Index_EmptyList()
        {
            //Arrange
            CategoryRepo.Setup(c => c.GetAllAsync()).ReturnsAsync(_categories);
            UnitiOfWork.Setup(u => u.Category).Returns(CategoryRepo.Object);
            ManagerCategoriesController Mcc = new ManagerCategoriesController(UnitiOfWork.Object);
            //Act
            var actionResult = await Mcc.Index();
            ViewResult viewResult = actionResult as ViewResult;
            var resultat = viewResult.ViewData.Model as IEnumerable<Category>;
            //Assert
            Assert.Equal(0, resultat.Count());
        }
        [Fact]
        public async void TestCategory_Index_NotEmptyList()
        {
            //Arrange
            _categories.Add(new Category());
            _categories.Add(new Category { });
            _categories.AsQueryable();
            CategoryRepo.Setup(c => c.GetAllAsync()).ReturnsAsync(_categories);
            UnitiOfWork.Setup(u => u.Category).Returns(CategoryRepo.Object);
            ManagerCategoriesController Mcc = new ManagerCategoriesController(UnitiOfWork.Object);
            //Act
            var actionResult = await Mcc.Index();
            ViewResult viewResult = actionResult as ViewResult;
            var resultat = viewResult.ViewData.Model as IEnumerable<Category>;


            //Assert
            Assert.Equal(2, resultat.Count());
        }
        #endregion
        #region Create
        /*Create Post */
        [Fact]
        public async void TestCreateCategory_Fail()
        {
            //Arrange
            ManagerCategoriesController Mcc = new ManagerCategoriesController(UnitiOfWork.Object);
            Mcc.ModelState.AddModelError("Name", "Required");
            //Act

            var model = new Category
            {
                Name = null
            };
            var actionResult = await Mcc.Create(model);
            ViewResult viewResult = actionResult as ViewResult;
            var resultat = viewResult.ViewData.ModelState;

            // Assert
            Assert.False(resultat.IsValid);
        }
        [Fact]
        public async void TestCreateCategory_Working()
        {
            //Arrange
            CategoryRepo.Setup(c => c.Add(It.IsAny<Category>()))
            .Verifiable();
            UnitiOfWork.Setup(c=>c.Category).Returns(CategoryRepo.Object);  
            ManagerCategoriesController Mcc = new ManagerCategoriesController(UnitiOfWork.Object);
            

            //Act

            var model = new Category
            {
                Name = "Test"
            };
            var actionResult = await Mcc.Create(model);
            

            // Assert - Kollar om jag blev Redirected och till samma controller samt att det är index.
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(actionResult);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            UnitiOfWork.Verify();//Kollar så att Add Gjordes :)
        }
        #endregion
        #region Edit
        /*EditGet */

        [Theory]
        [InlineData((string)null)]//If id is null
        [InlineData("ABC123")]//If db cant find the Category object
        public async void TestGet_EditCategory_Fail(string? id)
        {

            //Arrange
            CategoryRepo.Setup(c => c.GetAsync(id)).ReturnsAsync((Category)null);//Test if category is null
            UnitiOfWork.Setup(u => u.Category).Returns(CategoryRepo.Object);
            ManagerCategoriesController Mcc = new ManagerCategoriesController(UnitiOfWork.Object);
            //Act
            var result = Mcc.Edit(id).Result; //<-- CAST HERE
         
            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(id, notFoundObjectResult.Value);

        }
        [Fact]
        public async void TestGet_EditCategory_returnView()
        {
            //Arrange
            string id = "123Find";
            Category category = new Category { Id = id, Name = "TestGet" };   
            CategoryRepo.Setup(c => c.GetAsync(id)).ReturnsAsync(category);//Test if category is null
            UnitiOfWork.Setup(u => u.Category).Returns(CategoryRepo.Object);
            ManagerCategoriesController Mcc = new ManagerCategoriesController(UnitiOfWork.Object);
            //Act
            var result = Mcc.Edit(id).Result; //Check during debugging test and wanted just result

            // Assert
            var FoundObjectResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(category, FoundObjectResult.Model);

        }

        
        /*EditPost */
        [Fact]
        public async void TestPost_EditCategory_FailIdsNotMatching()
        {
            //Arrange
            string id = "123";
            Category category = new Category { Id = "456", Name = "TestGet" };
           
            ManagerCategoriesController Mcc = new ManagerCategoriesController(UnitiOfWork.Object);
            //Act
            var result = Mcc.Edit(id, category).Result; 

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(id, notFoundObjectResult.Value);

        }
        [Fact]
        public async void TestPost_EditCategory_FailModelState()
        {
            //Arrange
            string id = "123";
            Category category = new Category { Id = "123", Name = "TestGet" };

            ManagerCategoriesController Mcc = new ManagerCategoriesController(UnitiOfWork.Object);
            Mcc.ModelState.AddModelError("Name", "Required");
            //Act
            var result = Mcc.Edit(id, category).Result; 

            // Assert
            var ReturnCreateView = Assert.IsType<ViewResult>(result);
            Assert.Equal(category, ReturnCreateView.Model);
            Assert.Null(ReturnCreateView.ViewName);
        }
        [Fact]
        public async void TestPost_EditCategory_Success()
        {
            //Arrange
            string id = "123";
            Category category = new Category { Id = "123", Name = "TestGet" };
            CategoryRepo.Setup(c => c.Update(It.IsAny<Category>()))
           .Verifiable();
            UnitiOfWork.Setup(c => c.Category).Returns(CategoryRepo.Object);
            ManagerCategoriesController Mcc = new ManagerCategoriesController(UnitiOfWork.Object);
            //Act
            var actionResult = await Mcc.Edit(id, category); 

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(actionResult);
            Assert.NotNull(category.EditedAt);
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
        public void TestGet_DeleteCategory_Fail(string? id)
        {
            //Arrange
            CategoryRepo.Setup(c => c.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Category, bool>>>()))
           .ReturnsAsync((Category)null);
            UnitiOfWork.Setup(u => u.Category).Returns(CategoryRepo.Object);
            ManagerCategoriesController Mcc = new ManagerCategoriesController(UnitiOfWork.Object);
            //Act
            var result = Mcc.Delete(id).Result; 

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(id, notFoundObjectResult.Value);

        }
        [Fact]
        public void TestGet_DeleteCategory_returnView()
        {
            //Arrange
            string id = "123Find";
            Category category = new Category { Id = id, Name = "TestGet" };            
            CategoryRepo.Setup(c => c.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Category, bool>>>()))
            .ReturnsAsync(category);


            UnitiOfWork.Setup(u => u.Category).Returns(CategoryRepo.Object);
            ManagerCategoriesController Mcc = new ManagerCategoriesController(UnitiOfWork.Object);
            //Act
            var result = Mcc.Delete(id).Result; //Check during debugging test and wanted just result

            // Assert
            var FoundObjectResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(category, FoundObjectResult.Model);

        }
        /*Delete Confirmed */
        [Fact]
        public void TestDeleteConfirmed_Fail()
        {
            string id = "123";
            //Arrange
            CategoryRepo.Setup(c => c.GetAsync(id)).ReturnsAsync((Category)null);//Test if category is null
            UnitiOfWork.Setup(u => u.Category).Returns(CategoryRepo.Object);
            ManagerCategoriesController Mcc = new ManagerCategoriesController(UnitiOfWork.Object);
            //Act
            var result = Mcc.DeleteConfirmed(id).Result;

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(id, notFoundObjectResult.Value);

        }
        [Fact]
        public async void TesDeleteConfirmedy_SuccesRedirectToIndex()
        {
            //Arrange
            string id = "123Find";
            Category category = new Category { Id = id, Name = "TestGet" };
            CategoryRepo.Setup(c => c.GetAsync(id)).ReturnsAsync(category);//Test if category is null
            CategoryRepo.Setup(c => c.Remove(It.IsAny<Category>())).Verifiable();
            UnitiOfWork.Setup(u => u.Category).Returns(CategoryRepo.Object);
            ManagerCategoriesController Mcc = new ManagerCategoriesController(UnitiOfWork.Object);
            //Act
            var actionResult = await Mcc.DeleteConfirmed(id); //Check during debugging test and wanted just result

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(actionResult);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            UnitiOfWork.Verify();//Kollar så att Remove Gjordes :)

        }


        #endregion


    }
}
