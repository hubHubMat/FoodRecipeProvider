using FoodRecipeProvider.Models.RRS;

namespace FoodRecipeProvider.Services
{
    public interface IRRSApiClient
    {
        Task<List<RecommendedRecipe>> GetRecommendedRecipes(string userId);
    }
}
