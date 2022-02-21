using Helperland.Models.Data;
using Helperland.Models.ViewModel.BookService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Repository
{
    public  interface IBookServiceRepository
    {
        public Boolean CheckAvailabilityOfService(string pincode);
        public List<City> GetAllCity();
        public Boolean SetAddress(AddressViewModel addressViewModel);
        public List<AddressViewModel> GetAddress(int userID);
    }
}
