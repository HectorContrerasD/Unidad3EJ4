using FruitStore.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMvc();
builder.Services.AddDbContext<FruteriashopContext>(
    x=> x.UseMySql("user=root;password=root;server=localhost;database=fruteriashop", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql")));
var app = builder.Build();

app.UseFileServer();
app.MapDefaultControllerRoute();

app.Run();
