using Helperland.Models.Data;
using Helperland.Models.ViewModel.BookService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Repository
{
   public interface IAddressRepository
    {
        public Boolean SetAddress(AddressViewModel addressViewModel);
        public List<AddressViewModel> GetAddress(int userID);

        public AddressViewModel GetAddressById(int AddressID);

        public Boolean UpdateAddress(AddressViewModel addressViewModel);
        public Boolean DeleteAddress(int addressId);
        public UserAddress GetServiceProviderAddress(int userId);
    }
}
