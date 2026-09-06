using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarServiceManager.Pages.User
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            HttpContext.Session.Clear();
            TempData.Clear();
            return RedirectToPage("/Index");
        }
    }
}
