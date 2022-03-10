using Helperland.Helpers;
using Helperland.Models.Data;
using Helperland.Models.ViewModel.ServiceProvider;
using Helperland.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    [SessionHelper(UserTypeID: 2)]
    public class ServiceProviderController : Controller
    {
        private readonly IUserRegistrationRepository userRegistrationRepository;
        private readonly IAddressRepository addressRepository;
        private readonly IBookServiceRepository bookServiceRepository;
        private readonly ILoginRepository loginRepository;
        private readonly IRatingRepository ratingRepository;
        private readonly IServiceRequestRepository serviceRequestRepository;
        private readonly IFavouriteAndBlockRepository favouriteAndBlockRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        private ISession _session => httpContextAccessor.HttpContext.Session;
        private int ServiceProviderUserID;

        public ServiceProviderController(IUserRegistrationRepository userRegistrationRepository, IAddressRepository addressRepository,
                                IBookServiceRepository bookServiceRepository,ILoginRepository loginRepository,
                                IRatingRepository ratingRepository,IServiceRequestRepository serviceRequestRepository,
                                IFavouriteAndBlockRepository favouriteAndBlockRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.userRegistrationRepository = userRegistrationRepository;
            this.addressRepository = addressRepository;
            this.bookServiceRepository = bookServiceRepository;
            this.loginRepository = loginRepository;
            this.ratingRepository = ratingRepository;
            this.serviceRequestRepository = serviceRequestRepository;
            this.favouriteAndBlockRepository = favouriteAndBlockRepository;
            this.httpContextAccessor = httpContextAccessor;
            ServiceProviderUserID = (int)_session.GetInt32("userID");
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult NewServiceRquest(string hasPate)
        {
            bool hasPasts ;
            if (hasPate == "on")
            {
                hasPasts = true;
            }
            else
            {
                hasPasts = false;
            }
            ViewBag.hasPat = hasPasts;
            return View(serviceRequestRepository.GetServiceRequestsNotAccepted(ServiceProviderUserID, hasPasts));
        }
        public IActionResult UpcomingServices()
        {
            return View(serviceRequestRepository.GetServiceRequestsIsAccepted(ServiceProviderUserID));
        }
        public IActionResult ServiceHistory()
        {

            return View(serviceRequestRepository.GetServiceHistoryForServiceProvider(ServiceProviderUserID));
        }
        public IActionResult MyRating()
        {
            
            return View(ratingRepository.GetRatingsForServiceProvider(ServiceProviderUserID));
        }
        public IActionResult BlockCustomer()
        {
            return View(favouriteAndBlockRepository.GetListOfCustomer(ServiceProviderUserID));
        }
        public IActionResult MySetting()
        {
            MyDetailsViewModel model = new MyDetailsViewModel();
            User user;
            user = userRegistrationRepository.GetUserById(ServiceProviderUserID);
            UserAddress userAddress = addressRepository.GetServiceProviderAddress(ServiceProviderUserID);
            if (user != null)
            {
                model.FirstName = user.FirstName;
                model.lastName = user.LastName;
                model.month = Convert.ToDateTime(user.DateOfBirth).Month;
                model.day = Convert.ToDateTime(user.DateOfBirth).Day;
                model.year = Convert.ToDateTime(user.DateOfBirth).Year;
                model.mobileNumber = user.Mobile;
                model.email = user.Email;
                if (user.Gender != null)
                    model.Gender = (int)user.Gender;
                if (user.NationalityId != null)
                    model.NationalityId = (int)user.NationalityId;

                model.UserProfilePicture = user.UserProfilePicture;
                if (user.IsActive != null)
                    model.IsActive = (bool)user.IsActive;
                else
                    model.IsActive = false;

            }
            if (userAddress != null)
            {
                model.AddressLine1 = userAddress.AddressLine1;
                model.AddressLine2 = userAddress.AddressLine2;
                model.Zipcode = userAddress.ZipCode;
                model.CityId = userAddress.CityId;
            }

            ViewBag.city = bookServiceRepository.GetAllCity().Select(x => new SelectListItem()
            {
                Text = x.CityName,
                Value = x.Id.ToString()
            });
            return View(model);
        }

        [HttpPost]
        public IActionResult GetUserData(MyDetailsViewModel myDetailsViewModel)
        {
            if (ModelState.IsValid)
            {
                userRegistrationRepository.UpdateServiceProviderData(myDetailsViewModel, ServiceProviderUserID);
                
            }
            return RedirectToAction("MySetting");
        }

        public bool ResetPassword(string oldPassword,string newPassword)
        {
            return loginRepository.ResetPassword(ServiceProviderUserID, oldPassword, newPassword);
        }

        public IActionResult GetServiceRequestData(string ServiceReqestId)
        {
            return Json(JsonConvert.SerializeObject(serviceRequestRepository.GetServiceDetailsforServiceProvider(Int32.Parse(ServiceReqestId))));
        }

        public string AcceptServiceRequest(string serviceRequeestId)
        {
            return serviceRequestRepository.AcceptServiceRequest(ServiceProviderUserID, Int32.Parse(serviceRequeestId));
        }

        public bool CompleteServiceRequest(string serviceRequeestId)
        {
            return serviceRequestRepository.CompletedService(Int32.Parse(serviceRequeestId));
        }
        public bool CanceledServiceRequest(string serviceRequeestId,string message)
        {
            return serviceRequestRepository.CancelServiceRequest(Int32.Parse(serviceRequeestId), message);
        }

        public bool SetUnblockCustomer(string userId)
        {
            return favouriteAndBlockRepository.UnblockCustomer(ServiceProviderUserID, Int32.Parse(userId));
        }
        public bool SetBlockCustomer(string userId)
        {
            return favouriteAndBlockRepository.BlockCustomer(ServiceProviderUserID, Int32.Parse(userId));
        }

    }
}
