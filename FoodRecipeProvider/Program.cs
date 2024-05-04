using FoodRecipeProvider.Data;
using FoodRecipeProvider.Models;
using FoodRecipeProvider.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddHttpClient<EdamamApiClient>(client =>
{
    client.BaseAddress = new Uri("https://api.edamam.com/");
});

builder.Services.AddHttpClient<RRSApiClient>(client =>
{
    client.BaseAddress = new Uri("http://127.0.0.1:5000");
});

builder.Services.Configure<EdamamApiOptions>(configuration.GetSection("EdamamApi"));

builder.Services.AddControllersWithViews();

var app = builder.Build();




if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "recipes",
    pattern: "{controller=Recipes}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
