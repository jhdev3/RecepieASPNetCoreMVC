using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeWebsiteMVC.Models
{
    public class Direction : BaseEntity
    {
        public string Text { get; set; }    
       
        [ForeignKey("Recipe")]
        public string RecipeId { get; set; }    
        
    }
}
