
using Helperland.Models;
using Helperland.Models.Data;
using Helperland.Models.ViewModel;
using Helperland.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;
using System.Net;
using System.Net.Mime;
using Helperland.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.DataProtection;
using Helperland.Security;

namespace Helperland.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRegistrationRepository userRegistrationRepository;
        private readonly IContactRepository _icontactRepository;
        private readonly ILoginRepository loginRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration configuration;
        
        public HomeController(IContactRepository icontactRepository,
                              IUserRegistrationRepository userRegistrationRepository,
                              ILoginRepository loginRepository, 
                              IHttpContextAccessor httpContextAccessor,
                              IConfiguration _configuration
                             
                              )
        {
            this.userRegistrationRepository = userRegistrationRepository;
            _icontactRepository = icontactRepository;
            this.loginRepository = loginRepository;
            _httpContextAccessor = httpContextAccessor;
            configuration = _configuration;
           
        }
        public IActionResult Index(Boolean isLoginOpen=false)
        {
            ViewBag.isLoginOpen = isLoginOpen;
            ViewBag.isOpen = false;
            ViewBag.isForgetPasswordOpen = false;
            ViewBag.Success = false;

          
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                if(contactViewModel.FirstName.Trim() !="" && contactViewModel.LastName.Trim() != "")
                   _icontactRepository.AddContactusFrom(contactViewModel);
                else
                {
                    ModelState.Clear();
                    return RedirectToAction("Contact");
                }
            }
            ModelState.Clear();
            return RedirectToAction("Contact");
        }
        public IActionResult FAQ()
        {
            return View();
        }
        public IActionResult Prices()
        {
            return View();
        }

        public IActionResult UserRegistration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserRegistration(UserRegistrationViewModel userRegistrationViewModel)
        {
            if (ModelState.IsValid)
            {

                if (userRegistrationViewModel.FirstName.Trim() == "" && userRegistrationViewModel.LastName.Trim() == "")
                {
                    ModelState.AddModelError("", "please enter correct data");
                }
                else
                {
                    ViewBag.Success = userRegistrationRepository.AddUser(userRegistrationViewModel);

                }

            }
            else {
                return View();
            }
            if (ViewBag.Success == true)
            {
                ModelState.Clear();

            }
            else
            {
                ModelState.AddModelError("", userRegistrationRepository.Message());
            }
            return View();
        }


        [HttpPost]

        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            { int userID = loginRepository.IsValidUser(loginViewModel);
                if (userID != -1)
                {
                    User user = loginRepository.GetUser(userID);
                    if(user != null)
                    {
                        CookieOptions options = new CookieOptions();
                        options.Expires = DateTime.Now.AddMinutes(20);
                        Response.Cookies.Append("userID", user.UserId.ToString(),options);
                        Response.Cookies.Append("username", user.FirstName,options);
                        Response.Cookies.Append("usertype", user.UserTypeId.ToString(),options);
                        HttpContext.Session.SetInt32("userID", user.UserId);
                        HttpContext.Session.SetString("username", user.FirstName);
                        HttpContext.Session.SetInt32("usertype", user.UserTypeId);
                    }
                    ModelState.Clear();
                }
                else
                {
                    ModelState.AddModelError("", loginRepository.Message());
                    ViewBag.isOpen = true;
                }

                
            }
            else
            {
                ViewBag.isOpen = true;
                ModelState.AddModelError("", "Enter valid Data");
            }
                

            return View("Index");
        }
         public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete("userID");
            Response.Cookies.Delete("username");
            Response.Cookies.Delete("usertype");
            return RedirectToAction("Index");
        }

        public IActionResult ServiceProviderSignUP()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ServiceProviderSignUP(UserRegistrationViewModel userRegistrationViewModel)
        {
            if (ModelState.IsValid)
            {
               
                if (userRegistrationViewModel.FirstName.Trim() == "" && userRegistrationViewModel.LastName.Trim() == "")
                {
                    ModelState.AddModelError("", "please enter correct data");
                }
                else
                {
                    ViewBag.Success = userRegistrationRepository.AddServiceProvider(userRegistrationViewModel);
                    
                }

            }
            else
            {
                return View();
            }
            if ((Boolean)ViewBag.Success)
            {
                ModelState.Clear();
                
            }
            else
            {
                ModelState.AddModelError("", userRegistrationRepository.Message());
            }
            return View();
        }

        [HttpPost]
        public IActionResult ForgrtPassword(LoginViewModel loginViewModel)
        {
            if (loginRepository.IsValidUserEmail(loginViewModel))
            {
                try
                {
                    int UserID = loginRepository.GetUserID(loginViewModel.Email);
                    string welcomeMessage = "wlcome to Helperland, below link is for reset password.   <br/>";
                    string path = "<a href=\"" +" https://" + _httpContextAccessor.HttpContext.Request.Host.Value + "/home/resetpassword/" + UserID.ToString() + " \" > Reset Password </a>";
                    MailHelper mailHelper = new MailHelper(configuration);
                    ViewBag.sendMail = mailHelper.Send(loginViewModel.Email,welcomeMessage+path);
                    ModelState.Clear();
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError("", loginRepository.Message());
                ViewBag.isForgetPasswordOpen = true;
            }

            
            
            return View("Index");
        }

        public IActionResult Resetpassword(int id)
        {
           return View();
        }
        [HttpPost]
        public IActionResult Resetpassword(ResetPasswordViewModel resetPasswordViewModel,int id)
        {
            if (ModelState.IsValid)
            {
                if (resetPasswordViewModel.Password.Equals(resetPasswordViewModel.Confirmpassword))
                {
                    if (loginRepository.ChangePassword(id,resetPasswordViewModel))
                    {
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", loginRepository.Message());
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Enter Same Password");
                }
            }
             
            return View();
        }
    }
}
