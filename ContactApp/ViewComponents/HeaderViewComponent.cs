using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ContactApp.Services;

namespace ContactApp.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ContactService _contactService;

        public HeaderViewComponent(ContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var contactCount = _contactService.GetContacts(1).Count;

            return View(contactCount);
        }
    }
}
