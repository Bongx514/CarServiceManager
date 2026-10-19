using CarServiceManager.Data;
using CarServiceManager.Models;
using CarServiceManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarServiceManager.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly CarServiceContext _context;

        public IndexModel(CarServiceContext context)
        {
            _context = context;
        }

        public Users? LoggedInUser { get; set; }

        [TempData]
        public string? NotificationMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var UserID = HttpContext.Session.GetInt32("UserID");

            if (UserID == null)
            {
                return RedirectToPage("/User/Login");
            }

            LoggedInUser = await _context.Users
                .Where(u => u.pkiUserID == UserID)
                .FirstOrDefaultAsync();

            return Page();
        }
    }
}
