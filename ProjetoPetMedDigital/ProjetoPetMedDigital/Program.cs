using ProjetoPetMedDigital.Models; // NECESSÁRIO para PetMedContext
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity; // NECESSÁRIO para IdentityUser, IdentityRole
using Microsoft.Extensions.DependencyInjection; // NECESSÁRIO para AddDefaultIdentity
using ProjetoPetMedDigital.Data.Initializer; // <<< ADICIONADO PARA O DBINITIALIZER

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

// *** INÍCIO DO BLOCO DE CHAMADA DO DBINITIALIZER ***
// Este bloco DEVE vir APÓS app.Build() e ANTES de app.Run()
// Ele será executado na inicialização da aplicação
using (var scope = app.Services.CreateScope())
{
    await DbInitializer.Initialize(scope.ServiceProvider);
}
// *** FIM DO BLOCO DE CHAMADA DO DBINITIALIZER ***


// ** INÍCIO DA ORDEM CORRETA DE MAPEAMENTO DE ROTAS **
// Primeiro, mapeie Razor Pages (necessário para o Identity)
app.MapRazorPages(); // Esta linha DEVE vir antes do MapControllerRoute se o default for uma Razor Page

// Depois, mapeie a rota padrão para Controllers MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Mantenha Home/Index como default para Controllers

// Adicione um endpoint para redirecionar a raiz explicitamente para o login do Identity.
// Este MapFallbackToPage é o mais garantido para pegar a rota '/' e redirecionar para uma Razor Page.
app.MapFallbackToPage("/Identity/Account/Login"); // Redireciona QUALQUER rota não encontrada para a página de login do Identity
// ** FIM DA ORDEM CORRETA DE MAPEAMENTO DE ROTAS **

app.Run();