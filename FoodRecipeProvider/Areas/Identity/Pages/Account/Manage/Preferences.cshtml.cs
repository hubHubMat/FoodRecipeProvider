using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FoodRecipeProvider.Data;
using FoodRecipeProvider.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace FoodRecipeProvider.Areas.Identity.Pages.Account.Manage
{
    public class PreferencesModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<PreferencesModel> _logger;

        public PreferencesModel(
            UserManager<AppUser> userManager,
            ApplicationDbContext context,
            SignInManager<AppUser> signInManager,
            ILogger<PreferencesModel> logger)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
            _logger = logger;
        }
        public IList<UserCuisineType> ExistingCuisineTypes { get; set; }
        public IList<UserHealthLabel> ExistingHealthLabels { get; set; }
        public IList<UserDietLabel> ExistingDietLabels { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var userId = await _userManager.GetUserIdAsync(user);


            ExistingCuisineTypes = await _context.UserCuisineTypes
                .Where(uct => uct.UserId == userId)
                .ToListAsync();

            ExistingHealthLabels = await _context.UserHealthLabels
                .Where(uct => uct.UserId == userId)
                .ToListAsync();

            ExistingDietLabels = await _context.UserDietLabels
                .Where(uct => uct.UserId == userId)
                .ToListAsync();

            return Page();
        }
    }
}
