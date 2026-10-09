using CarServiceManager.Data;
using CarServiceManager.Helpers;
using CarServiceManager.Models;
using CarServiceManager.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CarServiceManager.Pages.User
{
    public class LoginModel : PageModel
    {
        private readonly CarServiceContext _context;
        private readonly DbHelper _helper;
        private readonly AuthService _authService;

        public LoginModel(CarServiceContext context, DbHelper dbHelper, AuthService authService)
        {
            _context = context;
            _helper = dbHelper;
            _authService = authService;
        }

        [BindProperty]
        public LoginRequest LoginRequest { get; set; } = new LoginRequest();
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
            if(string.IsNullOrWhiteSpace(LoginRequest.userEmail))
            {
                TempData["Message"] = "Email address is required.";
            }

            if(string.IsNullOrWhiteSpace(LoginRequest.password))
            {
                TempData["Message"] = "Password is required.";
            }

            var response = await _authService.LoginAsync(LoginRequest);

            if (string.IsNullOrEmpty(response.Token))
            {
                TempData["Message"] = "Invalid email or password.";
                return Page();
            }
            else
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.userEmail == LoginRequest.userEmail);

                if (user == null)
                {
                    TempData["Message"] = "User not found.";
                    return Page();
                }

                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(response.Token);

                HttpContext.Session.SetInt32("UserID", int.Parse(jwt.Claims.First(c => c.Type == "nameid").Value));
                HttpContext.Session.SetString("UserName", jwt.Claims.First(c => c.Type == "unique_name").Value);
                HttpContext.Session.SetString("UserEmail", jwt.Claims.First(c => c.Type == "email").Value);

                if (user.pkiUserID != int.Parse(jwt.Claims.First(c => c.Type == "nameid").Value))
                {
                    TempData["Message"] = "User ID mismatch.";
                    await HttpContext.SignOutAsync("CookieAuth");
                    HttpContext.Session.Clear();
                    return Page();
                }

                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, jwt.Claims.First(c => c.Type == "unique_name").Value),
                        new Claim(ClaimTypes.Email, jwt.Claims.First(c => c.Type == "email").Value),
                        new Claim("UserID", jwt.Claims.First(c => c.Type == "nameid").Value)
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
}
