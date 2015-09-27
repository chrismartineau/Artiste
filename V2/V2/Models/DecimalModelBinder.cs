using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace V2.Models
{
    public class DecimalModelBinder :  System.Web.Mvc.IModelBinder
    {

        public object BindModel(ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            System.Web.Mvc.ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            System.Web.Mvc.ModelState modelState = new System.Web.Mvc.ModelState { Value = valueResult };

            object actualValue = null;

            if (valueResult.AttemptedValue != string.Empty)
            {
                try
                {
                    actualValue = Convert.ToDecimal(valueResult.AttemptedValue, CultureInfo.CurrentCulture);
                }
                catch (FormatException e)
                {
                    modelState.Errors.Add(e);
                }
            }

            bindingContext.ModelState.Add(bindingContext.ModelName.ToString(), modelState);

            return actualValue;
        }

    }
}