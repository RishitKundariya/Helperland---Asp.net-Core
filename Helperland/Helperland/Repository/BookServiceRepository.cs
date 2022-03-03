using Helperland.Helpers;
using Helperland.Models.Data;
using Helperland.Models.ViewModel.BookService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Repository
{
    public class BookServiceRepository : IBookServiceRepository
    {
        private readonly HelperlandContext helperlandContext;
        private readonly IConfiguration configuration;
        public BookServiceRepository(HelperlandContext helperlandContext, IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.helperlandContext = helperlandContext;
        }

        #region Check Avaliblity of Service 
        public Boolean CheckAvailabilityOfService(string pincode)
        {
            User user = helperlandContext.Users.Where(x => x.ZipCode == pincode &&  x.UserTypeId == 2).FirstOrDefault();
            if (user == null)
                return false;
            else
                return true;
        }
        #endregion

        #region GetAllCity
        public List<City> GetAllCity()
        {
            return helperlandContext.Cities.ToList();
        }
        #endregion

        #region Book Service Request
        public int BookService(BookServiceViewModel bookServiceViewModel)
        {

            ServiceRequest sr = new ServiceRequest();
            sr.UserId = bookServiceViewModel.UserId;
            sr.ServiceStartDate = bookServiceViewModel.ServiceStartDate;
            sr.ZipCode = bookServiceViewModel.ZipCode;
            sr.ServiceHourlyRate = 18;
            sr.ServiceHours = Decimal.ToDouble(bookServiceViewModel.TotalService);
            sr.TotalCost = bookServiceViewModel.TotalCost;
            sr.Comments = bookServiceViewModel.Comments;
            sr.HasPets = bookServiceViewModel.HasPets;
            sr.CreatedDate = DateTime.Now;
            sr.ModifiedDate = DateTime.Now;
            sr.PaymentDone = true;
            double extra = 0;
            if( ! (bookServiceViewModel.InsideCabinet == "notselected"))
            {
                extra += 0.5;
            }
            if (!(bookServiceViewModel.InsideFridge == "notselected"))
            {
                extra += 0.5;
            }
            if (!(bookServiceViewModel.InteriorOven == "notselected"))
            {
                extra += 0.5;
            }
            if (!(bookServiceViewModel.InteriorWindows == "notselected"))
            {
                extra += 0.5;
            }
            if (!(bookServiceViewModel.LaundryWashDry == "notselected"))
            {
                extra += 0.5;
            }
            sr.ExtraHours = extra;
            try
            {
                helperlandContext.ServiceRequests.Add(sr);
                helperlandContext.SaveChanges();
                UserAddress userAddress = helperlandContext.UserAddresses.Where(x => x.AddressId == bookServiceViewModel.AddressId).FirstOrDefault();
                ServiceRequestAddress serviceRequestAddress = new ServiceRequestAddress();
                serviceRequestAddress.ServiceRequestId = sr.ServiceRequestId;
                serviceRequestAddress.AddressLine1 = userAddress.AddressLine1;
                serviceRequestAddress.AddressLine2 = userAddress.AddressLine2;
                serviceRequestAddress.City = userAddress.CityId.ToString();
                serviceRequestAddress.PostalCode = userAddress.ZipCode;
                serviceRequestAddress.Mobile = userAddress.Mobile;
                serviceRequestAddress.Email = userAddress.Email;
                helperlandContext.ServiceRequestAddresses.Add(serviceRequestAddress);
                helperlandContext.SaveChanges();
                
                if (!(bookServiceViewModel.InsideCabinet == "notselected"))
                {
                    ServiceRequestExtra serviceRequestExtra = new ServiceRequestExtra();
                    serviceRequestExtra.ServiceRequestId = sr.ServiceRequestId;
                    serviceRequestExtra.ServiceExtraId = 1;
                    helperlandContext.ServiceRequestExtras.Add(serviceRequestExtra);
                    helperlandContext.SaveChanges();

                }
                if (!(bookServiceViewModel.InsideFridge == "notselected"))
                {
                    ServiceRequestExtra serviceRequestExtra = new ServiceRequestExtra();
                    serviceRequestExtra.ServiceRequestId = sr.ServiceRequestId;
                    serviceRequestExtra.ServiceExtraId = 2;
                    helperlandContext.ServiceRequestExtras.Add(serviceRequestExtra);
                    helperlandContext.SaveChanges();
                }
                if (!(bookServiceViewModel.InteriorOven == "notselected"))
                {
                    ServiceRequestExtra serviceRequestExtra = new ServiceRequestExtra();
                    serviceRequestExtra.ServiceRequestId = sr.ServiceRequestId;
                    serviceRequestExtra.ServiceExtraId = 3;
                    helperlandContext.ServiceRequestExtras.Add(serviceRequestExtra);
                    helperlandContext.SaveChanges();
                }
                if (!(bookServiceViewModel.InteriorWindows == "notselected"))
                {
                    ServiceRequestExtra serviceRequestExtra = new ServiceRequestExtra();
                    serviceRequestExtra.ServiceRequestId = sr.ServiceRequestId;
                    serviceRequestExtra.ServiceExtraId = 4;
                    helperlandContext.ServiceRequestExtras.Add(serviceRequestExtra);
                   helperlandContext.SaveChanges();
                }
                if (!(bookServiceViewModel.LaundryWashDry == "notselected"))
                {
                    ServiceRequestExtra serviceRequestExtra = new ServiceRequestExtra();
                    serviceRequestExtra.ServiceRequestId = sr.ServiceRequestId;
                    serviceRequestExtra.ServiceExtraId = 5;
                    helperlandContext.ServiceRequestExtras.Add(serviceRequestExtra);
                    helperlandContext.SaveChanges();
                }
            }
            catch(Exception ex)
            {

            }
            SendNotification(sr.HasPets, sr.ZipCode);
            return sr.ServiceRequestId;
        }
        #endregion

        #region Send mail notification
        public void SendNotification(Boolean workWithPat, string zipcode)
        {
            List<User> users = new List<User>();
            if (workWithPat)
            {
                 users = helperlandContext.Users.Where(x => x.ZipCode == zipcode && x.WorksWithPets == workWithPat).ToList();
            }
            else
            {
                 users = helperlandContext.Users.Where(x => x.ZipCode == zipcode).ToList();
            }

            foreach(var item in users)
            {
                string welcomeMessage = "Welcome to Helperland,   <br/> You get new Service request. <br/> <b> Check it now <b>";

                MailHelper mailHelper = new MailHelper(configuration);
                mailHelper.Send(item.Email, welcomeMessage,"New Service Request");
            }
        }
        #endregion

    }
}
