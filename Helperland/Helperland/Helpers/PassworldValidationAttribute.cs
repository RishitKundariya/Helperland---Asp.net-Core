using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Helpers
{
    public class PassworldValidationAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            string str= value.ToString();
            if (str.Length < 8)
                return false;
            else if (!str.Any(char.IsDigit))
                return false;
            else if (!str.Any(char.IsUpper))
                return false;
            else if (!str.Any(ch => Char.IsLetterOrDigit(ch)))
                return false;
            else
                return true;

        }
        
    }
}
