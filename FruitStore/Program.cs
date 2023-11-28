using FruitStore.Models.Entities;
using FruitStore.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<Repository<Categorias>>();
builder.Services.AddTransient<ProductosRepository>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.AccessDeniedPath = "/Home/Denied";
    x.LoginPath = "/Home/Login";
    x.LogoutPath = "/Home/Logout";
    x.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    x.Cookie.Name = "fruteriacookie"; //poner nombre personalizado referente a ti cuando el proyecto esté en linea
});
//CLase que es utilizada por muchas clases (servicio)
builder.Services.AddMvc();
builder.Services.AddDbContext<FruteriashopContext>(
    x=> x.UseMySql("user=root;password=root;server=localhost;database=fruteriashop", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql")));
var app = builder.Build();
app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
app.UseFileServer();
app.MapDefaultControllerRoute();
app.UseAuthentication();
app.UseAuthorization();
app.Run();
