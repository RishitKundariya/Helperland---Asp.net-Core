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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    [SessionHelper(UserTypeID:1)]
    public class BookServiceController : Controller
    {
        private readonly IBookServiceRepository bookServiceRepository;
        private readonly IAddressRepository addressRepository;
        public BookServiceController(IBookServiceRepository bookServiceRepository, IAddressRepository addressRepository)
        {
            this.bookServiceRepository = bookServiceRepository;
            this.addressRepository = addressRepository;
        }
        public IActionResult Index()
        {

            ViewBag.city= bookServiceRepository.GetAllCity().Select(x => new SelectListItem()
            {
                Text = x.CityName,
                Value = x.Id.ToString()
            });
           
            return View();
        }
        [HttpPost]
        public JsonResult CheckPincode(string Pincode)
        {
            if(Pincode.Length > 7 || ! Regex.IsMatch(Pincode, @"^[0-9]{6}$"))
            {
                return Json( false);
            }
            else
            {
                if (bookServiceRepository.CheckAvailabilityOfService(Pincode))
                {
                    return Json(true);
                }
                else
                    return Json("We are not providing service in this area. We’ll notify you if any helper would start working near your area.");
            }
        }

        [HttpPost]
        public IActionResult AddAddress([FromBody] AddressViewModel address)
        {
            if(address.UserId != null && address.AddressLine1 != null && address.AddressLine2 != null && address.CityId != null)
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
            else
            {
                return Json(false);
            }
           
        }
        public IActionResult GetAddress(string userID)
        {
            List<AddressViewModel> addresses = addressRepository.GetAddress(Int32.Parse(userID));
            if (addresses == null)
            {
                Json(false);
            }
               return Json( JsonConvert.SerializeObject(addresses));
            
        }

        [HttpPost]
        public IActionResult BookServiceRequest(BookServiceViewModel bookServiceViewModel)
        {
            ViewBag.ServiceRequestID = bookServiceRepository.BookService(bookServiceViewModel);
            ViewBag.SuccessRequest = true;
            ViewBag.city = bookServiceRepository.GetAllCity().Select(x => new SelectListItem()
            {
                Text = x.CityName,
                Value = x.Id.ToString()
            });
            return View("Index");
        }

    }
}
