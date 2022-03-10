using Helperland.Helpers;
using Helperland.Models.Data;
using Helperland.Models.ViewModel.Customer;
using Helperland.Models.ViewModel.ServiceProvider;
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

        #region Get Service Request Which is not Accepted
        public List<NewServiceRequestViewModel> GetServiceRequestsNotAccepted(int userId,bool hasPate)
        {
            User u = _helperlandContext.Users.Where(x => x.UserId == userId).FirstOrDefault();
            List<ServiceRequest> sr = _helperlandContext.ServiceRequests.Where(x => x.Status == null && x.ServiceProviderId == null && x.ZipCode == u.ZipCode && x.HasPets == hasPate).ToList();
            List<NewServiceRequestViewModel> newServiceRequestViewModels = new List<NewServiceRequestViewModel>();
          
            foreach (var item in sr)
            {
                NewServiceRequestViewModel l = new NewServiceRequestViewModel();
                l.ServiceRequestID = item.ServiceRequestId;
                l.ServicestarDate = item.ServiceStartDate.ToString("dd/MM/yyyy");
                l.Payment = item.TotalCost;
                User user = _helperlandContext.Users.Where(x => x.UserId == item.UserId).FirstOrDefault();
                l.CustomerName = user.FirstName + " " + user.LastName;
                ServiceRequestAddress adress = _helperlandContext.ServiceRequestAddresses.Where(x => x.ServiceRequestId == item.ServiceRequestId).FirstOrDefault();
                l.Addressline1 = adress.AddressLine1 + " " + adress.AddressLine2;
                City city = _helperlandContext.Cities.Where(x => x.Id == Int32.Parse(adress.City)).FirstOrDefault();
                l.Addressline2 = adress.PostalCode+" "+city.CityName;
                newServiceRequestViewModels.Add(l);


            }
            newServiceRequestViewModels.Reverse();
            return newServiceRequestViewModels;

        }
        #endregion

        #region Get New Service reqest data from Service RequestId for service provider
        public object GetServiceDetailsforServiceProvider(int serviceRequestId)
        {
            ServiceRequest sr = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();

            List<ServiceRequestExtra> sre = _helperlandContext.ServiceRequestExtras.Where(x => x.ServiceRequestId == serviceRequestId).ToList();
            ServiceRequestAddress sra = _helperlandContext.ServiceRequestAddresses.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();
            User u = _helperlandContext.Users.Where(x => x.UserId == sr.UserId).FirstOrDefault();
            City city = _helperlandContext.Cities.Where(x => x.Id == Int32.Parse(sra.City)).FirstOrDefault();
            string extra = "";
            foreach (var i in sre)
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
                Address = sra.AddressLine1 + " , " + sra.AddressLine2 + " , " + sra.PostalCode + " ," +city.CityName,
                ServiceProviderId = sr.ServiceProviderId,
                Comments = sr.Comments,
                Haspet = sr.HasPets,
                CustomerName =u.FirstName+ " "+ u.LastName
            };
            return temp;

        }
        #endregion

        #region Accept Service Request 
        public string AcceptServiceRequest(int userId,int serviceRequestId)
        {
            ServiceRequest sr = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();
            if(sr.ServiceProviderId != null)
            {
                return "This service request is no more available. It has been assigned to another provider.";
            }
            else
            {
                ServiceRequest s = _helperlandContext.ServiceRequests.Where(x => x.ServiceProviderId == userId && x.Status == 3 && x.ServiceStartDate == sr.ServiceStartDate).FirstOrDefault();
                if(s != null)
                {
                    return "Another service request  has already been assigned which has time overlap with this service request. You can’t pick this one!";
                }
                else
                {
                    sr.ServiceProviderId = userId;
                    sr.SpacceptedDate = DateTime.Now;
                    sr.Status = 3;
                    _helperlandContext.ServiceRequests.Update(sr);
                    _helperlandContext.SaveChanges();
                     User user = _helperlandContext.Users.Where(x => x.UserId == userId).FirstOrDefault();
                    string welcomeMessage = "Welcome to Helperland,   <br/> Your Service is accpted by  " + user.FirstName + " "+user.LastName + " <br/> <b> Check it now <b>";
                    User u = _helperlandContext.Users.Where(x => x.UserId == sr.UserId).FirstOrDefault();
                    MailHelper mailHelper = new MailHelper(configuration);
                    mailHelper.Send(u.Email, welcomeMessage, "Service Request Accepted");
                    return "true";
                }
            }

        }
        #endregion

        #region Get Upcoming Service Request Which is  Accepted
        public List<NewServiceRequestViewModel> GetServiceRequestsIsAccepted(int userId)
        {
            User u = _helperlandContext.Users.Where(x => x.UserId == userId).FirstOrDefault();
            List<ServiceRequest> sr = _helperlandContext.ServiceRequests.Where(x => x.Status == 3 && x.ServiceProviderId == userId).ToList();
            List<NewServiceRequestViewModel> newServiceRequestViewModels = new List<NewServiceRequestViewModel>();
            foreach (var item in sr)
            {
                NewServiceRequestViewModel l = new NewServiceRequestViewModel();
                l.ServiceRequestID = item.ServiceRequestId;
                l.ServicestarDate = item.ServiceStartDate.ToString("dd/MM/yyyy");
                l.Payment = item.TotalCost;
                User user = _helperlandContext.Users.Where(x => x.UserId == item.UserId).FirstOrDefault();
                l.CustomerName = user.FirstName + " " + user.LastName;
                ServiceRequestAddress adress = _helperlandContext.ServiceRequestAddresses.Where(x => x.ServiceRequestId == item.ServiceRequestId).FirstOrDefault();
                l.Addressline1 = adress.AddressLine1 + " " + adress.AddressLine2;
                City city = _helperlandContext.Cities.Where(x => x.Id == Int32.Parse(adress.City)).FirstOrDefault();
                l.Addressline2 = adress.PostalCode + " " + city.CityName;
                newServiceRequestViewModels.Add(l);


            }
            newServiceRequestViewModels.Reverse();
            return newServiceRequestViewModels;

        }
        #endregion

        #region Completed Service Request
        public bool CompletedService(int serviceRequestId)
        {
            ServiceRequest serviceRequest = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();
            serviceRequest.Status = 1;
            _helperlandContext.ServiceRequests.Update(serviceRequest);
            _helperlandContext.SaveChanges();

            User user = _helperlandContext.Users.Where(x => x.UserId == serviceRequest.UserId).FirstOrDefault();
            string welcomeMessage = "Welcome to Helperland,   <br/> Hurry, Your Service Request with id   "+ serviceRequest.ServiceRequestId.ToString()+" is Completed. ";
            MailHelper mailHelper = new MailHelper(configuration);
            mailHelper.Send(user.Email, welcomeMessage, "Completed Service Request");
            return true;
        }
        #endregion

        #region Canceled Service Request From Service Provider
        public bool CancelServiceRequest(int serviceRequestId,string message)
        {
            ServiceRequest serviceRequest = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();
            serviceRequest.Status = 2;
            _helperlandContext.ServiceRequests.Update(serviceRequest);
            _helperlandContext.SaveChanges();

            User user = _helperlandContext.Users.Where(x => x.UserId == serviceRequest.UserId).FirstOrDefault();
            string welcomeMessage = "Welcome to Helperland,   <br/> Your Service Request with id   " + serviceRequest.ServiceRequestId.ToString() + " is canceled due to  <b>" + message+" </b>." ;
            MailHelper mailHelper = new MailHelper(configuration);
            mailHelper.Send(user.Email, welcomeMessage, "Canceled Service Request");
            return true;
        }
        #endregion

        #region Service History
        public List<ServiceProviderServiceHistoryViewModel> GetServiceHistoryForServiceProvider(int useId)
        {
            List<ServiceRequest> sr = _helperlandContext.ServiceRequests.Where(x => x.ServiceProviderId == useId && x.Status == 1).ToList();
            List<ServiceProviderServiceHistoryViewModel> li = new List<ServiceProviderServiceHistoryViewModel>();
            foreach(var item in sr)
            {
                User u = _helperlandContext.Users.Where(x => x.UserId == item.UserId).FirstOrDefault();
                ServiceRequestAddress sra= _helperlandContext.ServiceRequestAddresses.Where(x => x.ServiceRequestId == item.ServiceRequestId).FirstOrDefault();
                City c = _helperlandContext.Cities.Where(x => x.Id == Int32.Parse(sra.City)).FirstOrDefault();
                ServiceProviderServiceHistoryViewModel temp = new ServiceProviderServiceHistoryViewModel();
                temp.ServiceRequestId = item.ServiceRequestId;
                temp.ServiceDate = item.ServiceStartDate.ToString("dd/MM/yyyy");
                temp.CustomerName = u.FirstName + " " + u.LastName;
                temp.AddressLine1 = sra.AddressLine1 + " ," + sra.AddressLine2;
                temp.AddressLine2 = sra.PostalCode + "," + c.CityName;
                li.Add(temp);

            }
            return li;
        }
        #endregion
    }
}
