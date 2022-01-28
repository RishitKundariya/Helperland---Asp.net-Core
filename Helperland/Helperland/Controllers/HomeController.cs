
using Helperland.Models;
using Helperland.Models.Data;
using Helperland.Models.ViewModel;
using Helperland.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ContactRepository _contactRepository;
        public HomeController(ContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public IActionResult Index()
        {
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
                _contactRepository.AddContactusFrom(contactViewModel);
            }
            return View();
        }
        public IActionResult FAQ()
        {
            return View();
        }
        public IActionResult Prices()
        {
            return View();
        }


    }
}
