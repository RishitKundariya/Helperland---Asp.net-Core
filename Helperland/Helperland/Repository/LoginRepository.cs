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

        #region Check for Valid Username or not
        public int IsValidUser(LoginViewModel loginViewModel)
        {
            try
            {
               
                User user = helperlandContext.Users.Where(x => x.Email == loginViewModel.Email).FirstOrDefault();
                if (user != null && protector.Unprotect( user.Password) == loginViewModel.Password)
                {
                    if(user.IsActive == false)
                    {
                        _Message += " Your Account is not Activated ";
                        return -1;
                    }
                    else
                    {
                        if(user.UserTypeId==2 &&  user.IsApproved == false)
                        {
                            _Message += " Your Account is not Approved ";
                            return -1;
                        }
                        return user.UserId;
                    }
                   
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
        #endregion

        #region Check for Vallid Email Address in forget
        public Boolean IsValidUserEmail(ForgetPasswordLoginModel forgetPasswordLoginModel)
        {
            try
            {
                User user = helperlandContext.Users.Where(x => x.Email == forgetPasswordLoginModel.Email).FirstOrDefault();
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
        #endregion

        #region Get User ID from Email address
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
        #endregion

        #region Change Password
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
        #endregion

        #region Reset Password 
        public bool ResetPassword(int userId,string OldPassword,string NewPassword)
        {
            try
            {
                User user = helperlandContext.Users.Find(userId);
                string pass = protector.Unprotect(user.Password);
                if (pass == OldPassword)
                {
                    user.Password = protector.Protect(NewPassword);
                    helperlandContext.Users.Update(user);
                    helperlandContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
        #endregion

        #region Check for User validation
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
        #endregion
    }
}
