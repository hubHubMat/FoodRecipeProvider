using FoodRecipeProvider.Data;
using FoodRecipeProvider.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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

            var userId = await _userManager.GetUserIdAsync(user);

            try
            {
                var existingUserHealthLabels = await _context.UserHealthLabels
                    .Where(uhl => uhl.AppUserId == userId)
                    .ToListAsync();

                if (SelectedHealthLabelIds != null)
                {
                    var selectedHealthLabelIdsSet = new HashSet<int>(SelectedHealthLabelIds);

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

                    foreach (var existingUserHealthLabel in existingUserHealthLabels)
                    {
                        if (!selectedHealthLabelIdsSet.Contains(existingUserHealthLabel.HealthLabelId))
                        {
                            _context.UserHealthLabels.Remove(existingUserHealthLabel);
                        }
                    }

                    await _context.SaveChangesAsync();
                }

                return RedirectToPage("Preferences");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user's health labels.");
                throw;
            }
        }
    }
}
