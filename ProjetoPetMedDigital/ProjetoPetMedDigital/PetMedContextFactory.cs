using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration; // Adicione este using
using ProjetoPetMedDigital.Data; // Certifique-se de que este é o namespace correto do seu DbContext
using System.IO; // Adicione este using se já não estiver

namespace ProjetoPetMedDigital
{
    public class PetMedContextFactory : IDesignTimeDbContextFactory<PetMedContext>
    {
        public PetMedContext CreateDbContext(string[] args)
        {
            // 1. Configura o builder de configuração para ler o appsettings.json
            //    Isso permite que a fábrica de DbContext leia sua string de conexão
            //    exatamente como o seu aplicativo faria em tempo de execução.
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Define o caminho base para a pasta do projeto
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Carrega o appsettings.json
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true, reloadOnChange: true) // Carrega o appsettings.Development.json se existir
                .Build();

            // 2. Obtém a string de conexão pelo nome definido em appsettings.json
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Validação simples para garantir que a string de conexão foi encontrada
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            }

            // 3. Configura as opções do DbContext usando a string de conexão
            var optionsBuilder = new DbContextOptionsBuilder<PetMedContext>();
            optionsBuilder.UseSqlServer(connectionString); // Usando SQL Server como provedor

            // 4. Retorna uma nova instância do seu DbContext com as opções configuradas
            return new PetMedContext(optionsBuilder.Options);
        }
    }
}