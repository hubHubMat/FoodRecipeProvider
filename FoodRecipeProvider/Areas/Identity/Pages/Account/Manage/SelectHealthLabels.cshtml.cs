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
    public class SelectHealthLabelsModel : PageModel
    {
        private readonly ILogger<SelectHealthLabelsModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public SelectHealthLabelsModel(ILogger<SelectHealthLabelsModel> logger, ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public List<int>? SelectedHealthLabelIds { get; set; }
        public List<HealthLabel> AvailableHealthLabels { get; set; } = new List<HealthLabel>();

        public async Task<IActionResult> OnGetAsync()
        {
            // Load available health labels from the database
            AvailableHealthLabels = await _context.HealthLabels.ToListAsync();

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
                // Retrieve existing user's health label associations
                var existingUserHealthLabels = await _context.UserHealthLabels
                    .Where(uhl => uhl.AppUserId == userId)
                    .ToListAsync();

                // Update user's selected health labels
                if (SelectedHealthLabelIds != null)
                {
                    var selectedHealthLabelIdsSet = new HashSet<int>(SelectedHealthLabelIds);

                    // Add new associations
                    foreach (var healthLabelId in selectedHealthLabelIdsSet)
                    {
                        if (!existingUserHealthLabels.Any(uhl => uhl.HealthLabelId == healthLabelId))
                        {
                            var newUserHealthLabel = new UserHealthLabel
                            {
                                AppUserId = userId,
                                HealthLabelId = healthLabelId
                            };
                            _context.UserHealthLabels.Add(newUserHealthLabel);
                        }
                    }

                    // Remove associations that are not selected anymore
                    foreach (var existingUserHealthLabel in existingUserHealthLabels)
                    {
                        if (!selectedHealthLabelIdsSet.Contains(existingUserHealthLabel.HealthLabelId))
                        {
                            _context.UserHealthLabels.Remove(existingUserHealthLabel);
                        }
                    }

                    // Save changes to the database
                    await _context.SaveChangesAsync();
                }

                return RedirectToPage("Preferences"); // Redirect to preferences page after successful update
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user's health labels.");
                throw; // You may want to handle this more gracefully
            }
        }
    }
}
