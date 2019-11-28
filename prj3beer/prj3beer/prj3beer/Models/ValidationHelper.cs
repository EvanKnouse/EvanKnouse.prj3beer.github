using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace prj3beer.Models
{
    /// <summary>
    /// This is the validation helper for validating objects passed into it
    /// </summary>
    class ValidationHelper
    {
        public static IList<ValidationResult> Validate(object model)
        {
            // Results will be a new list of validation results (errors)
            List<ValidationResult> results = new List<ValidationResult>();

            // Instantiate a new Validation Context, taking in the current object
            ValidationContext vc = new ValidationContext(model, null, null);

            // This will attempt to validate the object, passing in the object, the Validation Context, results list, and true for "validate all properties"
            Validator.TryValidateObject(model, vc, results, true);

            // If the object can be validated, then validate the object as a validateable object using the validation context
            if (model is IValidatableObject) (model as IValidatableObject).Validate(vc);

            // return the list of errors created from validating
            return results;
        }
    }
}