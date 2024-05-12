using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TelegramWeatherBotServer.Extensions;

const string configPath = "config.json";
var configuration = new ConfigurationBuilder()
    .AddJsonFile(configPath, optional: false)
    .Build();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services
            .AddSettings(configuration)
            .AddBotServices(configuration)
            .AddWeatherServices()
            .AddOtherServices()
            .AddHostedServices()
            .AddHttpClient();
    })
    .UseConsoleLifetime()
    .Build();

await host.RunAsync();