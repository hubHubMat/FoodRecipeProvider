using Newtonsoft.Json;

namespace FoodRecipeProvider.Models.APIRecipeResponse
{  

    public class Images
    {
        public THUMBNAIL THUMBNAIL { get; set; }
        public SMALL SMALL { get; set; }
        public REGULAR REGULAR { get; set; }
        public LARGE LARGE { get; set; }
    }

   

    public class LARGE
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
        public Next next { get; set; }
    }

    

    public class REGULAR
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    
    public class Self
    {
        public string title { get; set; }
        public string href { get; set; }
    } 
    public class Next
    {
        public string title { get; set; }
        public string href { get; set; }
    }

    public class SMALL
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }


    public class THUMBNAIL
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }  


}
