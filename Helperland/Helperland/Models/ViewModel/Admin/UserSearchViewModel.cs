using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModel.Admin
{
    public class UserSearchViewModel
    {
        public string Username { get; set; }
        public int UserType { get; set; }
        public string PhoneNumber { get; set; }
        public string Zipcode { get; set; }
    }
}
