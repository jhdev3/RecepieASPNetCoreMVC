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
        public int Portions { get; set; }

        public IList<Direction> Directions { get; set; }         
        public IList<Ingredient> Ingredients { get; set; }

        public DateTime? EditedAt { get; set; }

        public Recipe()
        {
            Directions = new List<Direction>(); 
            Ingredients = new List<Ingredient>();   
        }
    }
}
