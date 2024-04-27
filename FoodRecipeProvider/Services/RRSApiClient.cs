using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using FoodRecipeProvider.Models.RRS;


namespace FoodRecipeProvider.Services;

public class RRSApiClient: IRRSApiClient
{
    private readonly HttpClient _httpClient;

    public RRSApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<RecommendedRecipe>> GetRecommendedRecipes(string userId)
    {
        var response = await _httpClient.PostAsJsonAsync("recommend", new { user_id = userId, top_n = 6 });

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<RecommendedRecipesResponse>(responseBody);

            return responseObject.RecommendedRecipes;
        }
        else
        {
            throw new Exception($"Failed to get recommended recipes: {response.StatusCode}");
        }
    }
}