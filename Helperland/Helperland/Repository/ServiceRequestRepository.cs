using Helperland.Helpers;
using Helperland.Models.Data;
using Helperland.Models.ViewModel.Admin;
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
           List<ServiceRequest> sr = _helperlandContext.ServiceRequests.Where(x=> x.UserId == userID ).ToList();
            sr = sr.Where(x => x.Status == null || x.Status == 3).ToList();
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
                    l.Avatar = u.UserProfilePicture;
                }
                l.StartDate = item.ServiceStartDate.ToString("dd/MM/yyyy");
                l.Payment = item.TotalCost;
                l.Rating = ratingRepository.GetRating(item.ServiceProviderId);
                
                csv.Add(l);
           }
           return csv;
        }
        #endregion

        #region Get Service reqest details  from Service RequestId 
        public object GetServiceDetails(int serviceRequestId)
        {
            ServiceRequest sr = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();

            List<ServiceRequestExtra> sre = _helperlandContext.ServiceRequestExtras.Where(x => x.ServiceRequestId == serviceRequestId).ToList();
            ServiceRequestAddress sra= _helperlandContext.ServiceRequestAddresses.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();
            string name = "";
            string avatar = "";
            User u = _helperlandContext.Users.Where(x => x.UserId == sr.UserId).FirstOrDefault();
            if (sr.ServiceProviderId != null)
            {
                User user = _helperlandContext.Users.Where(x => x.UserId == sr.ServiceProviderId).FirstOrDefault();
                name= user.FirstName + " " + user.LastName;
                avatar = user.UserProfilePicture;
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
                Rating=ratingRepository.GetRating(sr.ServiceProviderId),
                Avatar=avatar
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

        #region Get all services history for User
        public List<ServiceHistoryViewModel> GetServicesHistoryByUserId(int userID)
        {
            List<ServiceRequest> sr = _helperlandContext.ServiceRequests.Where(x => x.UserId == userID ).ToList();
            sr = sr.Where(x => x.Status == 1 || x.Status == 2).ToList();
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
                    l.Avatar = u.UserProfilePicture;
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
            List<FavoriteAndBlocked> blockUserList = _helperlandContext.FavoriteAndBlockeds.Where(x => x.UserId == userId).ToList();
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

                bool isblockUser = blockUserList.Any(x => x.TargetUserId == item.UserId && x.IsBlocked == true);
                if (!isblockUser)
                {
                    newServiceRequestViewModels.Add(l);
                }


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
                if(city != null)
                {
                    l.Addressline2 = adress.PostalCode + " " + city.CityName;
                }
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

        #region Service History for Service Provider
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

        #region Service List for Admin
        public List<ServiceRequestAdminViewModel> GetAllServiceListForAdmin()
        {
            List<ServiceRequest> sr = _helperlandContext.ServiceRequests.Include(x=>x.ServiceRequestAddresses).ToList();

            List<ServiceRequestAdminViewModel> list = new List<ServiceRequestAdminViewModel>();
            foreach(var item in sr)
            {
                ServiceRequestAdminViewModel temp = new ServiceRequestAdminViewModel();
                User Customer = _helperlandContext.Users.Where(x => x.UserId == item.UserId).FirstOrDefault();
                if(item.ServiceProviderId != null)
                {
                    User ServiceProvider = _helperlandContext.Users.Where(x => x.UserId == item.ServiceProviderId).FirstOrDefault();
                    temp.ServiceRequestId =(int) item.ServiceProviderId;
                    temp.ServiceProviderName = ServiceProvider.FirstName + " " + ServiceProvider.LastName;
                    temp.Rating = (int)ratingRepository.GetRating(item.ServiceProviderId);
                    temp.Avatar = ServiceProvider.UserProfilePicture;
                }
                
                temp.ServiceRequestId = item.ServiceRequestId;
                temp.ServiceDate = item.ServiceStartDate.ToString("dd/MM/yyyy");
                temp.CustomerName = Customer.FirstName + " " + Customer.LastName;
                UserAddress address = _helperlandContext.UserAddresses.Where(x => x.UserId == item.UserId).FirstOrDefault();
                temp.CustomerAddressLine1 = address.AddressLine1;
                temp.CustomerAddressLine2 = address.AddressLine2;
                temp.NetAmount = item.TotalCost;
                temp.Status = item.Status;
               
                list.Add(temp);
            }

            return list;
        }
        #endregion

        #region Serach Service History
        public List<ServiceRequestAdminViewModel> GetServiceBySearch(ServiceHistoryAdminViewModel li)
        {
            List<ServiceRequest> sr = _helperlandContext.ServiceRequests.Include(x => x.ServiceRequestAddresses).ToList();
            List<ServiceRequestAdminViewModel> list = new List<ServiceRequestAdminViewModel>();
            SearchViewModel searchViewModel = li.SearchViewModel;
            if (searchViewModel.ServiceId != null)
            {
                sr = sr.Where(x => x.ServiceRequestId == searchViewModel.ServiceId).ToList();
            }
            if(searchViewModel.PostalCode != null)
            {
                sr = sr.Where(x => x.ZipCode.Contains(searchViewModel.PostalCode, System.StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            if(searchViewModel.ToDate != null)
            {
                DateTime todate = Convert.ToDateTime(searchViewModel.ToDate);
                sr = sr.Where(x => x.ServiceStartDate < todate).ToList();
            }
            if (searchViewModel.FromDate != null)
            {
                DateTime fromDate = Convert.ToDateTime(searchViewModel.FromDate);
                sr = sr.Where(x => x.ServiceStartDate > fromDate).ToList();
            }
            if(searchViewModel.Status != -1)
            {
                sr = sr.Where(x => x.Status == searchViewModel.Status).ToList();
            }
            bool check = false;
            bool isInside = false;
            foreach (var item in sr)
            {
                check = false;
                ServiceRequestAdminViewModel temp = new ServiceRequestAdminViewModel();
                User Customer = _helperlandContext.Users.Where(x => x.UserId == item.UserId).FirstOrDefault();
                if (item.ServiceProviderId != null)
                {
                    User ServiceProvider = _helperlandContext.Users.Where(x => x.UserId == item.ServiceProviderId).FirstOrDefault();
                    temp.ServiceRequestId = (int)item.ServiceProviderId;
                    temp.ServiceProviderName = ServiceProvider.FirstName + " " + ServiceProvider.LastName;
                    temp.Rating = (int)ratingRepository.GetRating(item.ServiceProviderId);
                }

                temp.ServiceRequestId = item.ServiceRequestId;
                temp.ServiceDate = item.ServiceStartDate.ToString("dd/MM/yyyy");
                temp.CustomerName = Customer.FirstName + " " + Customer.LastName;
                UserAddress address = _helperlandContext.UserAddresses.Where(x => x.UserId == item.UserId).FirstOrDefault();
                temp.CustomerAddressLine1 = address.AddressLine1;
                temp.CustomerAddressLine2 = address.AddressLine2;
                temp.NetAmount = item.TotalCost;
                temp.Status = item.Status;
                if(searchViewModel.CustomerName != null)
                {
                    isInside = true;
                    if(Customer.FirstName.Contains(searchViewModel.CustomerName, System.StringComparison.CurrentCultureIgnoreCase) || Customer.LastName.Contains(searchViewModel.CustomerName, System.StringComparison.CurrentCultureIgnoreCase))
                    {
                        check = true;
                    }
                    
                }
                if (searchViewModel.ServiceProviderName != null)
                {
                    if(temp.ServiceProviderName != null)
                    {
                        isInside = true;
                        if (temp.ServiceProviderName.Contains(searchViewModel.ServiceProviderName, System.StringComparison.CurrentCultureIgnoreCase))
                        {
                            check = true;
                        }
                    }
                }
                if (searchViewModel.Email != null)
                {
                    isInside = true;
                    if (Customer.Email.Contains(searchViewModel.Email, System.StringComparison.CurrentCultureIgnoreCase))
                    {
                        check = true;
                    }

                }
                if (isInside== true && check == true)
                {
                    list.Add(temp);
                }
                if(!isInside)
                {
                    list.Add(temp);
                }
                
            }

            return list;
        }
        #endregion

        #region Get Service reqest details  from ServiceRequestId for admin
        public object GetServiceDetailsForAdmin(int serviceRequestId)
        {
            ServiceRequest sr = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();
            ServiceRequestAddress sra = _helperlandContext.ServiceRequestAddresses.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();

            var temp = new
            {
                ServiceDate = sr.ServiceStartDate.ToString("yyyy/MM/dd"),
                StreetName = sra.AddressLine1,
                HouseNumber = sra.AddressLine2,
                Zipcode = sra.PostalCode,
                CityId = sra.City
            };
            return temp;

        }
        #endregion

        #region Update Service Request by Admin
        public void UpdateServiceRequest(EditServiceRequestViewModel editServiceRequestViewModel)
        {
            ServiceRequest sr = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == editServiceRequestViewModel.ServiceRequestId).FirstOrDefault();
            ServiceRequestAddress sra = _helperlandContext.ServiceRequestAddresses.Where(x => x.ServiceRequestId == editServiceRequestViewModel.ServiceRequestId).FirstOrDefault();
            bool isDateChange = false;
            if(editServiceRequestViewModel.ServiceStartDate != sr.ServiceStartDate)
            {
                isDateChange = true;
            }
            sr.ServiceStartDate = editServiceRequestViewModel.ServiceStartDate;
            sra.AddressLine1 = editServiceRequestViewModel.StreetName;
            sra.AddressLine2 = editServiceRequestViewModel.HouseNumber;
            sra.City = editServiceRequestViewModel.CityId.ToString();
            sra.PostalCode = editServiceRequestViewModel.Postalcode;
            _helperlandContext.ServiceRequests.Update(sr);
            _helperlandContext.ServiceRequestAddresses.Update(sra);
            _helperlandContext.SaveChanges();
            if (isDateChange)
            {
                User custome = _helperlandContext.Users.Where(x => x.UserId == sr.UserId).FirstOrDefault();
                string welcomeMessage = "Welcome to Helperland,   <br/> Your Service Request with id   " + sr.ServiceRequestId.ToString() + " is Reschedule on <b> "+ editServiceRequestViewModel.ServiceStartDate.ToString("dd/MM/yyyy") +" </b>. Due to this reason  <b>"+ editServiceRequestViewModel.Message+".<b/>";
                MailHelper mailHelper = new MailHelper(configuration);
                mailHelper.Send(custome.Email, welcomeMessage, "Reschedule Service Request");
                if(sr.ServiceProviderId != null)
                {
                    User ServiceProvider = _helperlandContext.Users.Where(x => x.UserId == sr.ServiceProviderId).FirstOrDefault();
                    string mesage = "Welcome to Helperland,   <br/>  Service Request with id   " + sr.ServiceRequestId.ToString() + " is Reschedule on <b> " + editServiceRequestViewModel.ServiceStartDate.ToString("dd/MM/yyyy") + "</b>. Due to this reason <b>" + editServiceRequestViewModel.Message+".</b>";
                    MailHelper mailHelper2 = new MailHelper(configuration);
                    mailHelper2.Send(ServiceProvider.Email, mesage, "Reschedule Service Request");
                }
            }

        }
        #endregion

        #region Get Refund Details of service Request
        public object GetRefundDetails(int serviceRequestId)
        {
            ServiceRequest sr = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();

            decimal refundAmount;
            if (sr.RefundedAmount == null)
            {
                refundAmount = 0;
            }
            else
                refundAmount =(decimal) sr.RefundedAmount;

            var temp = new
            {
                TotalAmount=sr.TotalCost,
                RefundAmount=refundAmount,

            };
            return temp;
        }
        #endregion

        #region Upadte Refund Amount

        public void RefundAmount(RefundViewModel refundViewModel)
        {
            ServiceRequest serviceRequest = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == refundViewModel.ServiceRequestId).FirstOrDefault();
            if(serviceRequest.RefundedAmount == null)
            {
                serviceRequest.RefundedAmount = refundViewModel.CalculateAmount;
            }
            else
            {
                serviceRequest.RefundedAmount += refundViewModel.CalculateAmount;
            }
            _helperlandContext.ServiceRequests.Update(serviceRequest);
            _helperlandContext.SaveChanges();
            User user = _helperlandContext.Users.Where(x => x.UserId == serviceRequest.UserId).FirstOrDefault();
            string mesage = "Welcome to Helperland,   <br/>  Service Request with id   " + serviceRequest.ServiceRequestId.ToString() + " . </br>  Refunded Amount is  <b> " + refundViewModel.CalculateAmount + "</b>  credited in your account. Due to this reason <b> " + refundViewModel.Message + ".</b>";
            MailHelper mailHelper2 = new MailHelper(configuration);
            mailHelper2.Send(user.Email, mesage, "Refuned Amount ");


        }
        #endregion

        #region Get Accepted and Upcoming Service Date 
        public object GetDateOfAcceptedAndUpcomingServiceRequest(int userId)
        {
            List<ServiceRequest> serviceRequestList = _helperlandContext.ServiceRequests.Where(x => x.ServiceProviderId == userId && (x.Status == 1 || x.Status == 3 )).ToList();
            serviceRequestList=serviceRequestList.Where(x => x.Status == 1 || x.Status == 3).ToList();
            var listOfDate = serviceRequestList.Select(x => new
            {
                id = x.ServiceRequestId,
                start = x.ServiceStartDate.ToString("dd/MM/yyyy"),
                color=x.Status==1 ? "grey" : "#1d7a8c"

            }).ToList();
            return listOfDate;
        }
        #endregion
    }
}
