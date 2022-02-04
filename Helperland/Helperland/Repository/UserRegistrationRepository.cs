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
        public string Message()
        {
            return _Message;
        }

    }



}
