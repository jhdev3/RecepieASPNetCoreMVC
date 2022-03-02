using System.ComponentModel.DataAnnotations;

namespace RecipeWebsiteMVC.Models
{
    /// <summary>
    /// Classes/models that want to store in a db should have theese properties
    /// ID is to easy search etc and DateOfCreation can be used for error check but also if we want to remove objects from a database based on how long its been stored :). 
 
    /// </summary>
    public abstract class BaseEntity
    /// Using new syntax instead of an constructor to initalize properties :) 

    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        public DateTime? EditedAt { get; set;}



    }
}
