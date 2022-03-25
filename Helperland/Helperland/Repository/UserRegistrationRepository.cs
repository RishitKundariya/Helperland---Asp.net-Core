using Helperland.Models.Data;
using Helperland.Models.ViewModel;
using Helperland.Models.ViewModel.Admin;
using Helperland.Models.ViewModel.ServiceProvider;
using Helperland.Security;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Repository
{
   
    public class UserRegistrationRepository : IUserRegistrationRepository
    {
        
           
        private readonly HelperlandContext _helperlandContext;
        public string _Message { get; set; }

        private readonly IDataProtector protector;
        public UserRegistrationRepository(HelperlandContext _helperlandContext, DataProtectionPurposeString dataProtectionPurposeString, IDataProtectionProvider dataProtectionProvider)
        {
            this._helperlandContext = _helperlandContext;
            protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeString.HelperlandPasswordValue);
        
        }

        #region Add User 
        public Boolean AddUser(UserRegistrationViewModel userRegistrationViewModel)
        {
            User check = _helperlandContext.Users.Where(x => x.Email == userRegistrationViewModel.Email).FirstOrDefault();
            if(check == null)
            {
                try
                {
                    User user = new User();
                    user.FirstName = userRegistrationViewModel.FirstName;
                    user.LastName = userRegistrationViewModel.LastName;
                    user.Email = userRegistrationViewModel.Email;
                    user.Mobile = userRegistrationViewModel.MobileNumber;
                    user.Password = protector.Protect(userRegistrationViewModel.Password);
                    user.CreatedDate = DateTime.Now.Date;
                    user.ModifiedDate = DateTime.Now.Date;
                    user.IsActive = false;
                    user.UserTypeId = 1;
                    _helperlandContext.Users.Add(user);
                    _helperlandContext.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    _Message = ex.Message;
                    return false;
                }
            }
            else
            {
                _Message = "Already register User with this email ";
                return false;
            }

        }
        #endregion

        #region Add Service Provide
        public Boolean AddServiceProvider(UserRegistrationViewModel userRegistrationViewModel)
        {
            User check = _helperlandContext.Users.Where(x => x.Email == userRegistrationViewModel.Email).FirstOrDefault();
            if (check == null)
            {
                try
                {
                    User user = new User();
                    user.FirstName = userRegistrationViewModel.FirstName;
                    user.LastName = userRegistrationViewModel.LastName;
                    user.Email = userRegistrationViewModel.Email;
                    user.Mobile = userRegistrationViewModel.MobileNumber;
                    user.Password =protector.Protect(userRegistrationViewModel.Password);
                    user.CreatedDate = DateTime.Now.Date;
                    user.ModifiedDate = DateTime.Now.Date;
                    user.UserTypeId = 2;
                    user.IsActive = false;
                    user.IsApproved = false;
                    user.UserProfilePicture = 0.ToString();
                    _helperlandContext.Users.Add(user);
                    _helperlandContext.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    _Message += ex.Message;
                    return false;
                }
            }
            else
            {
                _Message += "Already register User with this email ";
                return false;
            }

        }
        #endregion

        #region GetUser By ID
        public User GetUserById(int userID)
        { User user = new User();
            try
            {
                user = _helperlandContext.Users.Where(x => x.UserId == userID).FirstOrDefault();
                if (user != null)
                    return user;
                else
                    return null;
            }
            catch(Exception ex)
            {
                return null;
            }

        }

        #endregion

        #region Change User Data
        public Boolean ChangeUserData(string FirstName, string LastName, string DOB, string MobileNumber, int UserId, int LanguageId)
        {
            User user = _helperlandContext.Users.Where(x => x.UserId == UserId).FirstOrDefault();
            user.FirstName = FirstName;
            user.LanguageId = LanguageId;
            user.LastName = LastName;
            user.DateOfBirth = Convert.ToDateTime( DOB);
            user.Mobile = MobileNumber;

            try
            {
                _helperlandContext.Users.Update(user);
                _helperlandContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Update Service Proivider Data
        public void UpdateServiceProviderData(MyDetailsViewModel myDetailsViewModel, int userId)
        {
            User user = _helperlandContext.Users.Where(x => x.UserId == userId).FirstOrDefault();
            UserAddress userAddress = _helperlandContext.UserAddresses.Where(x => x.UserId == userId).FirstOrDefault();
            user.FirstName = myDetailsViewModel.FirstName;
            user.LastName = myDetailsViewModel.lastName;
            user.Mobile = myDetailsViewModel.mobileNumber;
            user.DateOfBirth = Convert.ToDateTime(myDetailsViewModel.day + "/" + myDetailsViewModel.month + "/" + myDetailsViewModel.year);
            user.NationalityId = myDetailsViewModel.NationalityId;
            user.Gender = myDetailsViewModel.Gender;
            user.UserProfilePicture = myDetailsViewModel.UserProfilePicture;
            user.ZipCode = myDetailsViewModel.Zipcode;
            bool isNull = false;
            if (userAddress == null)
            {
                userAddress = new UserAddress();
                userAddress.UserId = userId;
                isNull = true;
                
            }
            userAddress.AddressLine1 = myDetailsViewModel.AddressLine1;
            userAddress.AddressLine2 = myDetailsViewModel.AddressLine2;
            userAddress.ZipCode = myDetailsViewModel.Zipcode;
            userAddress.CityId = myDetailsViewModel.CityId;
            City city = _helperlandContext.Cities.Where(x => x.Id == myDetailsViewModel.CityId).FirstOrDefault();
            userAddress.StateId = city.StateId;
            _helperlandContext.Users.Update(user);
            if (isNull)
                _helperlandContext.UserAddresses.Add(userAddress);
            else
                _helperlandContext.UserAddresses.Update(userAddress);
            _helperlandContext.SaveChanges();

        }
        #endregion

        #region Get list of user for Admin Pannel User Management
        public List<UserManagementViewModel> GetAllUser()
        {
            List<User> users = _helperlandContext.Users.Where(x => x.UserTypeId != 3).ToList();
            List<UserManagementViewModel> list = new List<UserManagementViewModel>();
            foreach(var item in users)
            {
                UserManagementViewModel temp = new UserManagementViewModel();
                temp.UserId = item.UserId;
                temp.UserName = item.FirstName+" " + item.LastName;
                temp.RegistrationDate = item.CreatedDate.ToString("dd/MM/yyyy");
                if(item.UserTypeId == 1)
                {
                    temp.UserType = "Customer";
                }
                else if(item.UserTypeId == 2)
                {
                    temp.UserType = "Service Provider";
                    temp.IsApprove = item.IsApproved;
                }
                temp.UserTypeId = item.UserTypeId;
                temp.PhoneNumber = item.Mobile;
                temp.PostalCode = item.ZipCode;
                temp.Status = item.IsActive;
                list.Add(temp);
            }
            return list;
        }
        #endregion

        #region Delete User from Admin
        public bool DeleteUser(int userId)
        {
            User user = _helperlandContext.Users.Where(x => x.UserId == userId).FirstOrDefault();
            try
            {
                _helperlandContext.Users.Remove(user);
                _helperlandContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Activate User
        public bool ActivateUser(int userId)
        {
            User user = _helperlandContext.Users.Where(x => x.UserId == userId).FirstOrDefault();
            try
            {
                user.IsActive = true;
                _helperlandContext.Users.Update(user);
                _helperlandContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region De activate User
        public bool DeactivateUser(int userId)
        {
            User user = _helperlandContext.Users.Where(x => x.UserId == userId).FirstOrDefault();
            try
            {
                user.IsActive = false;
                _helperlandContext.Users.Update(user);
                _helperlandContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Filter User Data
        public List<UserManagementViewModel> GetFilterUserList(UserSearchViewModel userSearchViewModel)
        {
            List<User> users = _helperlandContext.Users.Where(x => x.UserTypeId != 3).ToList();
            List<UserManagementViewModel> list = new List<UserManagementViewModel>();
            if (userSearchViewModel.Zipcode != null)
            {
                users = users.Where(x =>x.ZipCode != null && x.ZipCode.Contains(userSearchViewModel.Zipcode, System.StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            if (userSearchViewModel.Username != null)
            {
                users = users.Where(x => x.FirstName.Contains(userSearchViewModel.Username, System.StringComparison.CurrentCultureIgnoreCase) || x.LastName.Contains(userSearchViewModel.Username, System.StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            if (userSearchViewModel.UserType != -1)
            {
                users = users.Where(x => x.UserTypeId == userSearchViewModel.UserType).ToList();
            }
            if (userSearchViewModel.PhoneNumber != null)
            {
                users = users.Where(x => x.Mobile != null && x.Mobile.Contains(userSearchViewModel.PhoneNumber, System.StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            foreach (var item in users)
            {
                UserManagementViewModel temp = new UserManagementViewModel();
                temp.UserId = item.UserId;
                temp.UserName = item.FirstName + " " + item.LastName;
                temp.RegistrationDate = item.CreatedDate.ToString("dd/MM/yyyy");
                if (item.UserTypeId == 1)
                {
                    temp.UserType = "Customer";
                }
                else if (item.UserTypeId == 2)
                {
                    temp.UserType = "Service Provider";
                }
                temp.PhoneNumber = item.Mobile;
                temp.PostalCode = item.ZipCode;
                temp.Status = item.IsActive;
                list.Add(temp);
            }
            return list;
        }
        #endregion

        #region Approve Service Provider
        public bool ApproveServiceProvider(int userId)
        {
            User user = _helperlandContext.Users.Where(x => x.UserId == userId).FirstOrDefault();
            try
            {
                user.IsApproved = true;
                _helperlandContext.Users.Update(user);
                _helperlandContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Dis-Approve Service Provider
        public bool DisApproveServiceProvider(int userId)
        {
            User user = _helperlandContext.Users.Where(x => x.UserId == userId).FirstOrDefault();
            try
            {
                user.IsApproved = false;
                _helperlandContext.Users.Update(user);
                _helperlandContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        public string Message()
        {
            return _Message;
        }

    }



}
