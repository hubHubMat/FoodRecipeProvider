using FoodRecipeProvider.Data;
using FoodRecipeProvider.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FoodRecipeProvider.Areas.Identity.Pages.Account.Manage
{
    public class SelectCuisineTypesModel : PageModel
    {
        private readonly ILogger<SelectCuisineTypesModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public SelectCuisineTypesModel(ILogger<SelectCuisineTypesModel> logger, ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public List<int>? SelectedCuisineTypeIds { get; set; }
        public List<CuisineType> AvailableCuisineTypes { get; set; } = new List<CuisineType>();

        public async Task<IActionResult> OnGetAsync()
        {
            AvailableCuisineTypes = await _context.CuisineTypes.ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userId = await _userManager.GetUserIdAsync(user);

            try
            {
                var existingUserCuisineTypes = await _context.UserCuisineTypes
                    .Where(uct => uct.AppUserId == userId)
                    .ToListAsync();

                if (SelectedCuisineTypeIds != null)
                {
                    var selectedCuisineTypeIdsSet = new HashSet<int>(SelectedCuisineTypeIds);

                    foreach (var cuisineTypeId in selectedCuisineTypeIdsSet)
                    {
                        if (!existingUserCuisineTypes.Any(uct => uct.CuisineTypeId == cuisineTypeId))
                        {
                            var newUserCuisineType = new UserCuisineType
                            {
                                AppUserId = userId,
                                CuisineTypeId = cuisineTypeId
                            };
                            _context.UserCuisineTypes.Add(newUserCuisineType);
                        }
                    }

                    foreach (var existingUserCuisineType in existingUserCuisineTypes)
                    {
                        if (!selectedCuisineTypeIdsSet.Contains(existingUserCuisineType.CuisineTypeId))
                        {
                            _context.UserCuisineTypes.Remove(existingUserCuisineType);
                        }
                    }

                    await _context.SaveChangesAsync();
                }

                return RedirectToPage("Preferences");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user's cuisine types.");
                throw;
            }
        }
    }
}
