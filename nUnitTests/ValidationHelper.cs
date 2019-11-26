using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace prj3beer.Models
{
    /// <summary>
    /// This class is a helper method to perform validation on the entity classes
    /// </summary>
    class ValidationHelper
    {
        /// <summary>
        /// This method will validate the entity
        /// </summary>
        /// <param name="model">The entity being validated</param>
        /// <returns>A List of errors if there was any</returns>
        public static IList<ValidationResult> Validate(object model)
        {
            var results = new List<ValidationResult>();

            var vc = new ValidationContext(model, null, null);

            Validator.TryValidateObject(model, vc, results, true);

            if (model is IValidatableObject) (model as IValidatableObject).Validate(vc);

            return results;
        }
    }
}