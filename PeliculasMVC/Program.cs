using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PeliculasMVC.Data;
using System.Globalization;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PeliculasMVCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PeliculasMVCContext") ?? throw new InvalidOperationException("Connection string 'PeliculasMVCContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//esto sirve para localizar los idiomas 
app.UseRequestLocalization("en-US", "es-CO");

//Esto para poner 200,000.56
var cultureInfo = new CultureInfo("en-US");
cultureInfo.NumberFormat.CurrencyDecimalSeparator = ".";
cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

app.Run();
