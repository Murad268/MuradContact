using ContactApp.Models;
using ContactApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ContactApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ContactService _contactService;

    
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ContactService contactService)
        {
            _logger = logger;
            _contactService = contactService;
        }


        public IActionResult Index()
        {

            var contacts = _contactService.GetContacts(1);
            ViewBag.Contacts = contacts;
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
