using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeWebsiteMVC.Models
{
    public class Direction : BaseEntity
    {
        [Required]
        public string Text { get; set; }    

        //Kanske behöver ett "Order attriubte" för att sätta ordningen på instruktionerna :)
       
    }
}
