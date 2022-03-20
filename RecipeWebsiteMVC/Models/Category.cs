using System.ComponentModel.DataAnnotations;

namespace RecipeWebsiteMVC.Models
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage ="Fält får inte vara tomt")]
        [Display(Name = "Kategori titel")]
        public string Name { get; set; }
        public DateTime? EditedAt { get; set; }

    }
}
