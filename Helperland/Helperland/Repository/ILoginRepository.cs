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

        public Boolean IsValidUser(LoginViewModel loginViewModel);

        public Boolean IsValidUserEmail(LoginViewModel loginViewModel);

        public int GetUserID(string Email);

        public Boolean ChangePassword(int UserID,ResetPasswordViewModel resetPasswordViewModel);


    }
}
