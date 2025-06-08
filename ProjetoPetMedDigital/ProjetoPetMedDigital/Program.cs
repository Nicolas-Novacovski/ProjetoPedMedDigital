using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity; // NECESSÁRIO
using Microsoft.Extensions.DependencyInjection;
using ProjetoPetMedDigital.Models; // NECESSÁRIO para AddDefaultIdentity


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PetMedContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PetMedContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    await DbInitializer.Initialize(scope.ServiceProvider);
}

    app.Run();