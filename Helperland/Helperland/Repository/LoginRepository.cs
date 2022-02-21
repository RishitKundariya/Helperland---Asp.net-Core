using Helperland.Models.Data;
using Helperland.Models.ViewModel;
using Helperland.Security;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly HelperlandContext helperlandContext;
        private readonly IDataProtector protector;
        public LoginRepository(HelperlandContext _helperlandContext, DataProtectionPurposeString dataProtectionPurposeString, IDataProtectionProvider dataProtectionProvider)
        {
            helperlandContext = _helperlandContext;
            protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeString.HelperlandPasswordValue);
        }

        public string _Message { get; set; }
        public string Message()
        {
            return _Message;
        }
        public int IsValidUser(LoginViewModel loginViewModel)
        {
            try
            {
               
                User user = helperlandContext.Users.Where(x => x.Email == loginViewModel.Email).FirstOrDefault();
                if(user != null && protector.Unprotect( user.Password) == loginViewModel.Password)
                {
                    return user.UserId;
                }
                else
                {
                    _Message += " Invalid username or password ";
                    return -1;
                    
                }


            }
            catch(Exception ex)
            {
                _Message += ex.Message;
                return -1;
            }
        }

        public Boolean IsValidUserEmail(LoginViewModel loginViewModel)
        {
            try
            {
                User user = helperlandContext.Users.Where(x => x.Email == loginViewModel.Email).FirstOrDefault();
                if (user == null)
                {
                    _Message += " Entered email is not registered";
                    return false;
                }
                else
                {
                    return true;
                }


            }
            catch (Exception ex)
            {
                _Message += ex.Message;
                return false;
            }
        }

        public int GetUserID(string Email)
        {
            try
            {
                User user = helperlandContext.Users.Where(x => x.Email == Email ).FirstOrDefault();
                if (user == null)
                {
                    _Message += " Invalid Email";
                    return -1;
                }
                else
                {

                    return user.UserId ;
                }


            }
            catch (Exception ex)
            {
                _Message += ex.Message;
                return -1;
            }
        }

        public Boolean ChangePassword(int UserID, ResetPasswordViewModel resetPasswordViewModel)
        {
            try
            {
                User user = helperlandContext.Users.Find(UserID);
                user.Password =protector.Protect(resetPasswordViewModel.Password);
                helperlandContext.Users.Update(user);
                helperlandContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                _Message += ex.Message;
                return false;
            }
        }

        public User GetUser(int userID)
        {
            try
            {

                User user = helperlandContext.Users.Where(x => x.UserId == userID).FirstOrDefault();
                if (user != null)
                {
                    return user;
                }
                else
                {
                    _Message += " Enter valid Email ";
                    return null;

                }


            }
            catch (Exception ex)
            {
                _Message += ex.Message;
                return null;
            }
        }
    }
}
