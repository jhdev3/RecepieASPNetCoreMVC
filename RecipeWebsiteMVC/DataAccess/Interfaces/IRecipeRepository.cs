using RecipeWebsiteMVC.Models;

namespace RecipeWebsiteMVC.DataAccess.Interfaces
{
    public interface IRecipeRepository : IRepositoryAsync<Recipe>
    {
        public void Update(Recipe obj);

        /// <summary>
        /// Ta Bort Ingridienser och Instruktioner Samtidgt som Receptet deletas
        /// </summary>
        /// <param name="obj"></param>
        public void DeleteCascade(Recipe obj);

        public Task<Recipe> GetDirectionsAndIngredients(string id);

        
    }
}
