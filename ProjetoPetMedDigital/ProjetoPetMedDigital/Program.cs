using Microsoft.AspNetCore.Identity; // NECESSÁRIO para IdentityUser, IdentityRole
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection; // NECESSÁRIO para AddDefaultIdentity
using ProjetoPetMedDigital.Models; // NECESSÁRIO para PetMedContext

// Certifique-se de que não há nenhum outro 'using' para "PetMed_Digital.Models" ou duplicado aqui.

var builder = WebApplication.CreateBuilder(args);

// ⬇️ Configura o DbContext para o Entity Framework
builder.Services.AddDbContext<PetMedContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ⬇️ Configura o ASP.NET Core Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>() // Permite usar perfis (Roles)
    .AddEntityFrameworkStores<PetMedContext>(); // Conecta o Identity ao seu PetMedContext

// Add services to the container. (Adiciona suporte a Controllers com Views)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuração do pipeline de requisições HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Redireciona erros para a página de erro do Home
    app.UseHsts();
}

app.UseHttpsRedirection(); // Redireciona requisições HTTP para HTTPS
app.UseStaticFiles();      // Permite servir arquivos estáticos (CSS, JS, imagens)

app.UseRouting();          // Habilita o roteamento

app.UseAuthentication();   // Habilita a autenticação (necessário para o Identity)
app.UseAuthorization();    // Habilita a autorização

// Mapeamento da rota padrão (AGORA PARA A TELA DE LOGIN DO IDENTITY)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Identity}/{action=Account/Login}/{id?}"); // <-- ALTERADO AQUI

// Mapeia as Razor Pages (essencial para o Identity UI)
app.MapRazorPages();
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    // Seed Roles (Perfis)
    string[] roleNames = { "Administrador", "Veterinario", "Secretaria" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // Seed Admin User
    string adminEmail = "admin@petmed.com";
    string adminPassword = "Admin@123";

    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var adminUser = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Administrador");
        }
    }
}

app.Run();