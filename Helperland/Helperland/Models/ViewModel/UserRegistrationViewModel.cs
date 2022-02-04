using Helperland.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModel
{
    public class UserRegistrationViewModel
    {
        [Required(ErrorMessage ="enter first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "enter last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "enter email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "enter mobile number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "enter valid mobile number")]
        public string MobileNumber { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "enter password")]
        [PassworldValidation(ErrorMessage = "Lenght of Password is must greater than 8 & atleat contain one capital, one Numeric & one Special Character")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "enter password")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }

        
    }
}
