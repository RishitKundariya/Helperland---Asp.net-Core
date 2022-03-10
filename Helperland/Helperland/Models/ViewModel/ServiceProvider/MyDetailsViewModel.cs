using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModel.ServiceProvider
{
    public class MyDetailsViewModel
    {
        [Required (ErrorMessage ="Please Enter First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Last name")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "Please Enter month ")]
        public int month { get; set; }
        [Required(ErrorMessage = "Please Enter Day ")]
        public int day { get; set; }
        [Required(ErrorMessage = "Please Enter Year")]
        public int year { get; set; }
        public string email { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "enter valid phone number")]
        [Required(ErrorMessage = "Please Enter Mobile Number")]
        public string mobileNumber { get; set; }

        [Required(ErrorMessage = "Please select Gender")]
        public int Gender { get; set; }

        [Required(ErrorMessage = "Please select Avatar")]
        public string UserProfilePicture { get; set; }
        [Required(ErrorMessage = "Please select Nationality")]
        public int NationalityId { get; set; }

        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Please enter street no")]
        public string AddressLine1 { get; set; }
        [Required(ErrorMessage = "Please enter  house number")]
        public string AddressLine2 { get; set; }
        [Required(ErrorMessage = "Please enter zipcode")]
        public string Zipcode { get; set; }
        public int CityId { get; set; }




    }
}
