using ProjetoPetMedDigital.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.Configuration; 
using Microsoft.Extensions.DependencyInjection; 
using Microsoft.Extensions.Hosting; 


var builder = WebApplication.CreateBuilder(args);

// ⬇️ Aqui você adiciona o DbContext para o Entity Framework
builder.Services.AddDbContext<PetMedContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseStaticFiles(); // ESSA LINHA PRECISA ESTAR AQUI para servir CSS, JS, Imagens da wwwroot

app.UseRouting();

app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();