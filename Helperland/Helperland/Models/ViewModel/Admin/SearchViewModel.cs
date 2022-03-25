using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModel.Admin
{
    public class SearchViewModel
    {
        public int? ServiceId { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public string CustomerName { get; set; }
        public string ServiceProviderName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int Status { get; set; }
    }
}
