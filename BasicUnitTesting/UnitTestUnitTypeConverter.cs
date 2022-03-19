using Xunit;
using RecipeWebsiteMVC.Models;
using System.Collections.Generic;
using System.Linq;
using RecipeWebsiteMVC.Models.ViewModels;
using RecipeWebsiteMVC.Models.UnitTypeConvert;

namespace BasicUnitTesting
{
  
    public class UnitTestUnitTypeConverter
    {
        public List<Ingredient> data;
        public UnitTestUnitTypeConverter()
        {
            data = new List<Ingredient>{
                new Ingredient  { Unit = 0.5, UnitType = "kg", Name = "Köttfärs"},
                new Ingredient  { Unit = 2000, UnitType = "g", Name = "Kyckling"},

            };
        }
        [Theory]
        [InlineData(0, 500, "g")]
        [InlineData(1, 2, "kg")]
        public void TestUpdate_ConvertGAndKG(int index, double res, string typeRes)
        {
            //Arrange
            var convert = new ConvertGAndKG();
            Ingredient ingredient = data[index];
            //Act
            convert.ConvertUnitTo(ingredient);

            //Assert
            Assert.Equal(typeRes, ingredient.UnitType);
            Assert.Equal(res, ingredient.Unit);
        }
        [Theory]
        [InlineData(0, 500, "g")]
        [InlineData(1, 2, "kg")]
        public void TestUpdate_UnitTypesCollection(int index, double res, string typeRes)
        {
            //Arrange
            var convert = new UnitTypesCollection();
            Ingredient ingredient = data[index];
            //Act
            convert.Convert(ingredient);

            //Assert
            Assert.Equal(typeRes, ingredient.UnitType);
            Assert.Equal(res, ingredient.Unit);
        }
    }
}
