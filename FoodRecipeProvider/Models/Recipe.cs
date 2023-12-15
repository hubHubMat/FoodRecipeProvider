using Newtonsoft.Json;

namespace FoodRecipeProvider.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    
    public class Hit
    {
        public Recipe recipe { get; set; }
    }

    public class Ingredient
    {
        public string text { get; set; }
        public double quantity { get; set; }
        public string measure { get; set; }
        public string food { get; set; }
        public double weight { get; set; }
        public string foodCategory { get; set; }
        public string foodId { get; set; }
        public string image { get; set; }
    }

    

    public class Recipe
    {
        public string uri { get; set; }
        public string label { get; set; }
        public string image { get; set; }
        public Images images { get; set; }
        public string source { get; set; }
        public string url { get; set; }
        public string shareAs { get; set; }
        public double yield { get; set; }
        public List<string> dietLabels { get; set; }
        public List<string> healthLabels { get; set; }
        public List<string> cautions { get; set; }
        public List<string> ingredientLines { get; set; }
        public List<Ingredient> ingredients { get; set; }
        public double calories { get; set; }
        public double totalWeight { get; set; }
        public double totalTime { get; set; }
        public List<string> cuisineType { get; set; }
        public List<string> mealType { get; set; }
        public List<string> dishType { get; set; }
        public TotalNutrients totalNutrients { get; set; }
        public TotalDaily totalDaily { get; set; }
        public List<Digest> digest { get; set; }
        public List<string> instructionLines { get; set; }
        public List<string> tags { get; set; }
    }


    public class Digest
    {
        public string label { get; set; }
        public string tag { get; set; }
        public string schemaOrgTag { get; set; }
        public double total { get; set; }
        public bool hasRDI { get; set; }
        public double daily { get; set; }
        public string unit { get; set; }
        public List<Sub> sub { get; set; }
    }

    public class Sub
    {
        public string label { get; set; }
        public string tag { get; set; }
        public string schemaOrgTag { get; set; }
        public double total { get; set; }
        public bool hasRDI { get; set; }
        public double daily { get; set; }
        public string unit { get; set; }
    }
}
