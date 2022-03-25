using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModel.Admin
{
    public class UserManagementViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public string UserType { get; set; }
        public string PhoneNumber { get; set; }
        public bool? Status { get; set; }
        public string RegistrationDate { get; set; }
        public string PostalCode { get; set; }
        public bool? IsApprove { get; set; }
        public int UserTypeId { get; set; }

    }
}
