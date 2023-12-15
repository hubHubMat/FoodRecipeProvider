namespace FoodRecipeProvider.Models
{
    public class SearchByQueryResponse
    {
        public string q { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public bool more { get; set; }
        public int count { get; set; }
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
}
