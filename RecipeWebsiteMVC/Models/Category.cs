using System.ComponentModel.DataAnnotations;

namespace RecipeWebsiteMVC.Models
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public DateTime? EditedAt { get; set; }

    }
}
