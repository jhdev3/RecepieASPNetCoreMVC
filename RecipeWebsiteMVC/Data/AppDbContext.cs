﻿using Microsoft.EntityFrameworkCore;
using RecipeWebsiteMVC.Models;

namespace RecipeWebsiteMVC.Data
{
    /// <summary>
    /// This Applications Database with Context and sets
    /// </summary>
    public class AppDbContext : DbContext
    {
        //Will configure the dB Context and set the Connection string when building the app 
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        //List all models that need u want in database here : :)
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Direction> Directions { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
