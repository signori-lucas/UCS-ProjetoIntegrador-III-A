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
                    services.AddSingleton<LitaDeAlunosService>();
                    services.AddSingleton<LitaDeTurmasService>();
                    services.AddSingleton<MenuService>();
                })
                .Build();

            var menu = host.Services.GetRequiredService<MenuService>();
            menu.AbrirMenu();

            await host.StopAsync();
        }
    }
}
