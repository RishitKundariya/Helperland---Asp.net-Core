using Helperland.Helpers;
using Helperland.Models.ViewModel.Admin;
using Helperland.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    [SessionHelper(UserTypeID: 3)]
    public class AdminController : Controller
    {
        private readonly IUserRegistrationRepository userRegistrationRepository;
        private readonly IServiceRequestRepository serviceRequestRepository;
        private readonly IBookServiceRepository bookServiceRepository;

        public AdminController(IUserRegistrationRepository userRegistrationRepository,IServiceRequestRepository serviceRequestRepository
                               ,IBookServiceRepository bookServiceRepository)
        {
            this.userRegistrationRepository = userRegistrationRepository;
            this.serviceRequestRepository = serviceRequestRepository;
            this.bookServiceRepository = bookServiceRepository;
        }
        [HttpPost]
        public IActionResult Index(ServiceHistoryAdminViewModel serchData)
        {
            ViewBag.city = bookServiceRepository.GetAllCity().Select(x => new SelectListItem()
            {
                Text = x.CityName,
                Value = x.Id.ToString()
            });
            ServiceHistoryAdminViewModel model = new ServiceHistoryAdminViewModel();
            model.ServiceHistory = serviceRequestRepository.GetServiceBySearch(serchData);
            return View(model);
        }

        public IActionResult Index()
        {
            ViewBag.city = bookServiceRepository.GetAllCity().Select(x => new SelectListItem()
            {
                Text = x.CityName,
                Value = x.Id.ToString()
            });
            ServiceHistoryAdminViewModel model = new ServiceHistoryAdminViewModel();
            model.ServiceHistory = serviceRequestRepository.GetAllServiceListForAdmin();
            return View(model);
        }
        public IActionResult UserManagement()
        {
            ServiceHistoryAdminViewModel model = new ServiceHistoryAdminViewModel();
            model.UserList = userRegistrationRepository.GetAllUser();
            return View(model);
        }
        [HttpPost]
        public IActionResult UserManagement(ServiceHistoryAdminViewModel serchData)
        {
            ServiceHistoryAdminViewModel model = new ServiceHistoryAdminViewModel();
            model.UserList = userRegistrationRepository.GetFilterUserList(serchData.UserSearchModel);
            return View(model);
        }

        public bool DeleteUser(string userId)
        {
            return userRegistrationRepository.DeleteUser(Int32.Parse(userId));
        }
        public bool ActivateUser(string userId)
        {
            return userRegistrationRepository.ActivateUser(Int32.Parse(userId));
        }
        public bool DeactivateUser(string userId)
        {
            return userRegistrationRepository.DeactivateUser(Int32.Parse(userId));
        }
        public IActionResult GetServiceRequestData(string ServiceReqestId)
        {

            return Json(JsonConvert.SerializeObject(serviceRequestRepository.GetServiceDetails(Int32.Parse(ServiceReqestId))));
        }
        public IActionResult GetServiceRequestDetailsForEdit(string ServiceReqestId)
        {

            return Json(JsonConvert.SerializeObject(serviceRequestRepository.GetServiceDetailsForAdmin(Int32.Parse(ServiceReqestId))));
        }

        public IActionResult EditService(ServiceHistoryAdminViewModel editData)
        {
            if (ModelState.IsValid)
            {
                serviceRequestRepository.UpdateServiceRequest(editData.EditServiceModel);
            }
       
            ViewBag.city = bookServiceRepository.GetAllCity().Select(x => new SelectListItem()
            {
                Text = x.CityName,
                Value = x.Id.ToString()
            });
            return RedirectToAction("Index");

        }

        public bool ApproveServiceProvider(string userId)
        {
            return userRegistrationRepository.ApproveServiceProvider(Int32.Parse(userId));
        }
        public bool DisApproveServiceProvider(string userId)
        {
            return userRegistrationRepository.DisApproveServiceProvider(Int32.Parse(userId));
        }

        public IActionResult GetRefundDetails(string serviceRequestId)
        {
            return Json(JsonConvert.SerializeObject(serviceRequestRepository.GetRefundDetails(Int32.Parse(serviceRequestId))));
        }

        public IActionResult RefundAmount(ServiceHistoryAdminViewModel refund)
        {

            serviceRequestRepository.RefundAmount(refund.RefundModel);
           return RedirectToAction("Index");
        }
    }
}
