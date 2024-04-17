using FoodRecipeProvider.Models;
namespace FoodRecipeProvider.Services
{
    public interface IEdamamApiClient
    {
        Task<HttpResponseMessage> GetRecipesAsync(SearchTags query);
        Task<HttpResponseMessage> GetRecipeDetailsAsync(string apiUrl);
    }

}
