using Helperland.Helpers;
using Helperland.Models.Data;
using Helperland.Models.ViewModel.BookService;
using Helperland.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Helperland.Controllers
{
    [SessionHelper(UserTypeID: 1)]
    public class CustomerController : Controller
    {
        private readonly IAddressRepository addressRepository;
        private readonly IBookServiceRepository bookServiceRepository;
        private readonly IUserRegistrationRepository userRegistrationRepository;
        private readonly IServiceRequestRepository serviceRequestRepository;
        private readonly IRatingRepository ratingRepository;
        private readonly ILoginRepository loginRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public CustomerController(IAddressRepository addressRepository,IBookServiceRepository bookServiceRepository,
                                  IUserRegistrationRepository userRegistrationRepository,IServiceRequestRepository serviceRequestRepository,
                                  IRatingRepository ratingRepository,ILoginRepository loginRepository,
                                  IHttpContextAccessor httpContextAccessor)
        {
            this.bookServiceRepository = bookServiceRepository;
            this.addressRepository = addressRepository;
            this.userRegistrationRepository = userRegistrationRepository;
            this.serviceRequestRepository = serviceRequestRepository;
            this.ratingRepository = ratingRepository;
            this.loginRepository = loginRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            int userId=(int) _session.GetInt32("userID");
            return View(serviceRequestRepository.GetServicesByUserId(userId) );
        }

        public IActionResult Dashbord()
        {
            ViewBag.city = bookServiceRepository.GetAllCity().Select(x => new SelectListItem()
            {
                Text = x.CityName,
                Value = x.Id.ToString()
            });
            return View();
        }

        public IActionResult GetAddress(string userID)
        {
            List<AddressViewModel> addresses = addressRepository.GetAddress(Int32.Parse(userID));
            if (addresses == null)
            {
                Json(false);
            }
            return Json(JsonConvert.SerializeObject(addresses));

        }
        public IActionResult GetAddressById(string AddressID)
        {
            AddressViewModel addresses = addressRepository.GetAddressById(Int32.Parse(AddressID));
            if (addresses == null)
            {
                Json(false);
            }
            return Json(JsonConvert.SerializeObject(addresses));

        }
        [HttpPost]
        public IActionResult UpdateAddress([FromBody] AddressViewModel address)
        {
            if (addressRepository.UpdateAddress(address))
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }

        }

        [HttpPost]
        public IActionResult AddNewAddress([FromBody] AddressViewModel address)
        {
            if (addressRepository.SetAddress(address))
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }

        }
        [HttpPost]
        public IActionResult DeleteAddress(string AddressId)
        {
            if (addressRepository.DeleteAddress(Int32.Parse(AddressId)))
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }

        }

        public IActionResult GetUserById(string userId)
        {
            User user;
            user = userRegistrationRepository.GetUserById(Int32.Parse(userId));
            if (user == null)
            {
                Json(false);
            }
            int lan = 0;
            if (user.LanguageId != null)
            {
                lan = (int)user.LanguageId;
            }
            var u = new
            {
                userId = user.UserId,
                firstName=user.FirstName,
                lastName=user.LastName,
                month = Convert.ToDateTime(user.DateOfBirth).Month,
                day = Convert.ToDateTime(user.DateOfBirth).Day,
                year = Convert.ToDateTime(user.DateOfBirth).Year,
                email = user.Email,
                mobileNumber = user.Mobile,
                language = lan
            };
            return Json(JsonConvert.SerializeObject(u));
        }

        [HttpPost]
        public IActionResult UpdateUserData(string FirstName,string LastName,string DOB,string MobileNumber, string UserId, string LanguageId)
        {
            var i = userRegistrationRepository.ChangeUserData(FirstName, LastName, DOB, MobileNumber, Int32.Parse(UserId), Int32.Parse(LanguageId));
            return Json(i);
        }

        public IActionResult GetServiceRequestData(string ServiceReqestId)
        {
            
            return Json(JsonConvert.SerializeObject(serviceRequestRepository.GetServiceDetails(Int32.Parse(ServiceReqestId))));
        }

        public string GetServiceDate(string serviceReqestId)
        {

            return serviceRequestRepository.GetServiceDate(Int32.Parse(serviceReqestId));
        }

        public bool UpdateServiceDate(string serviceDate,string serviceProviderId)
        {
            return serviceRequestRepository.UpdateServiceDate(Int32.Parse(serviceProviderId),Convert.ToDateTime(serviceDate));
        }
        public bool CancelService(string serviceRequestId, string message)
        {
            return serviceRequestRepository.CancelService(Int32.Parse(serviceRequestId),message);
        }

        public IActionResult ServiceHistory()
        {
            int userId = (int)_session.GetInt32("userID");
            return View(serviceRequestRepository.GetServicesHistoryByUserId(userId));
        }

        public bool AddRating(string ServiceReqestId, string onTime, string friendly, string qualityOfService,string feedBack)
        {
            return ratingRepository.AddRating(Int32.Parse(ServiceReqestId), decimal.Parse(onTime), decimal.Parse(friendly), decimal.Parse(qualityOfService), feedBack);
        }

        public bool ResetPassword(string oldPassword, string newPassword)
        {
            int userId= (int)_session.GetInt32("userID");
            
            return loginRepository.ResetPassword(userId, oldPassword, newPassword);
        }
    }
}
