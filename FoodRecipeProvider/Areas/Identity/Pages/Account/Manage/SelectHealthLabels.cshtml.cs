using FoodRecipeProvider.Data;
using FoodRecipeProvider.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodRecipeProvider.Areas.Identity.Pages.Account.Manage
{
    public class SelectHealthLabelsModel : PageModel
    {
        private readonly ILogger<SelectCuisineTypesModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public SelectHealthLabelsModel(ILogger<SelectCuisineTypesModel> logger, ApplicationDbContext applicationDbContext, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = applicationDbContext;
            _userManager = userManager;
        }

        [BindProperty]
        public List<string>? SelectedHealthLabels { get; set; }
        public List<string>? AvailableHealthLabels { get; set; }

        public IActionResult OnGet()
        {

            AvailableHealthLabels = Enum.GetNames(typeof(HealthLabelEnum)).ToList();

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

            var existingPreferences = _context.UserHealthLabels
                .Where(uct => uct.UserId == userId)
                .ToList();
            if (SelectedHealthLabels != null && SelectedHealthLabels.Any())
                foreach (string healthLabel in SelectedHealthLabels)
                {
                    var existingPreference = existingPreferences
                        .FirstOrDefault(uct => uct.HealthLabelName == healthLabel);

                    if (existingPreference == null)
                    {
                        var userHealthLabel = new UserHealthLabel
                        {
                            UserId = userId,
                            HealthLabelName = healthLabel
                        };

                        _context.UserHealthLabels.Add(userHealthLabel);
                    }
                    else
                    {
                        existingPreferences.Remove(existingPreference);
                    }
                }

            _context.UserHealthLabels.RemoveRange(existingPreferences);

            _context.SaveChanges();

            return RedirectToPage("Preferences");
        }
    }
}
