using Microsoft.EntityFrameworkCore;
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


      

        //Adding CascadeDelete On Ingridient And Direction
        //Cascade Delete Can be Auto now in later version othwerwise i need to configure this below:
        // modelbuilder.entity<recipe>()
        //        .hasmany<ingredient>(r => r.ingredients)
        //        .hasforeignkey(i => i.recipeid)
        //        .ondelete(deletebehavior.cascade);
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>()
                .HasMany<Ingredient>(g => g.Ingredients)
                .HasForeignKey("RecipeId")
                 .WillCascadeOnDelete(true);


            modelBuilder.Entity<Ingredient>()
               .HasOptional(o => o.Recipes)
               .WithMany(m => m.Recipes)
               .HasForeignKey(k => k.RecipeId)
               .WillCascadeOnDelete(true);

            modelBuilder.Entity("RecipeWebsiteMVC.Models.Ingredient", b =>
                {
                    b.HasOne("RecipeWebsiteMVC.Models.Recipe", null)
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }*/
    }
}
