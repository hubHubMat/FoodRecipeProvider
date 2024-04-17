using FoodRecipeProvider.Data;
using Microsoft.Extensions.Options;

namespace FoodRecipeProvider.Services
{
    public class ExportMLData
    {
        private readonly ApplicationDbContext _context;

        public ExportMLData(ApplicationDbContext context)
        {
            _context = context;
        }


    }
}
