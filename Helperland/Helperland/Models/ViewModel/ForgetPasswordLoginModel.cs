using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModel
{
    public class ForgetPasswordLoginModel
    {
        [Required(ErrorMessage = "please enter the username")]
        [EmailAddress(ErrorMessage = "please enter valid email")]

        public string Email { get; set; }
    }
}
