using Microsoft.AspNetCore.Mvc;

namespace FoodRecipeProvider.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
