using Microsoft.AspNetCore.Identity;

namespace FoodRecipeProvider.Models;

    public class UserCuisineType
    {
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public int CuisineTypeId { get; set; }
    public CuisineType CuisineType { get; set; }
}
    public class UserHealthLabel
    {
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public int HealthLabelId { get; set; }
    public HealthLabel HealthLabel { get; set; }
}
    public class UserDietLabel
    {
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public int DietLabelId { get; set; }
    public DietLabel DietLabel { get; set; }
}