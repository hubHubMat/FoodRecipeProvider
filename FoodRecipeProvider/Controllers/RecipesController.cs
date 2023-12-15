using FoodRecipeProvider.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodRecipeProvider.Models;
using Newtonsoft.Json;
using Microsoft.DotNet.MSIdentity.Shared;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Diagnostics;

namespace FoodRecipeProvider.Controllers
{
    public class RecipesController : Controller
    {
        private readonly EdamamApiClient _edamamApiClient;
        public RecipesController(EdamamApiClient edamamApiClient)
        {
            _edamamApiClient = edamamApiClient;
        }

        public async Task<IActionResult> Index(string query)
        {
            var response = await _edamamApiClient.GetRecipesAsync(query);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var myDeserializedClass = JsonConvert.DeserializeObject<SearchByQueryResponse>(content);

                return View(myDeserializedClass);
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

                return View(recipe);
            }
            else
            {
                return View("Error");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
