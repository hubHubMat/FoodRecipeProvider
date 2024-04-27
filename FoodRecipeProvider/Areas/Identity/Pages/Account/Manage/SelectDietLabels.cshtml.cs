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
    public class SelectDietLabelsModel : PageModel
    {
        private readonly ILogger<SelectDietLabelsModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public SelectDietLabelsModel(ILogger<SelectDietLabelsModel> logger, ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public List<int>? SelectedDietLabelIds { get; set; }
        public List<DietLabel> AvailableDietLabels { get; set; } = new List<DietLabel>();

        public async Task<IActionResult> OnGetAsync()
        {
            // Load available diet labels from the database
            AvailableDietLabels = await _context.DietLabels.ToListAsync();

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
                // Retrieve existing user's diet label associations
                var existingUserDietLabels = await _context.UserDietLabels
                    .Where(udl => udl.AppUserId == userId)
                    .ToListAsync();

                // Update user's selected diet labels
                if (SelectedDietLabelIds != null)
                {
                    var selectedDietLabelIdsSet = new HashSet<int>(SelectedDietLabelIds);

                    // Add new associations
                    foreach (var dietLabelId in selectedDietLabelIdsSet)
                    {
                        if (!existingUserDietLabels.Any(udl => udl.DietLabelId == dietLabelId))
                        {
                            var newUserDietLabel = new UserDietLabel
                            {
                                AppUserId = userId,
                                DietLabelId = dietLabelId
                            };
                            _context.UserDietLabels.Add(newUserDietLabel);
                        }
                    }

                    // Remove associations that are not selected anymore
                    foreach (var existingUserDietLabel in existingUserDietLabels)
                    {
                        if (!selectedDietLabelIdsSet.Contains(existingUserDietLabel.DietLabelId))
                        {
                            _context.UserDietLabels.Remove(existingUserDietLabel);
                        }
                    }

                    // Save changes to the database
                    await _context.SaveChangesAsync();
                }

                return RedirectToPage("Preferences"); // Redirect to preferences page after successful update
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user's diet labels.");
                throw; // You may want to handle this more gracefully
            }
        }
    }
}
