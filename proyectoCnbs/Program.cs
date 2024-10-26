using Microsoft.EntityFrameworkCore;
using proyectoCnbs.Data;
using proyectoCnbs.Services;

var builder = WebApplication.CreateBuilder(args);

//Configuramos la conexi�n a sql ser local db MSSQLLOCAL
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
            opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();


builder.Services.AddTransient<ApiService>();

// Registrar el servicio de XmlProcessor
builder.Services.AddTransient<XmlProcessor>();


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

app.Run();
