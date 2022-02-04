using Helperland.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Repository
{
   public interface IContactRepository
    {
        public void AddContactusFrom(ContactViewModel contactViewModel);
    }
}
