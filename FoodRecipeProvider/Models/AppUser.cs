using Microsoft.AspNetCore.Identity;

namespace FoodRecipeProvider.Models
{
    public class AppUser: IdentityUser
    {
        public List<string> HealthyTags { get; set; }
    }
}
