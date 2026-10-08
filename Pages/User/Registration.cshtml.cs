using CarServiceManager.Data;
using CarServiceManager.Helpers;
using CarServiceManager.Models;
using CarServiceManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarServiceManager.Pages.User
{
    public class RegistrationModel : PageModel
    {
        private readonly CarServiceContext _context;
        private readonly DbHelper _helper;
        private readonly AuthService _authService;

        public RegistrationModel(CarServiceContext context, DbHelper helper, AuthService authService)
        {
            _context = context;
            _helper = helper;
            _authService = authService;
        }

        [BindProperty]
        public RegisterRequest RegisterRequest { get; set; } = new();
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
                    var response = await _authService.RegisterAsync(RegisterRequest);

                    if (response.IsSuccessStatusCode)
                    {
                        NotificationMessage = "Registration successful! Please log in.";
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        NotificationMessage = $"Registration failed: {errorContent}";
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

            return RedirectToPage("/Login");
        }
    }
}
