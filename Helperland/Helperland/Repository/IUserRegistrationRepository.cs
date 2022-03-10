using Helperland.Models.Data;
using Helperland.Models.ViewModel;
using Helperland.Models.ViewModel.ServiceProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Repository
{
    public interface IUserRegistrationRepository
    {

        public Boolean AddUser(UserRegistrationViewModel userRegistrationViewModel);
        public Boolean AddServiceProvider(UserRegistrationViewModel userRegistrationViewModel);
        public User GetUserById(int userID);
        public Boolean ChangeUserData(string FirstName, string LastName, string DOB, string MobileNumber, int UserId, int LanguageId);

        public void UpdateServiceProviderData(MyDetailsViewModel myDetailsViewModel, int userId);
        public string Message();
    }
}
