using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using UCS_ProjetoIntegrador_III_A.Services;

namespace UCS_ProjetoIntegrador_III_A
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    // Registre aqui seus serviços para injeção de dependência
                    // Exemplo: registra um serviço de domínio e o MenuService que recebe esse serviço via construtor
                    services.AddSingleton<LitaDeAlunosService>();
                    services.AddSingleton<LitaDeTurmasService>();
                    services.AddSingleton<MenuService>();
                    // ex: services.AddTransient<IMeuServico, MeuServico>();
                })
                .Build();

            // Resolve e executa o serviço principal de menu
            var menu = host.Services.GetRequiredService<MenuService>();
            menu.AbrirMenu();

            // Se precisar de operações assíncronas do host, use StartAsync/StopAsync
            await host.StopAsync();
        }
    }
}
