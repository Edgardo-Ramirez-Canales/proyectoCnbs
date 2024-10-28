using Microsoft.EntityFrameworkCore;
using proyectoCnbs.Data;
using proyectoCnbs.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
            opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql")));


builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();


builder.Services.AddTransient<ApiService>();

builder.Services.AddTransient<XmlProcessor>();


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
