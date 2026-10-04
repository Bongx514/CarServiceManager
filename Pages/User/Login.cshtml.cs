using CarServiceManager.Data;
using CarServiceManager.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarServiceManager.Pages.User
{
    public class LoginModel : PageModel
    {
        private readonly CarServiceContext _context;
        private readonly DbHelper _helper;

        public LoginModel(CarServiceContext context, DbHelper dbHelper)
        {
            _context = context;
            _helper = dbHelper;
        }

        [BindProperty]
        public string? EmailAddress { get; set; }
        [BindProperty]
        public string? Password { get; set; }
        [TempData]
        public string? NotificationMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                await _context.Database.OpenConnectionAsync();

                Console.WriteLine("DB Connected successfully");

                await _context.Database.CloseConnectionAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DB Connection failed: {ex.Message}");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(string.IsNullOrWhiteSpace(EmailAddress))
            {
                TempData["Message"] = "Email address is required.";
            }

            if(string.IsNullOrWhiteSpace(Password))
            {
                TempData["Message"] = "Password is required.";
            }

            var user = await _helper.LoginAsync(EmailAddress, Password);

            if (user == null)
            {
                TempData["Message"] = "Invalid email or password.";
                return Page();
            }

            NotificationMessage = "Login successful!";

            HttpContext.Session.SetInt32("UserID", user.pkiUserID);
            HttpContext.Session.SetString("UserName", user.userName ?? string.Empty);
            HttpContext.Session.SetString("UserEmail", user.userEmail ?? string.Empty);

            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.userName ?? string.Empty),
                        new Claim(ClaimTypes.Email, user.userEmail ?? string.Empty),
                        new Claim("UserID", user.pkiUserID.ToString())
                    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                "CookieAuth",
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddHours(1)
                });

            return RedirectToPage("/Index");
        }
    }
}
