using CarServiceManager.Data;
using CarServiceManager.Models;
using CarServiceManager.ViewModels;
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
        public List<vw_VehicleDetails>? MyVehicles { get; set; }
        public string? txtMakeName { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var UserEmail = HttpContext.Session.GetInt32("UserEmail");

            if (UserEmail == null)
            {
                return RedirectToPage("/User/Login");
            }

            LoggedInUser = await _context.Users
                .Where(u => u.pkiUserID == UserEmail)
                .FirstOrDefaultAsync();

            MyVehicles = await _context.vw_VehicleDetails
                .Where(u => u.fkiUserId == UserEmail)
                .ToListAsync();

            return Page();
        }
    }
}
