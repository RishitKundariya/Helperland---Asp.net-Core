using Helperland.Models.Data;
using Helperland.Models.ViewModel.BookService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Repository
{
    public class AddressRepository :IAddressRepository
    {
        private readonly HelperlandContext helperlandContext;
        private readonly IConfiguration configuration;
        public AddressRepository(HelperlandContext helperlandContext, IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.helperlandContext = helperlandContext;
        }


        #region Get Service Provider Address
        public UserAddress GetServiceProviderAddress(int userId)
        {
            return helperlandContext.UserAddresses.Where(x => x.UserId == userId).FirstOrDefault();
        }
        #endregion

        #region SetAddress
        public Boolean SetAddress(AddressViewModel addressViewModel)
        {
            City city = helperlandContext.Cities.Where(x => x.Id == addressViewModel.CityId).FirstOrDefault();

            UserAddress userAddress = new UserAddress();
            userAddress.AddressLine1 = addressViewModel.AddressLine1;
            userAddress.AddressLine2 = addressViewModel.AddressLine2;
            userAddress.CityId = city.Id;
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
            catch (Exception ex)
            {
                string r = ex.Message;
                r += ex.InnerException.Message;
                return false;
            }

        }
        #endregion

        #region GetAddress  
        public List<AddressViewModel> GetAddress(int userID)
        {
            try
            {
                List<AddressViewModel> addresses = new List<AddressViewModel>();
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
        #endregion

        #region Get Address By ID
        public AddressViewModel GetAddressById(int AddressID)
        {
            try
            {
                UserAddress userAddress = helperlandContext.UserAddresses.Where(x => x.AddressId == AddressID).FirstOrDefault() ;
                
                    AddressViewModel address = new AddressViewModel();
                    address.CityId = userAddress.CityId;
                    address.ZipCode = userAddress.ZipCode;
                    address.MobileNo = userAddress.Mobile;
                    address.AddressID = userAddress.AddressId;
                    address.AddressLine1 = userAddress.AddressLine1;
                    address.AddressLine2 = userAddress.AddressLine2;
                
                return address;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Update Address
        public Boolean UpdateAddress(AddressViewModel addressViewModel)
        {
            City city = helperlandContext.Cities.Where(x => x.Id == addressViewModel.CityId).FirstOrDefault();

            UserAddress userAddress = new UserAddress
            {
                AddressId = (int)addressViewModel.AddressID,
                UserId = (int)addressViewModel.UserId,
                AddressLine1 = addressViewModel.AddressLine1,
                AddressLine2 = addressViewModel.AddressLine2,
                CityId = city.Id,
                StateId=city.StateId,
                Mobile = addressViewModel.MobileNo,
                ZipCode = addressViewModel.ZipCode

            };
            try
            {
                helperlandContext.UserAddresses.Update(userAddress);
                helperlandContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Delete Address
        public Boolean DeleteAddress(int addressId)
        {
            UserAddress userAddress = helperlandContext.UserAddresses.Where(x => x.AddressId == addressId).FirstOrDefault();
            try
            {
                helperlandContext.UserAddresses.Remove(userAddress);
                helperlandContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
