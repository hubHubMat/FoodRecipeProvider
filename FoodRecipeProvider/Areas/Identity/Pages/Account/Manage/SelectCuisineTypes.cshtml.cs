using FoodRecipeProvider.Data;
using FoodRecipeProvider.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            // Load available cuisine types from the database
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

            // Get the user ID
            var userId = await _userManager.GetUserIdAsync(user);

            try
            {
                // Retrieve existing user's cuisine type associations
                var existingUserCuisineTypes = await _context.UserCuisineTypes
                    .Where(uct => uct.AppUserId == userId)
                    .ToListAsync();

                // Update user's selected cuisine types
                if (SelectedCuisineTypeIds != null)
                {
                    var selectedCuisineTypeIdsSet = new HashSet<int>(SelectedCuisineTypeIds);

                    // Add new associations
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

                    // Remove associations that are not selected anymore
                    foreach (var existingUserCuisineType in existingUserCuisineTypes)
                    {
                        if (!selectedCuisineTypeIdsSet.Contains(existingUserCuisineType.CuisineTypeId))
                        {
                            _context.UserCuisineTypes.Remove(existingUserCuisineType);
                        }
                    }

                    // Save changes to the database
                    await _context.SaveChangesAsync();
                }

                return RedirectToPage("Preferences"); // Redirect to preferences page after successful update
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user's cuisine types.");
                throw; // You may want to handle this more gracefully
            }
        }
    }
}
