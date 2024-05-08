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
            .AddHostedServices()
            .AddHttpClient();
    })
    .UseConsoleLifetime()
    .Build();

// var service = host.Services.GetService<OpenWeatherService>();
// var locations = service.GetLocations("позняки");
// var result = service.GetCurrentWeather();

await host.RunAsync();

// var serviceProvider = CreateServices();
// var aboba = serviceProvider.GetService<TestService>();
// var aboba2 = serviceProvider.GetService<TestService2>();
// aboba?.DisplaySomething();
// aboba2?.Display();

// var botSettings = GetCommonSettings();
// var telegramBotClient = new TelegramBotClient(botSettings.BotToken);
// telegramBotClient.StartReceiving(ReceivingHandlers.HandleUpdateAsync, ReceivingHandlers.HandlePollingErrorAsync);