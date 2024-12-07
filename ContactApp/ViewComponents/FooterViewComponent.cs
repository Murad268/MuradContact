using ContactApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactApp.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {

        private readonly ContactService _contactService;

        public FooterViewComponent(ContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
           

            return View();
        }
    }
}
