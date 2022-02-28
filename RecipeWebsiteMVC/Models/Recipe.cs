namespace RecipeWebsiteMVC.Models
{
    /// <summary>
    /// Basic model of what is needed for the recipe model. 
    /// </summary>
    public class Recipe : BaseEntity
    {
        public string Title { get; set; }  
        public string Description { get; set; }
        public string Category { get; set; } //En sträng och inte ett Category objekt beror på att jag kan vilja ett ett recept har flera kategorier / tagar 
        public string Image { get; set; }
        public int portions { get; set; }

        public List<string> Instruktions = new List<string>();
        public List<Ingredient> Ingredients = new List<Ingredient>();



        /// <summary>
        /// Add an instruktion to the recipe
        /// </summary>
        /// <param name="instruktion"></param>
        public void AddInstruktion(string instruktion)
        {
            Instruktions.Add(instruktion);  
        }
        /// <summary>
        /// Add an ingrident
        /// </summary>
        /// <param name="i">Ingredient</param>
        public void AddIngredient(Ingredient i)
        {
            Ingredients.Add(i); 
        }
    }
}
