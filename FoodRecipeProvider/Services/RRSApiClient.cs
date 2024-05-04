using FoodRecipeProvider.Data;
using FoodRecipeProvider.Models.RRS;
using Newtonsoft.Json;


namespace FoodRecipeProvider.Services;

public class RRSApiClient : IRRSApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ApplicationDbContext _context;

    public RRSApiClient(HttpClient httpClient, ApplicationDbContext context)
    {
        _httpClient = httpClient;
        _context = context;
    }

    public async Task<List<RecommendedRecipe>> GetRecommendedRecipes(string userId)
    {

        var response = await _httpClient.PostAsJsonAsync("recommend", new { user_id = userId, top_n = 6 });

        // if (response.IsSuccessStatusCode)
        //{
        var responseBody = await response.Content.ReadAsStringAsync();
        var responseObject = JsonConvert.DeserializeObject<RecommendedRecipesResponse>(responseBody);

        return responseObject.RecommendedRecipes;
        //}
        /*        else
                {
                    throw new Exception($"Failed to get recommended recipes: {response.StatusCode}");
                }*/
    }
}