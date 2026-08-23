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

        public Users? User { get; set; }

        public async Task<IActionResult> OnGet()
        {
            User = await _context.Users
                .Where(u => u.pkiUserID == 1)
                .FirstOrDefaultAsync();

            return Page();
        }
    }
}
