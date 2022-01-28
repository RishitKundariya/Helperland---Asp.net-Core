using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModel
{
    public class ContactViewModel
    {
        public int ContactID { get; set; }
        [Required (ErrorMessage = "please enter First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "please enter last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "please enter phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "enter valid phone number")]
        public string phonenumber { get; set; }
        [Required(ErrorMessage = "please enter email")]
        [EmailAddress (ErrorMessage = "please enter valid email")]
        public string Email { get; set; }
        public string Subject { get; set; }
        [Required(ErrorMessage = "please enter message")]
        public string  Message { get; set; }

        public IFormFile File { get; set; }
    }
}
