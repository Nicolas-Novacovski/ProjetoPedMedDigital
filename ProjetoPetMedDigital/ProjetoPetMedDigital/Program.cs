using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjetoPetMedDigital.Models; // Certifique-se de que este using está correto para o DbInitializer

var builder = WebApplication.CreateBuilder(args);

// Adiciona o contexto do banco de dados
builder.Services.AddDbContext<PetMedContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configura o Identity para autenticação
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PetMedContext>();

// Adiciona suporte a controladores MVC com views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuração do pipeline de requisições HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Redireciona erros para a página de erro do Home
    app.UseHsts();
}

app.UseHttpsRedirection(); // Redireciona requisições HTTP para HTTPS
app.UseStaticFiles();     // Permite servir arquivos estáticos (CSS, JS, imagens)

app.UseRouting();         // Habilita o roteamento

app.UseAuthentication();  // Habilita a autenticação (necessário para o Identity)
app.UseAuthorization();   // Habilita a autorização

// ***** RESTAURADO: Rota padrão para Home/Index *****
// Esta rota é a rota padrão para seus controladores MVC.
// Após o login, o usuário provavelmente será redirecionado para cá.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Voltar para Home/Index como padrão

// Mapeia as Razor Pages (essencial para o Identity UI)
app.MapRazorPages();

// ***** ADIÇÃO CRÍTICA: Redireciona a raiz ("/") para a página de login do Identity *****
// Isso garante que ao iniciar a aplicação, a primeira tela seja a de login.
app.MapGet("/", context => {
    // Redireciona para o caminho padrão da página de login do Identity.
    // O caminho para as páginas do Identity UI é sempre /Identity/Account/Login
    context.Response.Redirect("/Identity/Account/Login");
    return Task.CompletedTask; // Retorna um Task.CompletedTask para indicar que a operação assíncrona foi concluída.
});
// ***********************************************************************************


// Chamada ao inicializador de banco de dados para criar Roles e um Admin padrão
// Este bloco deve estar APÓS app.Build() e ANTES de app.Run()
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

    // Se você tinha um DbInitializer.Initialize(scope.ServiceProvider) antes,
    // pode ter sido substituído ou integrado aqui.
    // Se o seu DbInitializer tinha outras lógicas, certifique-se de que ainda são executadas.
    // Exemplo:
    // await DbInitializer.Initialize(scope.ServiceProvider); 
}

app.Run();