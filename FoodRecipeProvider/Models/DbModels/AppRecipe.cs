namespace FoodRecipeProvider.Models
{
    public class AppRecipe
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Uri { get; set; }
        public ICollection<RecipeCuisineTypes> RecipeCuisineTypes { get; set; } = new List<RecipeCuisineTypes>();
        public ICollection<RecipeHealthLabels> RecipeHealthLabels { get; set; } = new List<RecipeHealthLabels>();
        public ICollection<RecipeDietLabels> RecipeDietLabels { get; set; } = new List<RecipeDietLabels>();


    }
}
