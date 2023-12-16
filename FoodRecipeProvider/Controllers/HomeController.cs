using FoodRecipeProvider.Data;
using FoodRecipeProvider.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace FoodRecipeProvider.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _context = applicationDbContext;
            _signInManager = signInManager;
        }

        public IActionResult Preferences()
        {
            var viewModel = new UserPreferencesViewModel
            {
                AvailableCuisineTypes = Enum.GetNames(typeof(CuisineTypeEnum)).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Preferences(UserPreferencesViewModel viewModel)
        {
            // Get the current user's ID
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Get the existing preferences for the user
            var existingPreferences = _context.UserCuisineTypes
                .Where(uct => uct.UserId == userId)
                .ToList();

            // Loop through the selected cuisine types in the view model
            foreach (string cuisineType in viewModel.SelectedCuisineTypes)
            {
                // Check if the user already has a preference for this cuisine type
                var existingPreference = existingPreferences
                    .FirstOrDefault(uct => uct.CuisineName == cuisineType);

                if (existingPreference == null)
                {
                    // If not, add a new UserCuisineType
                    var userCuisineType = new UserCuisineType
                    {
                        UserId = userId,
                        CuisineName = cuisineType
                    };

                    _context.UserCuisineTypes.Add(userCuisineType);
                }
                // Remove the cuisine type from the existing preferences list
                else
                {
                    existingPreferences.Remove(existingPreference);
                }
            }

            // Remove any remaining cuisine types that were unchecked
            _context.UserCuisineTypes.RemoveRange(existingPreferences);

            _context.SaveChanges();

            return RedirectToAction("Preferences", "Home");
        }

        public IActionResult HealthLabels()
        {
            var viewModel = new UserHealthLabelsViewModel
            {
                AvailableHealthLabels = Enum.GetNames(typeof(HealthLabelEnum)).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult HealthLabels(UserHealthLabelsViewModel viewModel)
        {
            // Get the current user's ID
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Get the existing preferences for the user
            var existingPreferences = _context.UserHealthLabels
                .Where(uct => uct.UserId == userId)
                .ToList();

            // Loop through the selected cuisine types in the view model
            foreach (string healthLabel in viewModel.SelectedHealthLabels)
            {
                // Check if the user already has a preference for this cuisine type
                var existingPreference = existingPreferences
                    .FirstOrDefault(uct => uct.HealthLabelName == healthLabel);

                if (existingPreference == null)
                {
                    // If not, add a new UserCuisineType
                    var userHealthLabel = new UserHealthLabel
                    {
                        UserId = userId,
                        HealthLabelName = healthLabel
                    };

                    _context.UserHealthLabels.Add(userHealthLabel);
                }
                // Remove the cuisine type from the existing preferences list
                else
                {
                    existingPreferences.Remove(existingPreference);
                }
            }

            // Remove any remaining cuisine types that were unchecked
            _context.UserHealthLabels.RemoveRange(existingPreferences);

            _context.SaveChanges();

            return RedirectToAction("HealthLabels", "Home");
        }





    }
}