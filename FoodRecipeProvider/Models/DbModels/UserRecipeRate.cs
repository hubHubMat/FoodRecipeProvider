namespace FoodRecipeProvider.Models
{
    public class UserRecipeRate
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int AppRecipeId { get; set; }
        public AppRecipe AppRecipe { get; set; }
        public double Rate { get; set; }
    }
}
