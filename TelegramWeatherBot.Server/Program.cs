using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using TelegramWeatherBotServer;
using TelegramWeatherBotServer.Extensions;
using TelegramWeatherBotServer.Settings;

const string configPath = "config.json";
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services
            .AddSettings(configPath)
            .AddHttpClient()
            .AddMainServices();
    })
    .UseConsoleLifetime()
    .Build();

await host.RunAsync();

// var serviceProvider = CreateServices();
// var aboba = serviceProvider.GetService<TestService>();
// var aboba2 = serviceProvider.GetService<TestService2>();
// aboba?.DisplaySomething();
// aboba2?.Display();

// var botSettings = GetCommonSettings();
// var telegramBotClient = new TelegramBotClient(botSettings.BotToken);
// telegramBotClient.StartReceiving(ReceivingHandlers.HandleUpdateAsync, ReceivingHandlers.HandlePollingErrorAsync);