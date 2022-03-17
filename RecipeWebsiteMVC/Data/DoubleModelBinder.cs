namespace RecipeWebsiteMVC.Data
{
  
    //public class DoubleModelBinder : DefaultModelBinder
    //{
    //    public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    //    {
    //        var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
    //        object actualValue = null;
    //        try
    //        {
    //            actualValue = Convert.ToDouble(valueResult.AttemptedValue, CultureInfo.InvariantCulture);
    //        }
    //        catch (FormatException e)
    //        {
    //            bindingContext.ModelState.AddModelError(bindingContext.ModelName, e);
    //        }
    //        return actualValue;
    //    }
    //}

}
