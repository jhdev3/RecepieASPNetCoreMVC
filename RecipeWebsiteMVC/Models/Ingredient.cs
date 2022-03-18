using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeWebsiteMVC.Models
{
    /// <summary>
    /// If you want to Create a "Header field when creating your recipe skip Unit and UnitType"
    /// Make sure to not use Lazy Loading in db. 
    /// </summary>
   
    //Skulle kunna vara en sträng och använda ett Regexp uttryck för att validera och se till att det är acceptabelt format för att uppdatera portioner etc.
    //En anledning att jag gör så här beror på det som står i Summary en annan är om jag senare vill lägga till konverterings funktioner så tror jag att
    // den här uppdelningen gör det enklare att implementera än att hela tiden behöva använda någon typ av sträng manipulering. :)
    public class Ingredient : BaseEntity, IComparable<Ingredient>
    {

        //[RegularExpression(@"^/(\d+(?:\.\d+)?)/$", ErrorMessage = "value of format 9.99")]
        //Super problem med input decimaler för number, Skulle kunna göra text och använda regexp och parasa allt men det orkar inte just nu
        //[Range(0.01, 1000000)]
        // [ValidateNever] //Fungerar inte för tillfälet 
        //[Range(0.01, 1000000.99,
        //ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        //  [Range(0.01, 1000000.99)]
        // [RegularExpression(@"^\((?=.)([+-] ? ([0 - 9] *)(\.([0 - 9] +)) ?)\)$")]
        /*Lössningen en så länge var att ändra i kontrollpanelen region input för decimal till . istället för , */
       // [DisplayFormat(DataFormatString = "{0:0.0}")]
       //[Range(0, 9999999999.999999)]

        [ValidateNever]
        public double? Unit { get; set; }
        [ValidateNever]
        public string? UnitType { get; set; }

        [Required(ErrorMessage = "Namn/Underrubrik på Ingrdiens krävs")]
        public string Name { get; set; }

        [ForeignKey("Recipe")]//Används i Databasen för att skapa relation etc ;) 
        public virtual string? RecipeId { get; set; }

        //Kan bli ett extra attribut för hålla ordning på ingridienserna till ingridienser ;) 
        public int DisplayOrder { get; set; }

        //använder Sort istället för order by, här kan det finnas fördel att använda orberby etc i query med tanke på prestanda:)
        public int CompareTo(Ingredient? other)
        {
            return DisplayOrder.CompareTo(other.DisplayOrder);
        }
    }
}
