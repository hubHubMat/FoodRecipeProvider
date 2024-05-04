using FoodRecipeProvider.Data;
using FoodRecipeProvider.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FoodRecipeProvider.Areas.Identity.Pages.Account.Manage
{
    public class PreferencesModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PreferencesModel> _logger;

        public PreferencesModel(
            UserManager<AppUser> userManager,
            ApplicationDbContext context,
            ILogger<PreferencesModel> logger)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }

        public List<CuisineType> UserCuisineTypes { get; set; } = new List<CuisineType>();
        public List<DietLabel> UserDietLabels { get; set; } = new List<DietLabel>();
        public List<HealthLabel> UserHealthLabels { get; set; } = new List<HealthLabel>();

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            UserCuisineTypes = await _context.UserCuisineTypes
                .Where(uct => uct.AppUserId == user.Id)
                .Select(uct => uct.CuisineType)
                .ToListAsync();

            UserDietLabels = await _context.UserDietLabels
                .Where(udl => udl.AppUserId == user.Id)
                .Select(udl => udl.DietLabel)
                .ToListAsync();

            UserHealthLabels = await _context.UserHealthLabels
                .Where(uhl => uhl.AppUserId == user.Id)
                .Select(uhl => uhl.HealthLabel)
                .ToListAsync();

            return Page();
        }
    }
}
