﻿namespace FoodRecipeProvider.Models
{

    public class RecipeCuisineTypes
    {
        public int AppRecipeId { get; set; }
        public AppRecipe Recipe { get; set; }

        public int CuisineTypeId { get; set; }
        public CuisineType CuisineType { get; set; }
    }
    public class RecipeMealTypes
    {
        public int AppRecipeId { get; set; }
        public AppRecipe AppRecipe { get; set; }
        public int MealTypeId { get; set; }
        public MealType MealType { get; set; }
    }
    public class RecipeDishTypes
    {
        public int AppRecipeId { get; set; }
        public AppRecipe AppRecipe { get; set; }
        public int DishTypeId { get; set; }
        public DishType DishType { get;set; }
    }
    public class RecipeHealthLabels
    {
        public int AppRecipeId { get; set; }
        public AppRecipe AppRecipe { get; set; }    
        public int HealthLabelId { get; set; }
        public HealthLabel HealthLabel { get; set; }
    }
    public class RecipeDietLabels
    {
        public int AppRecipeId { get; set; }
        public AppRecipe AppRecipe { get; set; }
        public int DietLabelId { get; set; }
        public DietLabel DietLabel { get; set; }
    }
}
