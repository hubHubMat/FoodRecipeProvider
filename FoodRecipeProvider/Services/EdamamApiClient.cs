using Microsoft.Extensions.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<HttpResponseMessage> GetRecipesAsync(string query)
        {
            string endpoint = $"search?q={query}&app_id={_apiOptions.AppId}&app_key={_apiOptions.ApiKey}";

            return await _httpClient.GetAsync(endpoint);
        }
        public async Task<HttpResponseMessage> GetRecipeDetailsAsync(string recipeUri)
        {
            string encodedUri = Uri.EscapeDataString(recipeUri);

            string endpoint = $"api/recipes/v2/by-uri?type=public&uri={encodedUri}&app_id={_apiOptions.AppId}&app_key={_apiOptions.ApiKey}";
            //string endpoint = $"search?r={apiUri}&app_id={_apiOptions.AppId}&app_key={_apiOptions.ApiKey}";
            return await _httpClient.GetAsync(endpoint);
        }
    }

}
