using FoodRecipeProvider.Data;
using FoodRecipeProvider.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FoodRecipeProvider.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _context = applicationDbContext;
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
        public IActionResult Preferences(UserPreferencesViewModel viewModel, string userId)
        {
            foreach (string cuisineType in viewModel.SelectedCuisineTypes) 
            {
               // UserCuisineType uct = new UserCuisineType(userId = userId, CuisineName = cuisineType);
              //  _context.UserCuisineTypes.Add();
              //start here
            }
            // Process the selected cuisine types (viewModel.SelectedCuisineTypes)
            // Save preferences to the database or perform other actions

            return RedirectToAction("Index", "Home"); // Redirect to the home page or another appropriate page
        }



    }
}