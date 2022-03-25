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
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult NewServiceRquest(string hasPate)
        {
            int userId = (int)_session.GetInt32("userID");
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
            return View(serviceRequestRepository.GetServiceRequestsNotAccepted(userId, hasPasts));
        }
        public IActionResult UpcomingServices()
        {
            int userId = (int)_session.GetInt32("userID");
            return View(serviceRequestRepository.GetServiceRequestsIsAccepted(userId));
        }
        public IActionResult ServiceHistory()
        {
            int userId = (int)_session.GetInt32("userID");

            return View(serviceRequestRepository.GetServiceHistoryForServiceProvider(userId));
        }
        public IActionResult MyRating(string Rating=null)
        {
            if(Rating == null || Int32.Parse(Rating)==0)
            {
                int userId = (int)_session.GetInt32("userID");
                ViewBag.selectRating = 0;
                return View(ratingRepository.GetRatingsForServiceProvider(userId));
                
            }
            else
            {
                int userId = (int)_session.GetInt32("userID");
                List<RatingsViewModel> temp = ratingRepository.GetRatingsForServiceProvider(userId);
                temp = temp.Where(x => x.Rating > Int32.Parse(Rating) -1 && x.Rating <= Int32.Parse(Rating)).ToList();
                ViewBag.selectRating = Int32.Parse(Rating);
                return View(temp);
            }
           
           
        }

        public IActionResult ServiceSchedule()
        {
            
           return View();
        }

        public IActionResult GetUpcomingServiceRequest()
        {
            int userId = (int)_session.GetInt32("userID");
            return new JsonResult(serviceRequestRepository.GetDateOfAcceptedAndUpcomingServiceRequest(userId));
        }
        public IActionResult BlockCustomer()
        {
            int userId = (int)_session.GetInt32("userID");
            return View(favouriteAndBlockRepository.GetListOfCustomer(userId));
        }
        public IActionResult MySetting()
        {
            int userId = (int)_session.GetInt32("userID");
            MyDetailsViewModel model = new MyDetailsViewModel();
            User user;
            user = userRegistrationRepository.GetUserById(userId);
            UserAddress userAddress = addressRepository.GetServiceProviderAddress(userId);
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
            int userId = (int)_session.GetInt32("userID");
            if (ModelState.IsValid)
            {
                userRegistrationRepository.UpdateServiceProviderData(myDetailsViewModel, userId);
                
            }
            return RedirectToAction("MySetting");
        }

        public bool ResetPassword(string oldPassword,string newPassword)
        {
            int userId = (int)_session.GetInt32("userID");
            return loginRepository.ResetPassword(userId, oldPassword, newPassword);
        }

        public IActionResult GetServiceRequestData(string ServiceReqestId)
        {
            return Json(JsonConvert.SerializeObject(serviceRequestRepository.GetServiceDetailsforServiceProvider(Int32.Parse(ServiceReqestId))));
        }

        public string AcceptServiceRequest(string serviceRequeestId)
        {
            int userId = (int)_session.GetInt32("userID");
            return serviceRequestRepository.AcceptServiceRequest(userId, Int32.Parse(serviceRequeestId));
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
            int LoginUserId = (int)_session.GetInt32("userID");
            return favouriteAndBlockRepository.UnblockCustomer(LoginUserId, Int32.Parse(userId));
        }
        public bool SetBlockCustomer(string userId)
        {
            int LoginUserId = (int)_session.GetInt32("userID");
            return favouriteAndBlockRepository.BlockCustomer(LoginUserId, Int32.Parse(userId));
        }

    }
}
