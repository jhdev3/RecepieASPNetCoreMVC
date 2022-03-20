using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeWebsiteMVC.Models
{
    /// <summary>
    /// Varje recept bör sparas länkat mellan recept och användare
    /// 
    /// Input ApplicationUserID
    /// Input RecipeID
    /// </summary>
    //Med många användare kan det här bli en lång tabell en annan lössning så blir det istället fler tabeller med en one to many approach.
    public class UserFavoriteRecipe : BaseEntity
    {
        public string ApplicationUserID { get; set; }
        [ForeignKey("ApplicationUserID")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public string RecipeID { get; set; }
        [ForeignKey("RecipeID")]
        [ValidateNever]
        public Recipe recipe {get; set;}
    }
}
