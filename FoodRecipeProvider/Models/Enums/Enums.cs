using System.Reflection;

namespace FoodRecipeProvider.Models { 

    public enum CuisineTypeEnum
    {
        American,
        Asian,
        British,
        Caribbean,
        Central_europe,
        Chinese,
        Eastern_europe,
        French,
        Greek,
        Indian,
        Italian,
        Japanese,
        Korean,
        Kosher,
        Mediterranean,
        Mexican,
        Middle_eastern,
        Nordic,
        South_american,
        South_east_asian,
        World
    }
 
    public enum MealTypeEnum
    {
        Breakfast,
        Brunch,
        Lunch_dinner,
        Snack,
        Teatime

    }

    public enum DishTypeEnum
    {
        alcohol_cocktail,
        biscuits_and_cookies,
        bread,
        cereals,
        condiments_and_sauces,
        desserts,
        drinks,
        egg,
        ice_cream_and_custard,
        main_course,
        pancake,
        pasta,
        pastry,
        pies_andtarts,
        pizza,
        preps,
        preserve,
        salad,
        sandwiches,
        seafood,
        side_dish,
        soup,
        special_occasions,
        starter,
        sweets
    }
    public enum HealthLabelEnum
    {
        alcohol_cocktail,
        alcohol_free,
        celery_free,
        crustacean_free,
        dairy_free,
        DASH,
        egg_free,
        fish_free,
        fodmap_free,
        gluten_free,
        immuno_supportive,
        keto_friendly,
        kidney_friendly,
        kosher,
        low_potassium,
        low_sugar,
        lupine_free,
        mediterranean,
        mollusk_free,
        mustard_free,
        No_oil_added,
        paleo,
        peanut_free,
        pecatarian,
        pork_free,
        red_meat_free,
        sesame_free,
        shellfish_free,
        soy_free,
        sugar_conscious,
        sulfite_free,
        tree_nut_free,
        vegan,
        vegetarian,
        wheat_free 

    }
    public enum DietLabelEnum
    {
        balanced,
        high_fiber,
        high_protein,
        low_carb,    
        low_fat, 
        low_sodium,
    }

}