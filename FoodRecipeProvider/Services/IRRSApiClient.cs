using FoodRecipeProvider.Models;
using FoodRecipeProvider.Models.RRS;
using System.Threading.Tasks;

namespace FoodRecipeProvider.Services
{
    public interface IRRSApiClient
    {
        Task<List<RecommendedRecipe>> GetRecommendedRecipes(string userId);
    }
}
