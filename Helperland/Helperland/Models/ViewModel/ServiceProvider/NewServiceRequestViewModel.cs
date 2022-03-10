using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModel.ServiceProvider
{
    public class NewServiceRequestViewModel
    {
        public int ServiceRequestID { get; set; }
        public string  ServicestarDate { get; set; }
        public string  CustomerName { get; set; }
        public string Addressline1 { get; set; }
        public string Addressline2 { get; set; }
        public decimal  Payment { get; set; }

    }
}
