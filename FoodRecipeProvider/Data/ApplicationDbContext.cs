using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using FoodRecipeProvider.Models;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;

namespace FoodRecipeProvider.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<UserCuisineType> UserCuisineTypes { get; set; }
        public DbSet<UserHealthLabel> UserHealthLabels { get; set; }
/*        public DbSet<UserPreferences> UserPreferences { get; set; }
        public DbSet<MealType> MealTypes { get; set; }
        public DbSet<CuisineType> CuisineTypes { get; set; }
        public DbSet<DishType> DishTypes { get; set; }
        public DbSet<HealthLabel> HealthLabels { get; set; }
        public DbSet<DietLabel> DietLabels { get; set; }*/

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        /*protected override void OnModelCreating(ModelBuilder builder)
        {
            List<string> cuisineTypeStrings = Enum.GetNames(typeof(CuisineTypeEnum)).ToList();

            for (int i = 0; i<cuisineTypeStrings.Count; i++)
            {
                var value = builder.Entity<CuisineType>().HasData(new CuisineType { Id = i, cuisineTypeStrings[i] });  
            }
        }*/
    }
}