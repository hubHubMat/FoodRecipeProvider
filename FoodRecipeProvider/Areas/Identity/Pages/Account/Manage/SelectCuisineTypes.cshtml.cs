using FoodRecipeProvider.Data;
using FoodRecipeProvider.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodRecipeProvider.Areas.Identity.Pages.Account.Manage
{
    public class SelectCuisineTypesModel : PageModel
    {
        private readonly ILogger<SelectCuisineTypesModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public SelectCuisineTypesModel(ILogger<SelectCuisineTypesModel> logger, ApplicationDbContext applicationDbContext, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = applicationDbContext;
            _userManager = userManager;
        }

        [BindProperty]
        public List<string>? SelectedCuisineTypes { get; set; }
        public List<string>? AvailableCuisineTypes { get; set; }

        public IActionResult OnGet()
        {
            
            AvailableCuisineTypes = Enum.GetNames(typeof(CuisineTypeEnum)).ToList();

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

            var existingPreferences = _context.UserCuisineTypes
                .Where(uct => uct.UserId == userId)
                .ToList();
            if(SelectedCuisineTypes != null && SelectedCuisineTypes.Any())
            foreach (string cuisineType in SelectedCuisineTypes)
            {
                var existingPreference = existingPreferences
                    .FirstOrDefault(uct => uct.CuisineName == cuisineType);

                if (existingPreference == null)
                {
                    var userCuisineType = new UserCuisineType
                    {
                        UserId = userId,
                        CuisineName = cuisineType
                    };

                    _context.UserCuisineTypes.Add(userCuisineType);
                }
                else
                {
                    existingPreferences.Remove(existingPreference);
                }
            }

            _context.UserCuisineTypes.RemoveRange(existingPreferences);

            _context.SaveChanges();

            return RedirectToPage("Preferences");
        }
    }
}
