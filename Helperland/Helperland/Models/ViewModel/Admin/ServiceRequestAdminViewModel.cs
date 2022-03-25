using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModel.Admin
{
    public class ServiceRequestAdminViewModel
    {
        public int ServiceRequestId { get; set; }
        public string ServiceDate { get; set; }
        public string CustomerName  { get; set; }
        public string CustomerAddressLine1 { get; set; }
        public string CustomerAddressLine2 { get; set; }
        public string ServiceProviderName { get; set; }
        public int ServiceProviderId { get; set; }
        public int Rating { get; set; }
        public decimal NetAmount { get; set; }
        public int?  Status { get; set; }
        public string Avatar { get; set; }

    }
}
