using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Helpers
{
    public class BooleanMustBeTrueAttribute : ValidationAttribute 
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (Convert.ToBoolean(value) == false)
                return new ValidationResult(ErrorMessage);
            else
                return ValidationResult.Success;
        }
    }
}
