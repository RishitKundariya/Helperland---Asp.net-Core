using Helperland.Helpers;
using Helperland.Models.Data;
using Helperland.Models.ViewModel.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Repository
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        private readonly HelperlandContext _helperlandContext;
        private readonly IConfiguration configuration;
        private readonly IRatingRepository ratingRepository;
        public ServiceRequestRepository(HelperlandContext helperlandContext, IConfiguration _configuration,IRatingRepository ratingRepository)
        {
            configuration = _configuration;
            _helperlandContext = helperlandContext;
            this.ratingRepository = ratingRepository;
        }

        #region Get all services by userid
        public List<CurrentServicesViewModel> GetServicesByUserId(int userID)
        {
           List<ServiceRequest> sr = _helperlandContext.ServiceRequests.Where(x=> x.UserId == userID && x.Status == null).ToList();
            List<CurrentServicesViewModel> csv =new List<CurrentServicesViewModel> ();
           foreach(var item in sr)
           {
                CurrentServicesViewModel l = new CurrentServicesViewModel();
                l.ServiceId = item.ServiceRequestId;
                l.ServiceProvideId = item.ServiceProviderId;
                if(l.ServiceProvideId != null)
                {
                    User u = _helperlandContext.Users.Where(x => x.UserId == l.ServiceProvideId).FirstOrDefault();
                    l.ServiceProviderName = u.FirstName + " " + u.LastName;
                }
                l.StartDate = item.ServiceStartDate.ToString("dd/MM/yyyy");
                l.Payment = item.TotalCost;
                l.Rating = ratingRepository.GetRating(item.ServiceProviderId);
                csv.Add(l);
           }
           return csv;
        }
        #endregion

        #region Get Service reqest data from Service RequestId
        public object GetServiceDetails(int serviceRequestId)
        {
            ServiceRequest sr = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();

            List<ServiceRequestExtra> sre = _helperlandContext.ServiceRequestExtras.Where(x => x.ServiceRequestId == serviceRequestId).ToList();
            ServiceRequestAddress sra= _helperlandContext.ServiceRequestAddresses.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();
            string name = "";
            User u = _helperlandContext.Users.Where(x => x.UserId == sr.UserId).FirstOrDefault();
            if (sr.ServiceProviderId != null)
            {
                User user = _helperlandContext.Users.Where(x => x.UserId == sr.ServiceProviderId).FirstOrDefault();
                name= user.FirstName + " " + user.LastName;
            }
            
            string extra = "";
            foreach(var i in sre)
            {
                if (i.ServiceExtraId == 1)
                    extra += "Inside Cabinet, ";
                if (i.ServiceExtraId == 2)
                    extra += "Inside Fridge, ";
                if (i.ServiceExtraId == 3)
                    extra += "Interior Oven, ";
                if (i.ServiceExtraId == 4)
                    extra += "Interior Window, ";
                if (i.ServiceExtraId == 5)
                    extra += "Laundry & Wash, ";
            }
            var temp = new
            {
                ServiceDate = sr.ServiceStartDate.ToString("dd/MM/yyyy"),
                TotalHour = sr.ServiceHours,
                Extra = extra,
                TotalCost = sr.TotalCost,
                Address = sra.AddressLine1 + " , " + sra.AddressLine2 + " , " + sra.PostalCode,
                MobileNumber = sra.Mobile,
                Email = u.Email,
                ServiceProviderId=sr.ServiceProviderId,
                Comments = sr.Comments,
                Haspet = sr.HasPets,
                ServiceProviderName = name,
                Rating=ratingRepository.GetRating(sr.ServiceProviderId)
            };
            return temp;
            
        }
        #endregion

        #region GetService Request Date
        public string GetServiceDate(int serviceRequestId)
        {
            ServiceRequest sr = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();
            return sr.ServiceStartDate.ToString("yyyy-MM-dd");
        }
        #endregion

        #region Update Service Date
        public bool UpdateServiceDate(int serviceRequestId,DateTime serviceDate)
        {
            ServiceRequest request=_helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();

            if (request.ServiceProviderId == null)
            {
                request.ServiceStartDate = serviceDate;
                _helperlandContext.ServiceRequests.Update(request);
                _helperlandContext.SaveChanges();
                return true;
            }
            else
            {
                ServiceRequest sr = _helperlandContext.ServiceRequests.Where(x => x.ServiceProviderId == request.ServiceProviderId && x.ServiceStartDate == serviceDate).FirstOrDefault();
                if (sr == null)
                {
                    User user = _helperlandContext.Users.Where(x => x.UserId == request.ServiceProviderId).FirstOrDefault();
                    string welcomeMessage = "Welcome to Helperland,   <br/> Service Date chanege for Service Request no " + request.ServiceRequestId +" <br/> <b> Check it now <b>";

                    MailHelper mailHelper = new MailHelper(configuration);
                    mailHelper.Send(user.Email, welcomeMessage, "Service Request Date Change");
                    request.ServiceStartDate = serviceDate;
                    _helperlandContext.ServiceRequests.Update(request);
                    _helperlandContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
        }
        #endregion

        #region Cancel Service
        public bool CancelService(int serviceRequestId, string message)
        {
            ServiceRequest sr = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();
            sr.Status = 2;
            try
            {
                _helperlandContext.ServiceRequests.Update(sr);
                _helperlandContext.SaveChanges();
                if(sr.ServiceProviderId != null)
                {
                    string welcomeMessage = "Welcome to Helperland,   <br/> Service Request no  " + sr.ServiceRequestId + " is canceled due to below reason. <br/>" + message;
                    User user = _helperlandContext.Users.Where(x => x.UserId == sr.ServiceProviderId).FirstOrDefault();
                    MailHelper mailHelper = new MailHelper(configuration);
                    mailHelper.Send(user.Email, welcomeMessage, "Cancellation of Service Request");

                }

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Get all services history 
        public List<ServiceHistoryViewModel> GetServicesHistoryByUserId(int userID)
        {
            List<ServiceRequest> sr = _helperlandContext.ServiceRequests.Where(x => x.UserId == userID && x.Status != null).ToList();
            List<ServiceHistoryViewModel> csv = new List<ServiceHistoryViewModel>();
            foreach (var item in sr)
            {
                ServiceHistoryViewModel l = new ServiceHistoryViewModel();
                l.ServiceId = item.ServiceRequestId;
                l.ServiceProvideId = item.ServiceProviderId;
                if (l.ServiceProvideId != null)
                {
                    User u = _helperlandContext.Users.Where(x => x.UserId == l.ServiceProvideId).FirstOrDefault();
                    l.ServiceProviderName = u.FirstName + " " + u.LastName;
                }
                l.StartDate = item.ServiceStartDate.ToString("dd/MM/yyyy");
                l.Payment = item.TotalCost;
                l.Status = item.Status;
                if(l.Status == 2)
                {
                    l.Rate = false;

                }
                else
                {
                    Rating rating = _helperlandContext.Ratings.Where(x => x.ServiceRequestId == item.ServiceRequestId).FirstOrDefault();
                    if (rating == null)
                        l.Rate = true;
                    else
                        l.Rate = false;
                }

                l.Rating = ratingRepository.GetRating(item.ServiceProviderId);
                csv.Add(l);
            }
            return csv;
        }
        #endregion
    }
}
