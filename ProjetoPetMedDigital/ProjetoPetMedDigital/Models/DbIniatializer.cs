using Microsoft.AspNetCore.Identity; // Necessário para UserManager e RoleManager
using Microsoft.Extensions.DependencyInjection; // Necessário para GetRequiredService
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoPetMedDigital.Models // Namespace para o Initializer
{
    public static class DbInitializer // Classe estática para métodos de seed
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            // Obter os serviços de RoleManager e UserManager
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Criar os perfis (Roles) se eles não existirem
            string[] roleNames = { "Administrador", "Veterinario", "Secretaria" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Criar um usuário administrador padrão se ele não existir
            string adminEmail = "admin@petmed.com";
            string adminPassword = "Admin@123"; // Uma senha forte (com maiúscula, minúscula, número, caractere especial)

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new IdentityUser
                {
                    UserName = adminEmail, // UserName é geralmente o email ou um login único
                    Email = adminEmail,
                    EmailConfirmed = true // Marcar como true para que ele possa fazer login imediatamente
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword); // Cria o usuário
                if (result.Succeeded)
                {
                    // Atribuir o perfil "Administrador" ao usuário recém-criado
                    await userManager.AddToRoleAsync(adminUser, "Administrador");
                }
            }
        }
    }
}