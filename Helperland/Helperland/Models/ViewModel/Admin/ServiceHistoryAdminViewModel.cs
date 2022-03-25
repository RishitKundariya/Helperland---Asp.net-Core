using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModel.Admin
{
    public class ServiceHistoryAdminViewModel
    {
        public SearchViewModel SearchViewModel { get; set; }
        public IEnumerable<ServiceRequestAdminViewModel> ServiceHistory { get; set; }

        public EditServiceRequestViewModel EditServiceModel { get; set; }

        public IEnumerable<UserManagementViewModel> UserList { get; set; }
        public UserSearchViewModel UserSearchModel { get; set; }

        public RefundViewModel RefundModel { get; set; }
    }
}
