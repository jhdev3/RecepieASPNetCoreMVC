namespace RecipeWebsiteMVC.Models
{
    /// <summary>
    /// Basic model of what is needed for the recipe model. 
    /// </summary>
    public class Recipe : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; } //En sträng och inte ett Category objekt => sortera på kategori görs med query/funktion, men ett recept skulle kunna ingå i flera kategorier, därav antingen göra det till lista eller använda någon typ av tagar #Kyckling#Wok. Samt jag kan välja om jag sparar Id till kategorin eller namnet spelar inte någon jätteroll gissar jag på då tabellen inte blir superstor.  
        public string Image { get; set; }
        public int portions { get; set; }

        public List<Direction> Directions = new List<Direction>(); 
        public List<Ingredient> Ingredients = new List<Ingredient>();


        /// <summary>
        /// Add direction to the list  
        /// </summary>
        /// <param name="d">Direction</param>
        public void AddDirection(Direction d)
        {
            Directions.Add(d);  
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
