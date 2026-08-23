using CarServiceManager.Data;
using CarServiceManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarServiceManager.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CarServiceContext _context;

        public IndexModel(CarServiceContext context)
        {
            _context = context;
        }

        public Users? LoggedInUser { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var UserID = HttpContext.Session.GetInt32("UserID");

            LoggedInUser = await _context.Users
                .Where(u => u.pkiUserID == UserID)
                .FirstOrDefaultAsync();

            return Page();
        }
    }
}
