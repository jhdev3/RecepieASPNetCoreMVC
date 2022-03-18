using RecipeWebsiteMVC.Data;
using Microsoft.EntityFrameworkCore;
using RecipeWebsiteMVC.DataAccess.Interfaces;
using RecipeWebsiteMVC.DataAccess;
using Microsoft.AspNetCore.Identity;
using RecipeWebsiteMVC.Models.EmailSender;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Add(new DoubleEntityBinderProvider());
});
//Setting up the connection string for AppDBContext
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>();//Lägger till möjligheten att sätta roler på User och använda mig av de tabellerna som Identity framework tillhandahåller :)
//builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
//    .AddEntityFrameworkStores<AppDbContext>();
//builder.Services.AddDefaultIdentity<IdentityUser>()
  //  .AddEntityFrameworkStores<AppDbContext>();
//Register IunitOfWork :)
//Ett request blir recived blir Scoped f�r det objekt. B�r vara f�rsiktigt med trancient som skapar f�r varje tryck 
//kr�vs bara 1 Inejection h�r men m�ste uppdatera IunitOfWork/UnitOfWork f�r varje tabell man vill anv�nda d�r ;)

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddSingleton<IEmailSender, FakeEmailSender>();
//För att Slippa skapa alla Login och User saker själv
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();



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
//order of pipline �r viktigt 
app.UseRouting();
//Viktigt att det är flre Authorization för man borde utför Authentication först ;)
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
//Hur Routing g�r dvs vilket pattern som anv�nds controller -> views -> dvs MVC Modelen:)
//Det �r �ven s� URL blir uppdelad i n�r man anv�nder MVC :)
app.MapControllerRoute(
    name: "default",
    pattern: "{area=User}/{controller=Home}/{action=Index}/{id?}");



app.Run();
