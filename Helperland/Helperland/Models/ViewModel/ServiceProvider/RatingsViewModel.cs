using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModel.ServiceProvider
{
    public class RatingsViewModel
    {
        public int RatingID { get; set; }
        public string CustomerName { get; set; }
        public int ServiceRequestID { get; set; }
        public string Comments { get; set; }
        public string ServiceDate { get; set; }
        public decimal Rating { get; set; }
    }
}
