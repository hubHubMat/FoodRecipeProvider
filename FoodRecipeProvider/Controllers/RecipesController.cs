using FoodRecipeProvider.Services;
using Microsoft.AspNetCore.Mvc;
using FoodRecipeProvider.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using FoodRecipeProvider.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FoodRecipeProvider.Controllers
{
    public class RecipesController : Controller
    {

        private readonly EdamamApiClient _edamamApiClient;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public RecipesController(EdamamApiClient edamamApiClient, ApplicationDbContext applicationDbContext,
                                UserManager<AppUser> userManager)
        {
            _edamamApiClient = edamamApiClient;
            _context = applicationDbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(SearchTags query)
        {

            var response = await _edamamApiClient.GetRecipesAsync(query);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var myDeserializedClass = JsonConvert.DeserializeObject<SearchByQueryResponse>(content);

                var model = new SearchQueryModel
                {
                    SearchByQueryResponse = myDeserializedClass,
                    SearchTags = new SearchTags(),
                    AvailableDietLabels = Enum.GetNames(typeof(DietLabelEnum)).ToList(),
                    AvailableHealthLabels = Enum.GetNames(typeof(HealthLabelEnum)).ToList(),
                    AvailableDishTypes = Enum.GetNames(typeof(DishTypeEnum)).ToList(),
                    AvailableCuisineTypes = Enum.GetNames(typeof(CuisineTypeEnum)).ToList(),
                    AvailableMealTypes = Enum.GetNames(typeof(MealTypeEnum)).ToList()
                };

                return View(model);
            }
            else
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> RecipeDetails(string recipeUri)
        {
            var response = await _edamamApiClient.GetRecipeDetailsAsync(recipeUri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var root = JsonConvert.DeserializeObject<SearchByUriResponse>(content);
                var recipe = root.hits.FirstOrDefault().recipe;
                var existingRecipe = await _context.Recipes
                     .FirstOrDefaultAsync(r => r.Uri == recipe.uri);

                if (existingRecipe != null)
                {
                    return View(recipe);
                }
                else
                {
                    AppRecipe appRecipe = new AppRecipe { Label = recipe.label, Uri = recipe.uri };

                    foreach (var cuisineType in recipe.cuisineType)
                    {
                        var existingCuisineType = await _context.CuisineTypes
                            .FirstOrDefaultAsync(ct => ct.Name == cuisineType);

                        if (existingCuisineType == null)
                        {
                            existingCuisineType = new CuisineType { Name = cuisineType };
                            _context.CuisineTypes.Add(existingCuisineType);
                        }

                        appRecipe.RecipeCuisineTypes.Add(new RecipeCuisineTypes
                        {
                            CuisineType = existingCuisineType
                        });
                    }

                    foreach (var healthLabel in recipe.healthLabels)
                    {
                        var existingHealthLabel = await _context.HealthLabels
                            .FirstOrDefaultAsync(ct => ct.Name == healthLabel);

                        if (existingHealthLabel == null)
                        {
                            existingHealthLabel = new HealthLabel { Name = healthLabel };
                            _context.HealthLabels.Add(existingHealthLabel);
                        }

                        appRecipe.RecipeHealthLabels.Add(new RecipeHealthLabels
                        {
                            HealthLabel = existingHealthLabel
                        });
                    }


                    _context.Recipes.Add(appRecipe);
                    await _context.SaveChangesAsync();

                    return View(recipe);
                }

            }
            else
            {
                return View("Error");
            }
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
