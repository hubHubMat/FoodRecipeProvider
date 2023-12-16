namespace FoodRecipeProvider.Models
{

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







    /*    public class MealType
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<UserPreferences> UserPreferences { get; set; }
        }

        public class DishType
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<UserPreferences> UserPreferences { get; set; }
        }
        public class HealthLabel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<UserPreferences> UserPreferences { get; set; }
        }

        public class DietLabel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<UserPreferences> UserPreferences { get; set; }
        }*/

}
