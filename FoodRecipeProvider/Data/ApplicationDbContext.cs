using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FoodRecipeProvider.Models;
using System.Reflection.Emit;
using System.Drawing;
using System.IO;
using System.Net;

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
        public DbSet<AppRecipe> Recipes { get; set; }
        public DbSet<RecipeCuisineTypes> RecipeCuisineTypes { get; set; }
        public DbSet<RecipeMealTypes> RecipeMealTypes { get; set; }
        public DbSet<RecipeDishTypes> RecipeDishTypes { get; set; }
        public DbSet<RecipeHealthLabels> RecipeHealthLabels { get; set; }
        public DbSet<RecipeDietLabels> RecipeDietLabels { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<RecipeCuisineTypes>()
            .HasKey(rct => new { rct.AppRecipeId, rct.CuisineTypeId });
            modelBuilder.Entity<RecipeMealTypes>()
            .HasKey(rct => new { rct.AppRecipeId, rct.MealTypeId });
            modelBuilder.Entity<RecipeDishTypes>()
            .HasKey(rct => new { rct.AppRecipeId, rct.DishTypeId });
            modelBuilder.Entity<RecipeHealthLabels>()
            .HasKey(rct => new { rct.AppRecipeId, rct.HealthLabelId });
            modelBuilder.Entity<RecipeDietLabels>()
            .HasKey(rct => new { rct.AppRecipeId, rct.DietLabelId });

            base.OnModelCreating(modelBuilder);

            // Seeding CuisineTypes
            var cuisineTypeList = new List<string>
            {
                "american",
                "asian",
                "british",
                "caribbean",
                "central europe",
                "chinese",
                "eastern europe",
                "french",
                "greek",
                "indian",
                "italian",
                "japanese",
                "korean",
                "kosher",
                "mediterranean",
                "mexican",
                "middle eastern",
                "nordic",
                "south american",
                "south east asian",
                "world"
            };

            foreach (var cuisineTypeName in cuisineTypeList)
            {
                modelBuilder.Entity<CuisineType>().HasData(new CuisineType
                {
                    Id = cuisineTypeList.IndexOf(cuisineTypeName) + 1, 
                    Name = cuisineTypeName
                });
            }

            // Seeding MealTypes
            var mealTypeList = new List<string>
            {
                "breakfast",
                "brunch",
                "lunch/dinner",
                "snack",
                "teatime"
            };

            foreach (var mealTypeName in mealTypeList)
            {
                modelBuilder.Entity<MealType>().HasData(new MealType
                {
                    Id = mealTypeList.IndexOf(mealTypeName) + 1, 
                    Name = mealTypeName
                });
            }

            // Seeding DishTypes
            var dishTypeList = new List<string>
            {
                "alcohol cocktail",
                "biscuits and cookies",
                "bread",
                "cereals",
                "condiments and sauces",
                "desserts",
                "drinks",
                "egg",
                "ice cream and custard",
                "main course",
                "pancake",
                "pasta",
                "pastry",
                "pies and tarts",
                "pizza",
                "preps",
                "preserve",
                "salad",
                "sandwiches",
                "seafood",
                "side dish",
                "soup",
                "special occasions",
                "starter",
                "sweets"
            };

            foreach (var dishTypeName in dishTypeList)
            {
                modelBuilder.Entity<DishType>().HasData(new DishType
                {
                    Id = dishTypeList.IndexOf(dishTypeName) + 1,
                    Name = dishTypeName
                });
            }

            // Seeding HealthLabels
            var healthLabelList = new List<string>
            {
                "alcohol-cocktail",
                "alcohol-free",
                "celery-free",
                "crustcean-free",
                "dairy-free",
                "dash",
                "egg-free",
                "fish-free",
                "fodmap-free",
                "gluten-free",
                "immuno-supportive",
                "keto-friendly",
                "kidney-friendly",
                "fosher",
                "low Potassium",
                "low Sugar",
                "lupine-Free",
                "mediterranean",
                "mollusk-Free",
                "mustard-Free",
                "no oil added",
                "paleo",
                "peanut-free",
                "pescatarian",
                "pork-free",
                "red-meat-free",
                "sesame-free",
                "shellfish-free",
                "soy-free",
                "sugar-conscious",
                "sulfite-free",
                "sree-nut-free",
                "vegan",
                "vegetarian",
                "wheat-free"
            };

            foreach (var healthLabelName in healthLabelList)
            {
                modelBuilder.Entity<HealthLabel>().HasData(new HealthLabel
                {
                    Id = healthLabelList.IndexOf(healthLabelName) + 1,
                    Name = healthLabelName
                });
            }

            //Seed DietLabels
            var dietLabelList = new List<string>
            {
                "balanced",
                "high-fiber",
                "high-protein",
                "low-carb",
                "low-fat",
                "low-sodium"
            };

            foreach (var dietLabelName in dietLabelList)
            {
                modelBuilder.Entity<DietLabel>().HasData(new DietLabel
                {
                    Id = dietLabelList.IndexOf(dietLabelName) + 1,
                    Name = dietLabelName
                });
            }
        }


    }
}