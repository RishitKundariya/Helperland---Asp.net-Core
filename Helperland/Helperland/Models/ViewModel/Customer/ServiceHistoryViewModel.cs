using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModel.Customer
{
    public class ServiceHistoryViewModel
    {
        public int ServiceId { get; set; }
        public String StartDate { get; set; }
        public int? ServiceProvideId { get; set; }
        public string ServiceProviderName { get; set; }
        public decimal Payment { get; set; }
        public int? Status { get; set; }
        public bool Rate { get; set; }
        public decimal? Rating { get; set; }
        public string Avatar { get; set; }
    }
}
