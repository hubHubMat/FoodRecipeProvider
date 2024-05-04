using FoodRecipeProvider.Data;
using FoodRecipeProvider.Models;
using FoodRecipeProvider.Models.APIRecipeResponse;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FoodRecipeProvider.Services
{
    public class EdamamApiClient : IEdamamApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly EdamamApiOptions _apiOptions;
        private readonly ApplicationDbContext _context;

        public EdamamApiClient(HttpClient httpClient, IOptions<EdamamApiOptions> apiOptions,
            ApplicationDbContext applicationDbContext)
        {
            _httpClient = httpClient;
            _apiOptions = apiOptions.Value;
            _context = applicationDbContext;
        }

        public async Task<SearchByQueryResponse> GetRecipesAsync(SearchTags query)
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
            if (query.cuisineType != null)
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
            var response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject<SearchByQueryResponse>(content);

                return jsonResponse;
            }
            else
            {
                return null;
            }

        }

        public async Task<Recipe> GetRecipeDetailsAsync(string recipeUri)
        {
            string encodedUri = Uri.EscapeDataString(recipeUri);
            string endpoint = $"api/recipes/v2/by-uri?type=public&uri={encodedUri}&app_id={_apiOptions.AppId}&app_key={_apiOptions.ApiKey}";
            var response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var root = JsonConvert.DeserializeObject<SearchByUriResponse>(content);
                var recipe = root.hits.FirstOrDefault().recipe;
                var existingRecipe = await _context.Recipes
                     .FirstOrDefaultAsync(r => r.Uri == recipe.uri);

                if (existingRecipe != null)
                {
                    return recipe;
                }
                else if (existingRecipe != null && existingRecipe.RecipeIngredients != null)
                {
                    foreach (var ingredient in recipe.ingredients)
                    {
                        var dbIngredient = await _context.Ingredients
                            .FirstOrDefaultAsync(i => i.Name == ingredient.food);

                        existingRecipe.RecipeIngredients.Add(new RecipeIngredient
                        {
                            Ingredient = dbIngredient
                        });
                    }
                    return recipe;
                }
                else
                {
                    AppRecipe appRecipe = new AppRecipe { Label = recipe.label, Uri = recipe.uri };

                    foreach (var cuisineType in recipe.cuisineType)
                    {
                        var existingCuisineType = await _context.CuisineTypes
                            .FirstOrDefaultAsync(ct => ct.Name == cuisineType);

                        if (existingCuisineType == null)
                        {
                            existingCuisineType = new CuisineType { Name = cuisineType };
                            _context.CuisineTypes.Add(existingCuisineType);
                        }

                        appRecipe.RecipeCuisineTypes.Add(new RecipeCuisineTypes
                        {
                            CuisineType = existingCuisineType
                        });
                    }


                    foreach (var healthLabel in recipe.healthLabels)
                    {
                        var existingHealthLabel = await _context.HealthLabels
                            .FirstOrDefaultAsync(ct => ct.Name == healthLabel);

                        if (existingHealthLabel == null)
                        {
                            existingHealthLabel = new HealthLabel { Name = healthLabel };
                            _context.HealthLabels.Add(existingHealthLabel);
                        }

                        appRecipe.RecipeHealthLabels.Add(new RecipeHealthLabels
                        {
                            HealthLabel = existingHealthLabel
                        });
                    }

                    foreach (var dietLabel in recipe.dietLabels)
                    {
                        var existingDietLabel = await _context.DietLabels
                            .FirstOrDefaultAsync(ct => ct.Name == dietLabel);

                        if (existingDietLabel == null)
                        {
                            existingDietLabel = new DietLabel { Name = dietLabel };
                            _context.DietLabels.Add(existingDietLabel);
                        }

                        appRecipe.RecipeDietLabels.Add(new RecipeDietLabels
                        {
                            DietLabel = existingDietLabel
                        });
                    }

                    foreach (var ingredient in recipe.ingredients)
                    {
                        var existingIngredient = await _context.Ingredients
                            .FirstOrDefaultAsync(ct => ct.Name == ingredient.food);

                        if (existingIngredient == null)
                        {
                            existingIngredient = new IngredientDb { Name = ingredient.food };
                            _context.Ingredients.Add(existingIngredient);
                        }

                        appRecipe.RecipeIngredients.Add(new RecipeIngredient
                        {
                            Ingredient = existingIngredient
                        });
                    }


                    _context.Recipes.Add(appRecipe);
                    await _context.SaveChangesAsync();

                    return recipe;
                }
            }
            else return null;
        }

        public async Task<SearchByUrisResponse> GetRecipesByUrisAsync(List<string> recipeUris)
        {
            var endpoint = $"api/recipes/v2/by-uri?type=public&app_id={_apiOptions.AppId}&app_key={_apiOptions.ApiKey}&field=uri&field=label&field=image&field=source";

            foreach (var uri in recipeUris)
            {
                var encodedUri = Uri.EscapeDataString(uri);
                Console.WriteLine(encodedUri);
                endpoint = string.Concat(endpoint, $"&uri={encodedUri}");
            }
            await Console.Out.WriteLineAsync("looking for recipes with given URIs");
            await Console.Out.WriteLineAsync(endpoint);
            var response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject<SearchByUrisResponse>(content);

                return jsonResponse;
            }
            else
            {
                return null;
            }
        }
    }
}




