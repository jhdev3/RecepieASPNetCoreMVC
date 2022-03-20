using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RecipeWebsiteMVC.Models
{
    /// <summary>
    /// Lägga till egna "Fält eller så För User i UserDb :)"
    /// I någon annan app skulle det kunna vara address o liknande
    /// Här väljer jag bara Namn och när Usern är skapad för att jag tycker det är rolig och relevant info
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Namn")]
        public string Name { get; set; }    
        public DateTime CreatedAt { get; set; }    
    }
}
