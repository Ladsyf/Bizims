using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace GenerateTypescriptClient;

internal class Program
{
    static async Task Main(string[] args)
    {
        var host = CreateHostBuilder().ConfigureAppConfiguration((context, config) =>
        {
            var environment = context.HostingEnvironment.EnvironmentName;
            config.SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                  .AddEnvironmentVariables();
        })
            .ConfigureServices(services => 
            {
                services
                    .AddOptions<ClientGeneratorSettings>()
                    .BindConfiguration(nameof(ClientGeneratorSettings));
            })
            .Build();


        var scope = host.Services.CreateScope();

        var services = scope.ServiceProvider;

        var clientGeneratorSettings = services.GetRequiredService<IOptions<ClientGeneratorSettings>>().Value;

        await ClientGenerator.GenerateWebClientAsync(clientGeneratorSettings);
    }

    public static IHostBuilder CreateHostBuilder(params string[] args) =>
        Host.CreateDefaultBuilder(args);
}
