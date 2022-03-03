using Helperland.Models.Data;
using Helperland.Models.ViewModel;
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
        public string Message()
        {
            return _Message;
        }

    }



}
