namespace RecipeWebsiteMVC.Models
{
    /// <summary>
    /// Classes/models that want to store in a db should have theese properties
    /// Using new syntax instead of an constructor to initalize them :)  
    /// </summary>
    public abstract class BasicProperties
    {
        /// <summary>
        /// Id of an object 
        /// </summary>
        public string Id { get;} = Guid.NewGuid().ToString();
        /// <summary>
        /// Sets the date of creation to right now 
        /// </summary>
        public DateTime DateOfCreation { get; set; } = DateTime.Now;

    }
}
