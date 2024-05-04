namespace FoodRecipeProvider.Models.APIRecipeResponse
{
    public class SearchByQueryResponse
    {
        public string q { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public bool more { get; set; }
        public int count { get; set; }
        public Links _links { get; set; }
        public List<Hit> hits { get; set; }
    }
    public class SearchByUriResponse
    {
        public int from { get; set; }
        public int to { get; set; }
        public int count { get; set; }
        public Links _links { get; set; }
        public List<Hit> hits { get; set; }
    }
    public class SearchByUrisResponse
    {
        public int from { get; set; }
        public int to { get; set; }
        public int count { get; set; }
        public Links _links { get; set; }
        public List<Hit> hits { get; set; }
    }
    public class SearchTags
    {
        public string? keyword { get; set; }
        public List<string>? healthLabels { get; set; }
        public List<string>? dietLabels { get; set; }
        public string cuisineType { get; set; }
        public string dishType { get; set; }
        public string mealType { get; set; }
    }
    public class SearchQueryModel
    {
        public SearchByQueryResponse SearchByQueryResponse { get; set; }
        public SearchByUrisResponse SearchByUrisResponse { get; set; }
        public SearchTags SearchTags { get; set; }
        public List<string>? AvailableDietLabels { get; set; }
        public List<string>? AvailableHealthLabels { get; set; }
        public List<string>? AvailableDishTypes { get; set; }
        public List<string>? AvailableCuisineTypes { get; set; }
        public List<string>? AvailableMealTypes { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public bool ifDefaultHealth { get; set; }
        public bool ifDefaultDiet { get; set; }
        public bool ifDefaultCuisine { get; set; }
    }
}
