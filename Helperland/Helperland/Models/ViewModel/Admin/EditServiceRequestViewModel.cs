using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModel.Admin
{
    public class EditServiceRequestViewModel
    {
        public int ServiceRequestId { get; set; }
        [Required(ErrorMessage = " Select Service Data")]
        [DataType(DataType.Date)]
        public DateTime ServiceStartDate { get; set; }
        [Required(ErrorMessage = "Enter Street name")]
        public string StreetName { get; set; }
        [Required(ErrorMessage = "Enter House name")]
        public string HouseNumber { get; set; }
        [Required(ErrorMessage = "Enter Postal code")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Invalid Zipcde")]
        public string Postalcode { get; set; }

        public int CityId { get; set; }
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}
