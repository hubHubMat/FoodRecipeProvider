using Microsoft.Extensions.Options;
using FoodRecipeProvider.Models;
using NuGet.Packaging.Signing;

namespace FoodRecipeProvider.Services
{
    public class EdamamApiClient: IEdamamApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly EdamamApiOptions _apiOptions;

        public EdamamApiClient(HttpClient httpClient, IOptions<EdamamApiOptions> apiOptions)
        {
            _httpClient = httpClient;
            _apiOptions = apiOptions.Value;
        }

        public async Task<HttpResponseMessage> GetRecipesAsync(SearchTags query)
        {
            
            string endpoint = $"api/recipes/v2?type=public&app_id={_apiOptions.AppId}&app_key={_apiOptions.ApiKey}&q={query.keyword}";

            if (query.dietLabels != null)
            {
                foreach (var item in query.dietLabels)
                {
                    string diet = item.Replace('_', '-');
                    endpoint = string.Concat(endpoint, $"&diet={diet}");
                }
            }
            if (query.healthLabels != null)
            {
                foreach (var item in query.healthLabels)
                {
                    string health = item.Replace('_', '-');
                    endpoint = string.Concat(endpoint, $"&health={health}");
                }
            }
            if(query.cuisineType != null)
            {
                string cuisine = query.cuisineType.Replace('_', '-');
                endpoint = string.Concat(endpoint, $"&cuisineType={cuisine}");
            }
            if (query.dishType != null)
            {
                string dish = query.dishType.Replace('_', '-');
                endpoint = string.Concat(endpoint, $"&dishType={dish}");
            }
            if (query.mealType != null)
            {
                string meal = query.mealType.Replace('_', '-');
                endpoint = string.Concat(endpoint, $"&mealType={meal}");
            }
            await Console.Out.WriteLineAsync(endpoint);
            return await _httpClient.GetAsync(endpoint);
        }

        public async Task<HttpResponseMessage> GetRecipeDetailsAsync(string recipeUri)
        {
            string encodedUri = Uri.EscapeDataString(recipeUri);
            string endpoint = $"api/recipes/v2/by-uri?type=public&uri={encodedUri}&app_id={_apiOptions.AppId}&app_key={_apiOptions.ApiKey}";
            return await _httpClient.GetAsync(endpoint);
        }
    }

}
