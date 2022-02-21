using Helperland.Models.Data;
using Helperland.Models.ViewModel.BookService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Repository
{
    public class BookServiceRepository : IBookServiceRepository
    {
        private readonly HelperlandContext helperlandContext;
        public BookServiceRepository(HelperlandContext helperlandContext)
        {
            this.helperlandContext = helperlandContext;
        }

        public Boolean CheckAvailabilityOfService(string pincode)
        {
            User user = helperlandContext.Users.Where(x => x.ZipCode == pincode &&  x.UserTypeId == 2).FirstOrDefault();
            if (user == null)
                return false;
            else
                return true;
        }

        public List<City> GetAllCity()
        {
            return helperlandContext.Cities.ToList();
        }

        public Boolean SetAddress(AddressViewModel addressViewModel)
        {
            City city = helperlandContext.Cities.Where(x => x.Id == addressViewModel.CityId).FirstOrDefault();

            UserAddress userAddress = new UserAddress();
            userAddress.AddressLine1 = addressViewModel.AddressLine1;
            userAddress.AddressLine2 = addressViewModel.AddressLine2;
            userAddress.CityId =city.Id;
            userAddress.StateId = city.StateId;
            userAddress.ZipCode = addressViewModel.ZipCode;
            userAddress.UserId = (int)addressViewModel.UserId;
            userAddress.Mobile = addressViewModel.MobileNo;
            try
            {
                helperlandContext.UserAddresses.Add(userAddress);
                helperlandContext.SaveChanges();
                return true;
            }
            catch(Exception ex) 
            {
                string r=ex.Message;
                r +=ex.InnerException.Message;
                return false;
            }
           
        }

        public List<AddressViewModel> GetAddress(int userID)
        {
            try
            {
                List<AddressViewModel> addresses=new List<AddressViewModel>();
                 List<UserAddress> userAddress = helperlandContext.UserAddresses.Where(x => x.UserId == userID).ToList();
                foreach (var item in userAddress)
                {
                    AddressViewModel address = new AddressViewModel();
                    address.UserId = userID;
                    address.CityId = item.CityId;
                    address.ZipCode = item.ZipCode;
                    address.MobileNo = item.Mobile;
                    address.AddressID = item.AddressId;
                    address.AddressLine1 = item.AddressLine1;
                    address.AddressLine2 = item.AddressLine2;
                    addresses.Add(address);
                }
                return addresses;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
