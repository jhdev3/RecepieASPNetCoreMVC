namespace RecipeWebsiteMVC.Models.ViewModels
{
    /// <summary>
    /// If already inListNo need for button to put in list with the ID
    /// </summary>
    public class AlreadyInListVM
    {
        public bool IsInList { get; set; } 
        public string recipeID { get; set; }    
    }
}
