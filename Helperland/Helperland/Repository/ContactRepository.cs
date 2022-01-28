using Helperland.Models.Data;
using Helperland.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Repository
{
    public class ContactRepository
    {
        private readonly HelperlandContext helperlandContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ContactRepository(HelperlandContext _helperlandContext, IWebHostEnvironment _webHostEnvironment)
        {
            helperlandContext = _helperlandContext;
            webHostEnvironment = _webHostEnvironment;
        }

        public void AddContactusFrom(ContactViewModel contactViewModel )
        {
            ContactU contactU = new ContactU();
            if(contactViewModel.File != null)
            {
                string filename = Guid.NewGuid().ToString() + contactViewModel.File.FileName;
                string serverPath = Path.Combine(webHostEnvironment.WebRootPath, "upload/contact/" + filename);
                 contactViewModel.File.CopyToAsync(new FileStream(serverPath, FileMode.Create));

                contactU.FileName = filename;
            }
            contactU.Name = contactViewModel.FirstName + " " + contactViewModel.LastName;
            contactU.PhoneNumber = contactViewModel.phonenumber;
            contactU.Subject = contactViewModel.Subject;
            contactU.Email = contactViewModel.Email;
            contactU.Message = contactViewModel.Message;
            contactU.CreatedOn = DateTime.Now;
            helperlandContext.ContactUs.Add(contactU);
            helperlandContext.SaveChanges();


        }
    }
}
