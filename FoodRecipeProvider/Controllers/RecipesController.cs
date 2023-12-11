using FoodRecipeProvider.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodRecipeProvider.Models;
using Newtonsoft.Json;
using Microsoft.DotNet.MSIdentity.Shared;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FoodRecipeProvider.Controllers
{
    public class RecipesController : Controller
    {
        private readonly EdamamApiClient _edamamApiClient;
        public RecipesController(EdamamApiClient edamamApiClient)
        {
            _edamamApiClient = edamamApiClient;
        }

        // GET: RecipesController
        public async Task<IActionResult> Index(string query)
        {
           /* string query = "soup";*/ // The search query for recipes
            var response = await _edamamApiClient.GetRecipesAsync(query);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(content);

                return View(myDeserializedClass);
            }
            else
            {
                return View("Error");
            }
        }

        // In RecipesController.cs
        public async Task<IActionResult> RecipeDetails(string recipeUri)
        {
            var response = await _edamamApiClient.GetRecipeDetailsAsync(recipeUri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var root = JsonConvert.DeserializeObject<Root>(content);
                var recipe = root.hits.FirstOrDefault().recipe;

                return View(recipe);
            }
            else
            {
                return View("Error");
            }
        }



    }
}
