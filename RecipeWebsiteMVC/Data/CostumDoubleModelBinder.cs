using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace RecipeWebsiteMVC.Data
{
    /// <summary>
    /// Gör så att man inte behöver ändra i kontroll panelen i region decimal mot . istället för  ,.
    /// </summary>
    public class CostumDoubleModelBinder : IModelBinder
    {
    
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            // var result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var valueAsString = valueProviderResult.FirstValue;


            if (valueAsString != null && !string.IsNullOrEmpty(valueAsString))
            {
                return Task.FromResult(valueAsString);  
            }
                // var result = valueProviderResult.FirstValue; // get the value as string

            var attempted = valueAsString.Replace(",", ".");
            double temp;
            if (double.TryParse(
                attempted,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out temp)
            )
            {
                bindingContext.Result = ModelBindingResult.Success(temp);
            }

            return Task.CompletedTask;
        }
    }

}