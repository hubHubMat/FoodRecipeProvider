using Newtonsoft.Json;

namespace FoodRecipeProvider.Models.RRS
{
    public class RecommendedRecipesResponse
    {
        [JsonProperty("recommended_recipes")]
        public List<RecommendedRecipe> RecommendedRecipes { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }

    public class RecommendedRecipe
    {
        [JsonProperty("recipe_uri")]
        public string RecipeUri { get; set; }

        [JsonProperty("score")]
        public double Score { get; set; }
    }


}
