using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FoodRecipeProvider.Services;
using FoodRecipeProvider.Models;
using FoodRecipeProvider.Data;
using Azure;


namespace FoodRecipeProvider.Controllers
{
    public class RecipesController : Controller
    {

        private readonly EdamamApiClient _edamamApiClient;
        private readonly RRSApiClient _rrsApiClient;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public RecipesController(EdamamApiClient edamamApiClient, RRSApiClient rrsApiClient,
                                ApplicationDbContext applicationDbContext, UserManager<AppUser> userManager)
        {
            _edamamApiClient = edamamApiClient;
            _rrsApiClient = rrsApiClient;
            _context = applicationDbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(SearchTags query)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var recomendedRecipesResponse = await _rrsApiClient.GetRecommendedRecipes(userId);
            List<string> recipeuris = new List<string>();
            if (recomendedRecipesResponse != null)
            {
                foreach(var recipe in recomendedRecipesResponse)
                {
                    recipeuris.Add(recipe.RecipeUri);
                }
            }

            var getRecipesByUrisResponse = await _edamamApiClient.GetRecipesByUrisAsync(recipeuris);
            if(getRecipesByUrisResponse != null)
            {
                foreach (var a in  getRecipesByUrisResponse.hits)
                {
                    await Console.Out.WriteLineAsync(a.recipe.label);
                }
            }

            var searchRecipesResponse = await _edamamApiClient.GetRecipesAsync(query);

            if (searchRecipesResponse != null)
            {
                var model = new SearchQueryModel
                {
                    SearchByQueryResponse = searchRecipesResponse,
                    SearchTags = new SearchTags(),
                    AvailableDietLabels = Enum.GetNames(typeof(DietLabelEnum)).ToList(),
                    AvailableHealthLabels = Enum.GetNames(typeof(HealthLabelEnum)).ToList(),
                    AvailableDishTypes = Enum.GetNames(typeof(DishTypeEnum)).ToList(),
                    AvailableCuisineTypes = Enum.GetNames(typeof(CuisineTypeEnum)).ToList(),
                    AvailableMealTypes = Enum.GetNames(typeof(MealTypeEnum)).ToList()
                };

                return View(model);
            }
            else return View("Error: Couldn't find recipes.");

        }


        public async Task<IActionResult> RecipeDetails(string recipeUri)
        {
            var responseRecipe = await _edamamApiClient.GetRecipeDetailsAsync(recipeUri);
            if (responseRecipe != null)
            {
                return View(responseRecipe);
            }
            else return View("Error: Couldn't find recipe.");
        }

           



        public async Task<IActionResult> SubmitRating(int recipeId, int rating)
        {
            string userId = _userManager.GetUserId(User);

            string message = $"You have rated the recipe with ID {recipeId} as {rating} stars.";
            ViewData["RatingMessage"] = message;
            return View("RecipeDetails");
        }

        [HttpPost]
        public async Task<IActionResult> RateRecipe(int rating, string recipeUri)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var recipe = _context.Recipes.FirstOrDefault(r => r.Uri == recipeUri);
            await Console.Out.WriteLineAsync("Rating" + rating);

            if (recipe != null && userId != null)
            {
                // Check if an existing rating entry exists for the user and recipe
                var existingRating = _context.UserRecipeRates
                    .FirstOrDefault(ur => ur.AppUserId == userId && ur.AppRecipeId == recipe.Id);
                

                if (existingRating != null)
                {
                    // Update the existing rating
                    existingRating.Rate = rating;
                }
                else
                {
                    // Create a new rating entry
                    var userRecipeRate = new UserRecipeRate
                    {
                        AppUserId = userId,
                        AppRecipeId = recipe.Id,
                        Rate = rating
                    };

                    _context.UserRecipeRates.Add(userRecipeRate);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("RecipeDetails", "Recipes", new { recipeUri = recipeUri });
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
