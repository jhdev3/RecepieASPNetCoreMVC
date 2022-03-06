using RecipeWebsiteMVC.Data;
using Microsoft.EntityFrameworkCore;
using RecipeWebsiteMVC.DataAccess.Interfaces;
using RecipeWebsiteMVC.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Setting up the connection string for AppDBContext
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ));
//Register ICategory :)
//Ett request blir recived blir Scoped för det objekt. Bör vara försiktigt med trancient som skapar för varje tryck 
//krävs bara 1 Inejection här men måste uppdatera IunitOfWork/UnitOfWork för varje tabell man vill använda där ;)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
//order of pipline är viktigt 
app.UseRouting();

app.UseAuthorization();
//Hur Routing går dvs vilket pattern som används controller -> views -> dvs MVC Modelen:)
//Det är även så URL blir uppdelad i när man använder MVC :)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
