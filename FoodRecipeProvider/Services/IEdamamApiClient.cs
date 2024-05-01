using FoodRecipeProvider.Models;
using FoodRecipeProvider.Models.APIRecipeResponse;
namespace FoodRecipeProvider.Services
{
    public interface IEdamamApiClient
    {
        Task<SearchByQueryResponse> GetRecipesAsync(SearchTags query);
        Task<Recipe> GetRecipeDetailsAsync(string apiUrl);
        Task<SearchByUrisResponse> GetRecipesByUrisAsync(List<string> recipeUris);
    }

}
