using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace RecipeWebsiteMVC.Models
{
    /// <summary>
    /// Basic model of what is needed for the recipe model. 
    /// </summary>
   //Attribute Validate Never om man vill få bort saker som inte ska valideras
    public class Recipe : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; } //En sträng och inte ett Category objekt här vill jag inte skapa någon relation på så sätt att de skapas extra tabeller i databasen. Tanken här är att hämta det som matchar kategrion i den columnen i tabellen.
        public string? Image { get; set; }

        [Required]
        [Range(1, 200, ErrorMessage = "Större än 0 och mindre än 200 :)")]
        public int Portions { get; set; }


        //Göra dessa till virtual skulle kunna öppna för lazy loading. Skapar n+1 extra queries.
        //Vilket vi inte vill ha för en web app. Försöka undvik virtual här
        //Kan också sätt lazyloading till false för AppDbContext
        public IList<Direction> Directions { get; set; }
        public IList<Ingredient> Ingredients { get; set; }

        public DateTime? EditedAt { get; set; }

        public Recipe()
        {
            Directions = new List<Direction>(); 
            Ingredients = new List<Ingredient>();   
        }
    }
}
