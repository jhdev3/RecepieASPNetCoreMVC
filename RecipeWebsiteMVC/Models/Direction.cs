using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeWebsiteMVC.Models
{
    public class Direction : BaseEntity, IComparable<Direction>
    {
        [Required]
        public string Text { get; set; }

        //Kanske behöver ett "Order attriubte" för att sätta ordningen på instruktionerna :)
        public int DisplayOrder { get; set; }

        public int CompareTo(Direction? other)
        {
            return DisplayOrder.CompareTo(other.DisplayOrder);
        }
    }
}
