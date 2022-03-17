using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace RecipeWebsiteMVC.Data
{
    public class DoubleEntityBinderProvider : IModelBinderProvider
    {
        /// <summary>
        /// Binder overrider för double så lägga till i program.cs 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
          
              if (context == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }

                if (context.Metadata.ModelType == typeof(double))
                {
                    return new BinderTypeModelBinder(typeof(CostumDoubleModelBinder));
                }

                return null;
            
        }
    }
}
