using CarServiceManager.Data;
using CarServiceManager.Helpers;
using CarServiceManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarServiceManager.Pages.User
{
    public class RegistrationModel : PageModel
    {
        private readonly CarServiceContext _context;
        private readonly DbHelper _helper;

        public RegistrationModel(CarServiceContext context, DbHelper helper)
        {
            _context = context;
            _helper = helper;
        }

        [BindProperty]
        public Users Users { get; set; }
        [BindProperty]
        public string? ConfirmPassword { get; set; }
        [TempData]
        public string? NotificationMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if (Users.hashPassword == ConfirmPassword)
                    {
                        var result = await _helper.RegisterUserAsync(Users);
                        NotificationMessage = result.Message;
                    }
                }
                else
                {
                    NotificationMessage = "Please correct the errors in the form.";
                }
            }
            catch (Exception ex) 
            {
                NotificationMessage = "An error occurred during registration: " + ex.Message;
            }
            return RedirectToAction("/Login");
        }
    }
}
