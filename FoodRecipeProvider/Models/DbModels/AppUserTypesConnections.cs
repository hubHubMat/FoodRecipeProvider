namespace FoodRecipeProvider.Models;

    public class UserCuisineType
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CuisineName { get; set; }
    }
    public class UserHealthLabel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string HealthLabelName { get; set; }
    }
    public class UserDietLabel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string DietLabelName { get; set; }
    }