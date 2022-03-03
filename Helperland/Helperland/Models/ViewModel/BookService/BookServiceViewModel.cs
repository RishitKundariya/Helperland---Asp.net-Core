using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModel.BookService
{
    public class BookServiceViewModel
    {
        public int UserId { get; set; }
        public DateTime ServiceStartDate { get; set; }
        public string ServiceStartTime { get; set; }
        
        public string ZipCode { get; set; }
        public int  AddressId { get; set; }
        public decimal TotalService { get; set; }
        public decimal ServiceHours { get; set; }
        public decimal TotalCost { get; set; }
        public string Comments { get; set; }
        public bool HasPets { get; set; }
        public string InsideCabinet { get; set; }
        public string InsideFridge { get; set; }
        public string InteriorOven { get; set; }
        public string LaundryWashDry { get; set; }
        public string InteriorWindows { get; set; }
        public DateTime CreatedDate { get; set; }
        
    }
}
