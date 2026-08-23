using CarServiceManager.Data;
using CarServiceManager.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarServiceManager.Pages.User
{
    public class LoginModel : PageModel
    {
        private readonly CarServiceContext _context;
        private readonly DbHelper _helper;

        public LoginModel(CarServiceContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string? EmailAddress { get; set; }
        [BindProperty]
        public string? Password { get; set; }
        [TempData]
        public string? NotificationMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(string.IsNullOrWhiteSpace(EmailAddress))
            {
                NotificationMessage = "Email address is required.";
            }

            if(string.IsNullOrWhiteSpace(Password))
            {
                NotificationMessage = "Password is required.";
            }

            try
            {
                var user = await _helper.LoginAsync(EmailAddress, Password);

                if (user == null)
                {
                    NotificationMessage = "Invalid email or password.";
                    return Page();
                }
                else
                {
                    NotificationMessage = "Login successful!";
                }

                return RedirectToPage("/Home");

            }
            catch (Exception ex)
            {
                NotificationMessage = "An error occurred during login: " + ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
