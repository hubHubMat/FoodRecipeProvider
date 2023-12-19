namespace FoodRecipeProvider.Models.Models
{
    public class UserRecipeRating
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string RecipeUrl { get; set; }
        public float Rate { get; set; }
    }
}
