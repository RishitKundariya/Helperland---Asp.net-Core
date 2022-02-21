using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModel.BookService
{
    public class PincodeViewModel
    {
        [Required (ErrorMessage = "Enter pincode")]
        [RegularExpression(@"^(\d{6})$", ErrorMessage = "Enter Valid Pincode")]
        public string Pincode { get; set; }
    }
}
