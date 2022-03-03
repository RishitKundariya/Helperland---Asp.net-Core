using Helperland.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModel
{
    public class LoginViewModel
    {
        public int UserID { get; set; }
        [Required (ErrorMessage = "please enter the username")]
        [EmailAddress(ErrorMessage = "please enter valid email")]

        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "please enter the password")]
        [PassworldValidation(ErrorMessage = "Length of Password is must greater than 8 & atleast contain one capital, one Numeric & one Special Character")]
        public string Password { get; set; }

    }
}
