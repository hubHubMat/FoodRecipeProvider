using FoodRecipeProvider.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodRecipeProvider.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<UserCuisineType> UserCuisineTypes { get; set; }
        public DbSet<UserHealthLabel> UserHealthLabels { get; set; }
        public DbSet<UserDietLabel> UserDietLabels { get; set; }
        public DbSet<UserRecipeRate> UserRecipeRates { get; set; }
        public DbSet<CuisineType> CuisineTypes { get; set; }
        public DbSet<MealType> MealTypes { get; set; }
        public DbSet<DishType> DishTypes { get; set; }
        public DbSet<HealthLabel> HealthLabels { get; set; }
        public DbSet<DietLabel> DietLabels { get; set; }
        public DbSet<IngredientDb> Ingredients { get; set; }
        public DbSet<AppRecipe> Recipes { get; set; }
        public DbSet<RecipeCuisineTypes> RecipeCuisineTypes { get; set; }
        public DbSet<RecipeHealthLabels> RecipeHealthLabels { get; set; }
        public DbSet<RecipeDietLabels> RecipeDietLabels { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<RecipeCuisineTypes>()
                .HasKey(rct => new { rct.AppRecipeId, rct.CuisineTypeId });
            modelBuilder.Entity<RecipeHealthLabels>()
                .HasKey(rct => new { rct.AppRecipeId, rct.HealthLabelId });
            modelBuilder.Entity<RecipeDietLabels>()
                .HasKey(rct => new { rct.AppRecipeId, rct.DietLabelId });
            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(ri => new { ri.AppRecipeId, ri.IngredientId });

            modelBuilder.Entity<UserDietLabel>()
                .HasKey(udl => new { udl.AppUserId, udl.DietLabelId });
            modelBuilder.Entity<UserHealthLabel>()
                .HasKey(uhl => new { uhl.AppUserId, uhl.HealthLabelId });
            modelBuilder.Entity<UserCuisineType>()
                .HasKey(uct => new { uct.AppUserId, uct.CuisineTypeId });


            base.OnModelCreating(modelBuilder);
        }
    }
}