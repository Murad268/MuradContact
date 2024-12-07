using ContactApp.Models;
using ContactApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
namespace ContactApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactService _contactService;

        public ContactController(ContactService contactService)
        {
            _contactService = contactService;
        }
        public IActionResult Index()
        {
            var contacts = _contactService.GetContacts(1);
            ViewBag.Contacts = contacts;
            ViewData["ContactCount"] = contacts.Count;
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Store(IFormFile image, string name, string surname, string email, string phone)
        {
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string uniqueFileName = null;
            if (image != null)
            {
                uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }

            _contactService.AddContactToDatabase(1, name, surname, email, phone, uniqueFileName);

            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            var contact = _contactService.GetContactById(id, 1);

            if (contact == null)
            {
                return NotFound("Contact not found!");
            }

            ViewBag.contact = contact;
            return View();
        }


        [HttpPost]
        public IActionResult Update(int id, string name, string surname, string email, string phone, IFormFile image)
        {
            var contact = _contactService.GetContactById(id, 1);
            if (contact == null)
            {
                return NotFound("Contact not found!");
            }

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            string uniqueFileName = contact.ImagePath;

            if (image != null)
            {
                if (!string.IsNullOrEmpty(contact.ImagePath))
                {
                    string oldFilePath = Path.Combine(uploadsFolder, contact.ImagePath);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath); 
                    }
                }

                uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }

            if (name == contact.Name &&
                surname == contact.Surname &&
                email == contact.Email &&
                phone == contact.Phone &&
                uniqueFileName == contact.ImagePath)
            {
                return RedirectToAction("Index");
            }

            _contactService.UpdateContactInDatabase(id, 1, name, surname, email, phone, uniqueFileName);

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var contact = _contactService.GetContactById(id, 1);
            if (contact == null)
            {
                return NotFound("Contact not found!");
            }

            if (!string.IsNullOrEmpty(contact.ImagePath))
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                string filePath = Path.Combine(uploadsFolder, contact.ImagePath);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath); 
                }
            }

            _contactService.DeleteContactFromDatabase(id, 1);

            return RedirectToAction("Index");
        }


        public IActionResult View(int id)
        {
            var contact = _contactService.GetContactById(id,1);
            ViewBag.contact = contact;

            return View(); 
        }


   
    }
}
