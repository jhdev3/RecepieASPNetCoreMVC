using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeWebsiteMVC.Models
{
    public class Direction : BaseEntity
    {
        [Required]
        public string Text { get; set; }    
       
    }
}
