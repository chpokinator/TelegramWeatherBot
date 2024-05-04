using Microsoft.Extensions.Configuration;
using TelegramWeatherBot.Services.Settings;

namespace TelegramWeatherBot.Services.Subscriptions;

public class SubscriptionsBackgroundService
{
    private readonly SubscriptionsSettings _subscriptionsSettings = SubscriptionsSettings.Create();

    public async Task<List<string>> RunAsync()
    {
        // var chatId = "163860339";
        // if (secondsDelay <= 1)
        // {
        //     return;
        // }
        // using var timer = new PeriodicTimer(TimeSpan.FromSeconds(secondsDelay));
        // while (await timer.WaitForNextTickAsync())
        // {
        //     await client.SendTextMessageAsync(chatId, $"Я тебе сообщениями спамлю, это сообщение отправлено {DateTime.Now:HH:mm:ss.fff}");
        //     Console.WriteLine($"отправлено сообщение в чат {chatId}");
        // }

        using var periodicTimer =
            new PeriodicTimer(TimeSpan.FromMinutes(_subscriptionsSettings.SubscriptionsSyncTimeoutInMinutes));


        throw new NotImplementedException();
    }
}