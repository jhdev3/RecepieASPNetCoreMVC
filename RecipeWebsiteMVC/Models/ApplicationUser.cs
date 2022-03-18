using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RecipeWebsiteMVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }    
    }
}
