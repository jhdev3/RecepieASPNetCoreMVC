namespace RecipeWebsiteMVC.Models
{
    /// <summary>
    /// Classes/models that want to store in a db should have theese properties
    /// Using new syntax instead of an construktio to initalize them :) 
    /// ID is to easy search etc and DateOfCreation can be used for error check but also if we want to remove objects from a database based on how long its been stored :). 
 
    /// </summary>
    public abstract class BasicProperties
    {
        public string Id { get;} = Guid.NewGuid().ToString();
        public DateTime DateOfCreation { get; set; } = DateTime.Now;

    }
}
