using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeWebsiteMVC.Models
{
    public class Direction : BaseEntity, IComparable<Direction>
    {
        [Required(ErrorMessage = "Instruktion fält får inte var tomt")]
        [Display(Name = "Instruktion")]
        public string Text { get; set; }

        public int DisplayOrder { get; set; }

        public int CompareTo(Direction? other)
        {
            return DisplayOrder.CompareTo(other.DisplayOrder);
        }
    }
}
