using CarServiceManager.Data;
using CarServiceManager.Helpers;
using CarServiceManager.Models;
using CarServiceManager.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace CarServiceManager.Pages.Vehicle
{
    public class MyVehiclesModel : PageModel
    {
        private readonly CarServiceContext _context;
        private readonly DbHelper _dbHelper;

        public MyVehiclesModel(CarServiceContext context, DbHelper dbHelper)
        {
            _context = context;
            _dbHelper = dbHelper;
        }

        [BindProperty]
        public Vehicles MyVehicles { get; set; }
        public List<vw_VehicleDetails> VehicleDetails { get; set; }
        [TempData]
        public string? NotificationMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            
            if (userId == null)
            {
                return RedirectToPage("/User/Login");
            }

            VehicleDetails = await _context.vw_VehicleDetails
                .Where(v => v.fkiUserId == userId)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnAddVehicleAsync()
        {
            if (!ModelState.IsValid)
            {
                NotificationMessage = "Please correct the errors in the form.";
                return Page();
            }

            var result = await _dbHelper.AddVehicleAsync(MyVehicles);
            NotificationMessage = result.Message;
            return RedirectToPage();
        }
    }
}
