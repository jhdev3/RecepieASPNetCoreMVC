using RecipeWebsiteMVC.Models.UnitTypeConvert.Contract;

namespace RecipeWebsiteMVC.Models.UnitTypeConvert
{   

    /// <summary>
    /// Converterar g till kg och vice versa när det behövs
    /// 
    /// </summary>
    public class ConvertGAndKG : IUnitTypeConvert
    {
        public void ConvertUnitTo(Ingredient ingredient)
        {
            if(ingredient.Unit <= 0)//Säkerhets grej borde inte vara det i appen men just to be safe
            {
                return;
            }
            if(ingredient.UnitType == "g" && ingredient.Unit >= 1000)
            {
                ingredient.Unit /= 1000;
                ingredient.UnitType = "kg";
            }
            else if(ingredient.UnitType == "kg" && ingredient.Unit < 1) 
            {
                ingredient.Unit *= 1000;
                ingredient.UnitType = "g";
            }
        }
    }
}
