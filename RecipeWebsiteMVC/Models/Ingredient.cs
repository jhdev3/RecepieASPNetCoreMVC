using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeWebsiteMVC.Models
{
    /// <summary>
    /// If you want to Create a "Header field when creating your recipe skip Unit and UnitType"
    /// </summary>
   
    //Skulle kunna vara en sträng och använda ett Regexp uttryck för att validera och se till att det är acceptabelt format för att uppdatera portioner etc.
    //En anledning att jag gör så här beror på det som står i Summary en annan är om jag senare vill lägga till konverterings funktioner så tror jag att
    // den här uppdelningen gör det enklare att implementera än att hela tiden behöva använda någon typ av sträng manipulering. :)
    public class Ingredient : BaseEntity
    {
        public double? Unit { get; set; }
        public string? UnitType { get; set; }
        public string Name { get; set; }

        public string? RecipeId { get; set; }

    }
}
