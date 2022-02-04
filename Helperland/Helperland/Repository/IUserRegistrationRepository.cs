using Helperland.Models.ViewModel;
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

        public string Message();
    }
}
