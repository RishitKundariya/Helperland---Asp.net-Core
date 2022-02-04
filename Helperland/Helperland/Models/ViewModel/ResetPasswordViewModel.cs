using Helperland.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModel
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage ="Enter the password") ]
        [DataType(DataType.Password)]
        [PassworldValidation(ErrorMessage = "Lenght of Password is must greater than 8 & atleat contain one capital, one Numeric & one Special Character")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter the Confirm password")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string Confirmpassword { get; set; }
    }
}
