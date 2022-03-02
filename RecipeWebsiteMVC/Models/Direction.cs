using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeWebsiteMVC.Models
{
    public class Direction : BaseEntity
    {
        public string Text { get; set; }    
       
        public string RecipeId { get; set; }    
    }
}
