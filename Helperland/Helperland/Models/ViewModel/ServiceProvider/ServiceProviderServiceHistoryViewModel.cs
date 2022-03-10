using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModel.ServiceProvider
{
    public class ServiceProviderServiceHistoryViewModel
    {
        public int ServiceRequestId { get; set; }
        public string CustomerName { get; set; }
        public string ServiceDate { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

    }
}
