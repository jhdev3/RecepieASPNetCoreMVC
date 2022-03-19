using RecipeWebsiteMVC.Models.UnitTypeConvert.Contract;

namespace RecipeWebsiteMVC.Models.UnitTypeConvert
{
    /// <summary>
    /// En Collections av Converters  
    /// </summary>
    public class UnitTypesCollection
    {
        //Finns en massa sätt att göra det på tyckte det här var lite kul. Det bästa som tar tid vore att ha tillgång till APi och göra UnitType till en select list
        //Där man bara får ha types som har IunitConvert eller går att convertera / omvandla genom enkelt api call.
        
        //Dictionary eftersom det blir smidigt men vill man konvertera g till Dl etc får man antingen ändra converGandKG eller byta till något annat kul format ;)
        private Dictionary<string, IUnitTypeConvert> ConvertCollections = new Dictionary<string, IUnitTypeConvert>();
        /// <summary>
        /// Add ConvertTypes 
        /// </summary>
        public UnitTypesCollection()
        {
            ConvertCollections.Add("g", new ConvertGAndKG());
            ConvertCollections.Add("kg", new ConvertGAndKG());

        }

        public void Convert(Ingredient ingredient)
        {
            if(ingredient.UnitType== null)
            {
                return;
            }
            var key = ingredient.UnitType;
            if (ConvertCollections.ContainsKey(key))
            {
                ConvertCollections[key].ConvertUnitTo(ingredient);
            }
            
        }


    }
}
