using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace FoodRecipeProvider.Models
{

        public class CuisineType
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public ICollection<RecipeCuisineTypes> RecipeCuisineTypes { get; set; } = new List<RecipeCuisineTypes>();

        }
        public class MealType
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public ICollection<RecipeMealTypes> RecipeMealTypes { get; set; } = new List<RecipeMealTypes>();
         }
        public class DishType
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public ICollection<RecipeDishTypes> RecipeDishTypes { get; set; } = new List<RecipeDishTypes>();
        }
        public class HealthLabel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public ICollection<RecipeHealthLabels> RecipeHealthLabels { get; set; } = new List<RecipeHealthLabels>();
        }
        public class DietLabel
        {
            public int Id { get; set; }
            public string Name { get; set; }
         public ICollection<RecipeDietLabels> RecipeDietLabels { get; set; } = new List<RecipeDietLabels>();
        }

    }

