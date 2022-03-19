namespace RecipeWebsiteMVC.Models.UnitTypeConvert.Contract
{
    /// <summary>
    /// Kontrakt som varje ConvertToKlass måste uppfylla.
    /// </summary>
    public interface IUnitTypeConvert
    {
        /* Kanske ska göras static */
        public void ConvertUnitTo(Ingredient ingredient);
    }
}
