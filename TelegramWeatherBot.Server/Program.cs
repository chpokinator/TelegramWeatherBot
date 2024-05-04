using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using TelegramWeatherBot.Core.Handlers;
using TelegramWeatherBotServer.Configuration;

var botSettings = GetSettings();
var telegramBotClient = new TelegramBotClient(botSettings.BotToken);
telegramBotClient.StartReceiving(ReceivingHandlers.HandleUpdateAsync, ReceivingHandlers.HandlePollingErrorAsync);


SomeService.RunSomethingPeriodically(botSettings.DelayValue, telegramBotClient);
Console.ReadLine();
return;

BotSettings GetSettings()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("config.json", optional: false);

    var configuration = builder.Build();
    return configuration.GetSection("botConfiguration").Get<BotSettings>()!;
}

internal static class SomeService
{
    public static async void RunSomethingPeriodically(int secondsDelay, ITelegramBotClient client)
    {
        var chatId = "163860339";
        if (secondsDelay <= 1)
        {
            return;
        }
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(secondsDelay));
        while (await timer.WaitForNextTickAsync())
        {
            await client.SendTextMessageAsync(chatId, $"Я тебе сообщениями спамлю, это сообщение отправлено {DateTime.Now:HH:mm:ss.fff}");
            Console.WriteLine($"отправлено сообщение в чат {chatId}");
        }
    }
}