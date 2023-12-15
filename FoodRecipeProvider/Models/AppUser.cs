using Microsoft.AspNetCore.Identity;

namespace FoodRecipeProvider.Models
{
    public class AppUser: IdentityUser
    {
        //public List<UserPreferences> UserPreferences { get; set; }
    }
/*    public class UserPreferences
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public int CuisineTypeId { get; set; }
        public int DishTypeId { get; set; }
        public int MealTypeId { get; set; }
        public int HealthLabelId { get; set; }
        public int DietLabelId { get; set; }
    }*/
}
