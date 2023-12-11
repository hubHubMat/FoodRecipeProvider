namespace FoodRecipeProvider.Services
{
    public interface IEdamamApiClient
    {
        Task<HttpResponseMessage> GetRecipesAsync(string query);
        Task<HttpResponseMessage> GetRecipeDetailsAsync(string apiUrl);
    }

}
