using FoodRecipeProvider.Data;
using FoodRecipeProvider.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodRecipeProvider.Areas.Identity.Pages.Account.Manage
{
    public class SelectDietLabelsModel : PageModel
    {
        private readonly ILogger<SelectCuisineTypesModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public SelectDietLabelsModel(ILogger<SelectCuisineTypesModel> logger, ApplicationDbContext applicationDbContext, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = applicationDbContext;
            _userManager = userManager;
        }

        [BindProperty]
        public List<string>? SelectedDietLabels { get; set; }
        public List<string>? AvailableDietLabels { get; set; }

        public IActionResult OnGet()
        {

            AvailableDietLabels = Enum.GetNames(typeof(DietLabelEnum)).ToList();

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

            var existingPreferences = _context.UserDietLabels
                .Where(uct => uct.UserId == userId)
                .ToList();
            if (SelectedDietLabels != null && SelectedDietLabels.Any())
                foreach (string DietLabel in SelectedDietLabels)
                {
                    var existingPreference = existingPreferences
                        .FirstOrDefault(uct => uct.DietLabelName == DietLabel);

                    if (existingPreference == null)
                    {
                        var userDietLabel = new UserDietLabel
                        {
                            UserId = userId,
                            DietLabelName = DietLabel
                        };

                        _context.UserDietLabels.Add(userDietLabel);
                    }
                    else
                    {
                        existingPreferences.Remove(existingPreference);
                    }
                }

            _context.UserDietLabels.RemoveRange(existingPreferences);

            _context.SaveChanges();

            return RedirectToPage("Preferences");
        }
    }
}
