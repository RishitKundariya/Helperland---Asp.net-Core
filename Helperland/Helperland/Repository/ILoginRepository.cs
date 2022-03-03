using Helperland.Models.Data;
using Helperland.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Repository
{
    public interface ILoginRepository
    {
        public string Message();

        public int IsValidUser(LoginViewModel loginViewModel);

        public Boolean IsValidUserEmail(ForgetPasswordLoginModel forgetPasswordLoginModel );

        public int GetUserID(string Email);

        public Boolean ChangePassword(int UserID,ResetPasswordViewModel resetPasswordViewModel);

        public User GetUser(int userID);

        public bool ResetPassword(int userId, string OldPassword, string NewPassword);


    }
}
